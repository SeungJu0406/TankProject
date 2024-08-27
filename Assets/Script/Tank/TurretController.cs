using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float maxAngle;
    void Update()
    {
        Rotate();
    }

    void Rotate()
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
}
