using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
     

    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(inputVector * Time.deltaTime * speed);

        transform.Rotate((Input.GetAxis("Mouse Y") * 100 * Time.deltaTime), (Input.GetAxis("Mouse X") * 100 * Time.deltaTime), 0);
    }
}
