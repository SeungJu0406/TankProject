using UnityEngine;

public class CannonMode : MonoBehaviour
{
    enum Mode { TopView, Cannon }

    [SerializeField] Mode nowMode;
    [SerializeField] TankMover tankMover;
    [HideInInspector] Camera cam;
    [SerializeField] TurretController turret;
    [SerializeField] Transform CannonView;
    [SerializeField] float topViewPosX;
    [SerializeField] float topViewPosY;
    [SerializeField] float topViewPosZ;
    [SerializeField] float topViewRotX;
    [SerializeField] float topViewRotY;
    [SerializeField] float topViewRotZ;
    [SerializeField] float rotateSpeed;
    private void Awake()
    {
        nowMode = Mode.TopView;
        cam = Camera.main;
        ChangeTopView();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cannon Mode"))
        {
            if (nowMode == Mode.TopView)
            {
                ChangeCannon();
            }
            else if (nowMode == Mode.Cannon)
            {
                ChangeTopView();
            }
        }
        if (nowMode == Mode.Cannon)
        {
            CannonLook();
        }
    }
    void CannonLook()
    {
        float x = Input.GetAxis("Mouse X"); // 마우스 x축 움직임 량
        float y = Input.GetAxis("Mouse Y");

        turret.transform.Rotate(Vector3.up, rotateSpeed * x * Time.deltaTime);
        turret.transform.Rotate(Vector3.left, rotateSpeed * y * Time.deltaTime);
    }
    void ChangeCannon()
    {
        //움직임 불가, 카메라 터렛에 고정
        nowMode = Mode.Cannon;
        tankMover.enabled = false;
        turret.enabled = false;
        cam.transform.parent = transform;
        cam.transform.parent = turret.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cam.transform.position = CannonView.position;
        cam.transform.rotation = CannonView.rotation;
    }
    void ChangeTopView()
    {
        nowMode = Mode.TopView;
        tankMover.enabled = true;
        turret.enabled=true;
        turret.transform.parent = transform;
        cam.transform.parent = null;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        cam.transform.position = new Vector3(topViewPosX, topViewPosY, topViewPosZ);
        cam.transform.rotation = Quaternion.Euler(topViewRotX, topViewRotY, topViewRotZ);
    }
}
