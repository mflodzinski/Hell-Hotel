using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Transform graphics;
	public float msBetweenShots = 100;
	public int bulletsPerMag;
	public float range = 100;
	public float force = 200;

	[Header("Recoil")]
	public Vector2 kickMinMax = new Vector2(.05f, .2f);
	public Vector2 recoilMinMax = new Vector2(3, 5);
	public float recoilMoveSettleTime = .1f;
	public float recoilRotationSettleTime = .1f;

	[Header("Effects")]
	public Transform shell;
	public Transform shellEjector;
	Muzzleflash muzzleFlash;

	public float reloadTime = .3f;
	float nextShotTime;
	int bulletsRemainingInMag;
	bool isReloading;

	Vector3 recoilSmoothDampVelocity;
	float recoilAngle;
	float recoilRotSmoothDampVelocity;

	private void Start()
	{
		muzzleFlash = GetComponent<Muzzleflash>();
		bulletsRemainingInMag = bulletsPerMag;
	}

	private void LateUpdate()
	{
		// animate recoil
		graphics.localPosition = Vector3.SmoothDamp(graphics.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, recoilMoveSettleTime);
		if (!isReloading)
		{
			recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRotSmoothDampVelocity, recoilRotationSettleTime);
			graphics.localEulerAngles = Vector3.left * recoilAngle;
		}

		if (!isReloading && bulletsRemainingInMag == 0)
		{
			Reload();
		}
	}
	public void Shoot(Transform parent)
	{

		if (!isReloading && Time.time > nextShotTime && bulletsRemainingInMag > 0)
		{
			Instantiate(shell, shellEjector.position, shellEjector.rotation);
			bulletsRemainingInMag--;
			nextShotTime = Time.time + msBetweenShots / 1000;
			Ray ray = new Ray(parent.position, parent.forward);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, range))
            {
				if (hit.collider.gameObject.CompareTag("Obstacle"))
                {
					Destroy(hit.collider.gameObject);
                }

				if (hit.rigidbody != null)
                {
					hit.rigidbody.AddForce(transform.forward * force);
				}
            }

			muzzleFlash.Activate();
			graphics.localPosition -= Vector3.forward * Random.Range(kickMinMax.x, kickMinMax.y);
			recoilAngle += Random.Range(recoilMinMax.x, recoilMinMax.y);
			recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);
		}
	}


    public void Reload()
	{
		if (!isReloading && bulletsRemainingInMag != bulletsPerMag)
		{
			StartCoroutine(AnimateReload());
		}
	}

	IEnumerator AnimateReload()
	{
		isReloading = true;
		yield return new WaitForSeconds(.2f);

		float percent = 0;
		float reloadSpeed = 1f / reloadTime;
		Vector3 initialRot = graphics.localEulerAngles;
		float maxReloadAngle = 30;

		while (percent < 1)
		{
			percent += Time.deltaTime * reloadSpeed;
			float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
			float reloadAngle = Mathf.Lerp(0, maxReloadAngle, interpolation);
			graphics.localEulerAngles = initialRot + Vector3.left * reloadAngle;
			yield return null;
		}

		isReloading = false;
		bulletsRemainingInMag = bulletsPerMag;
	}

}