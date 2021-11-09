using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 400;


    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(inputVector * Time.deltaTime * speed);

        transform.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime), 0, 0, Space.Self);
        transform.Rotate(0, -(Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), 0, Space.World);
    }
}
