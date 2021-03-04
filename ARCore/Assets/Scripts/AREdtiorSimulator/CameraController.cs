using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float keyboardSpeed;
    [SerializeField]
    float mouseSpeed = 0.1f;
    void Update()
    {
        Vector3 forwardDirection = this.transform.forward;
        forwardDirection = new Vector3(forwardDirection.x, 0f, forwardDirection.z);
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += forwardDirection * keyboardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= forwardDirection * keyboardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position -= new Vector3(-forwardDirection.z, 0f, forwardDirection.x) * keyboardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-forwardDirection.z, 0f, forwardDirection.x) * keyboardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.transform.position += new Vector3(0f, keyboardSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            this.transform.position -= new Vector3(0f, keyboardSpeed * Time.deltaTime, 0f);
        }
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            float x, y;
            Vector3 rotateValue;
            y = Input.GetAxis("Mouse X");
            x = Input.GetAxis("Mouse Y");
            rotateValue = new Vector3(x, y * -1, 0) * mouseSpeed * Time.deltaTime;
            transform.eulerAngles = transform.eulerAngles - rotateValue;
        }
    }
}
