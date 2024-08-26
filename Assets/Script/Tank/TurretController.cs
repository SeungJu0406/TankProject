using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
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
            transform.Rotate(Vector3.left * x * rotateSpeed * Time.deltaTime);
        }
        if (y != 0)
        {
            transform.Rotate(Vector3.up * y * rotateSpeed * Time.deltaTime);
        }
    }
}
