using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    private void Awake()
    {      
        rotateSpeed *= 10;
        transform.eulerAngles = Vector3.zero;
    }
    private void Start()
    {
        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = transform.rotation;
        Camera.main.transform.parent = transform;
    }
    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        transform.Rotate(Vector3.up * x * rotateSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.left * y * rotateSpeed * Time.deltaTime);
    }

    void Fire()
    {

    }
}
