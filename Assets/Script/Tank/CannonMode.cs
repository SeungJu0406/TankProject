using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class CannonMode : MonoBehaviour
{
    [SerializeField] TurretController turret;
    [SerializeField] float rotateSpeed;
    private void Update()
    {
        Look();
    }
    void Look()
    {
        float x = Input.GetAxis("Mouse X"); // 마우스 x축 움직임 량
        float y = Input.GetAxis("Mouse Y");

        turret.transform.Rotate(Vector3.up, rotateSpeed * x * Time.deltaTime, Space.World);
        turret.transform.Rotate(Vector3.left, rotateSpeed * y * Time.deltaTime);
    }
}
