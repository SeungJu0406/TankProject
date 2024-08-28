using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] ChangeMode changeMode;

    [SerializeField] float rotateSpeed;
    [SerializeField] float maxAngle;

    [SerializeField] float cannonModeRotateSpeed;
    void Update()
    {
        if(changeMode.curMode == TankMode.TopView)
        {
            TopViewMode();
        }
        else if(changeMode.curMode == TankMode.Cannon)
        {
            CannonMode();
        }
    }

    void TopViewMode()
    {
        float y = Input.GetAxisRaw("Barrel Horizontal");
        float x = Input.GetAxisRaw("Barrel Vertical");

        if (x != 0)
        {
            if (150 < transform.eulerAngles.x && transform.eulerAngles.x < 360 - maxAngle && x > 0) { }
            else if (0 < transform.eulerAngles.x && transform.eulerAngles.x < 150 && x < 0) { }
            else
            {
                transform.Rotate(Vector3.left * x * rotateSpeed * Time.deltaTime);
            }
        }
        if (y != 0)
        {
            transform.Rotate(Vector3.up * y * rotateSpeed * Time.deltaTime, Space.World);
        }
    }

    void CannonMode()
    {
        float x = Input.GetAxis("Mouse X"); // 마우스 x축 움직임 량
        float y = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, cannonModeRotateSpeed * x * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.left, cannonModeRotateSpeed * y * Time.deltaTime);
    }
}
