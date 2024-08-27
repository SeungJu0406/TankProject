using UnityEngine;

public class CannonMode : MonoBehaviour
{
    enum Mode { TopView, Cannon }

    [SerializeField] Mode nowMode;
    [SerializeField] TankMover tankMover;
    [HideInInspector] Camera cam;
    [SerializeField] float topViewPosX;
    [SerializeField] float topViewPosY;
    [SerializeField] float topViewPosZ;
    [SerializeField] float topViewRotX;
    [SerializeField] float topViewRotY;
    [SerializeField] float topViewRotZ;
    [SerializeField] TurretController turret;
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
    }
    void ChangeCannon()
    {
        //움직임 불가, 카메라 터렛에 고정
        nowMode = Mode.Cannon;
        tankMover.enabled = false;
        cam.transform.parent = transform;
        turret.transform.parent = cam.transform;
    }
    void ChangeTopView()
    {
        nowMode = Mode.TopView;
        tankMover.enabled = true;
        turret.transform.parent = transform;
        cam.transform.parent = null; 

        cam.transform.position = new Vector3(topViewPosX, topViewPosY, topViewPosZ);
        cam.transform.rotation = Quaternion.Euler(topViewRotX, topViewRotY, topViewRotZ);
    }
}
