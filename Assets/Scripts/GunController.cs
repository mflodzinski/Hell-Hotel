using UnityEngine;

public class GunController : MonoBehaviour
{
	public Transform weaponHolder;
	public Transform cameraT;
	public Gun gun;
	Gun equippedGun;

    public void EquipGun(Gun gunToEquip)
	{
		//instantiate gun at weaponHold's possition and player's rotation
		equippedGun = Instantiate(gunToEquip, weaponHolder.position, weaponHolder.rotation);
		equippedGun.transform.parent = weaponHolder;
	}

	public void OnTriggerHold()
	{
		if (equippedGun != null)
		{
			equippedGun.Shoot(cameraT);
		}
	}
}