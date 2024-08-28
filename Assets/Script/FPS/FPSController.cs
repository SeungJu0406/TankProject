using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] Transform muzzlePoint;

    [Header("Player Statue")]
    [SerializeField] float moveSpeed;

    [SerializeField] float rotateSpeed;
    
    [Header("Gun Status")]
    [SerializeField] float range;

    [SerializeField] int damage;

    int layerMask;

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Monster");
        rotateSpeed *= 10;
        transform.eulerAngles = Vector3.zero;
    }
    private void Start()
    {
        Camera.main.transform.position = muzzlePoint.position;
        Camera.main.transform.rotation = muzzlePoint.rotation;
        Camera.main.transform.parent = muzzlePoint;
    }
    private void Update()
    {
        Move();
        Rotate();
        Fire();
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3 (x, 0, z);
        moveDir.Normalize();

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);


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
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, range, layerMask))
            {
                IHit monster = hit.transform.GetComponent<IHit>();
                monster.Hit(damage);
            }
        }
    }

 
}
