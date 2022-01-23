using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkAcceleration = 10;
    public float sprintAcceleration = 15;
    public float rotationSpeed = 400;
    [SerializeField]
    private float maxWalkingSpeed = 4;
    [SerializeField]
    private float maxSprintingSpeed = 7;

    public Transform cam;
    public Rigidbody playerBody;
    public Camera playerCamera;

    public float jumpHeight = 10;
    bool isGrounded;
    public float gravity = 9.81f;
    [SerializeField]
    private float frictionCoefficient = 0.1f;

    GunController gunController;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gunController = GetComponent<GunController>();
        gunController.EquipGun(gunController.gun);
    }


    void Update()
    {
        Quaternion previousCamRotation = cam.rotation;
        cam.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime), 0, 0, Space.Self);
        if (Vector3.Angle(cam.forward, transform.forward) > 90) cam.rotation = previousCamRotation;

        transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), 0, Space.World);

        //Mouse input
        if (Input.GetMouseButton(0))
        {
            gunController.OnTriggerHold();
        }
    }

    private void FixedUpdate()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float xzVelocityLength = Mathf.Sqrt(playerBody.velocity.x * playerBody.velocity.x + playerBody.velocity.z * playerBody.velocity.z);

        if (Input.GetButton("Sprint"))
        {
            //Sprinting
            if (xzVelocityLength < maxSprintingSpeed) playerBody.AddRelativeForce(inputVector * Time.fixedDeltaTime * sprintAcceleration * 6, ForceMode.VelocityChange);
        }
        else
        {
            //Walking
            if (xzVelocityLength < maxWalkingSpeed) playerBody.AddRelativeForce(inputVector * Time.fixedDeltaTime * walkAcceleration * 6, ForceMode.VelocityChange);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            playerBody.AddRelativeForce(new Vector3(0, jumpHeight, 0), ForceMode.VelocityChange);
        }
        
        playerBody.AddRelativeForce(new Vector3(0, -gravity, 0), ForceMode.Acceleration);
        //Fake friction
        playerBody.AddForce(-new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z) * frictionCoefficient, ForceMode.VelocityChange);
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }
    private void OnCollisionExit()
    {
        isGrounded = false;
    }

}
