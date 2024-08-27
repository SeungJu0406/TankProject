using UnityEngine;

public class ChangeMode : MonoBehaviour
{
    enum Mode { TopView, Cannon }

    [SerializeField] Mode nowMode;
    [SerializeField] TankMover tankMover;
    [HideInInspector] Camera cam;
    [SerializeField] TurretController turret;
    [SerializeField] Transform CannonView;
    [SerializeField] CannonMode cannonMode;
    [SerializeField] float topViewPosX;
    [SerializeField] float topViewPosY;
    [SerializeField] float topViewPosZ;
    [SerializeField] float topViewRotX;
    [SerializeField] float topViewRotY;
    [SerializeField] float topViewRotZ;
    
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
        nowMode = Mode.Cannon;
        tankMover.enabled = false;
        turret.enabled = false;

        cannonMode.enabled = true;

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

        cannonMode.enabled = false;
        
        turret.transform.parent = transform;
        cam.transform.parent = null;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        cam.transform.position = new Vector3(topViewPosX, topViewPosY, topViewPosZ);
        cam.transform.rotation = Quaternion.Euler(topViewRotX, topViewRotY, topViewRotZ);
    }
}
