using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float sprintSpeed = 15;
    public float rotationSpeed = 400;

    public Transform cam;
    public Rigidbody playerBody;
    public Camera playerCamera;

    public float jumpHeight = 10;
    bool isGrounded;
    public float gravity = 9.81f;

    GunController gunController;
    Vector3 velocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gunController = GetComponent<GunController>();
        //gunController.EquipGun(gunController.gun);
    }


    void FixedUpdate()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        
        if (Input.GetButton("Sprint") && isGrounded)
        {
            transform.Translate(inputVector * Time.deltaTime * sprintSpeed);

        }
        else
        {
            transform.Translate(inputVector * Time.deltaTime * speed);
        }

        Quaternion previousCamRotation = cam.rotation;
        cam.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime), 0, 0, Space.Self);
        if (Vector3.Angle(cam.forward, transform.forward) > 90) cam.rotation = previousCamRotation;

        transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), 0, Space.World);

        if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && isGrounded)
        {
            velocity = new Vector3(0, Mathf.Sqrt(2 * jumpHeight * gravity), 0);
        }
        velocity.y -= gravity * Time.deltaTime;
        playerBody.velocity = velocity;

        //Mouse input
        if (Input.GetMouseButton(0))
        {
//            gunController.OnTriggerHold();
        }
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
        velocity = Vector3.zero;
    }
    private void OnCollisionExit()
    {
        isGrounded = false;
    }

}
