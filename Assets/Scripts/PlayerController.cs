using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 400;
    public Transform cam;
    public Rigidbody playerBody;
    public float jumpHeight = 10;
    bool isGrounded;
    bool jumpPressed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(inputVector * Time.deltaTime * speed);

        Quaternion previousCamRotation = cam.rotation;
        cam.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime), 0, 0, Space.Self);
        if (Vector3.Angle(cam.forward, transform.forward) > 90) cam.rotation = previousCamRotation;

        transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), 0, Space.World);

        jumpPressed = Input.GetButtonDown("Jump") || Input.GetButton("Jump") ;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        if (jumpPressed && isGrounded)
        {
            playerBody.velocity = new Vector3(0, jumpHeight, 0);
            isGrounded = false;
        }
    }
}
