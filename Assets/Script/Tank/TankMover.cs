using UnityEngine;

public class TankMover : MonoBehaviour
{
    [SerializeField] ChangeMode changeMode;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;


    private void Update()
    {
        if (changeMode.curMode == TankMode.TopView)
        {
            Move();
        }
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, 0, z);
      
        if (x != 0)
        {
            transform.Translate(Vector3.right * x * moveSpeed * Time.deltaTime, Space.World);
        }
        if (z != 0)
        {
            transform.Translate(Vector3.forward * z * moveSpeed * Time.deltaTime, Space.World);
        }
        if (x != 0 || z != 0)
        {
            Quaternion rotateDir = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotateDir, rotateSpeed * Time.deltaTime);
        }
    }
}
