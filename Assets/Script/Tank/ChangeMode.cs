using UnityEngine;

public enum TankMode { TopView, Cannon }
public class ChangeMode : MonoBehaviour
{
    

    [SerializeField] public TankMode curMode;
    [HideInInspector] Camera cam;
    [SerializeField] TurretController turret;
    [SerializeField] Transform CannonView;
    [SerializeField] float topViewPosX;
    [SerializeField] float topViewPosY;
    [SerializeField] float topViewPosZ;
    [SerializeField] float topViewRotX;
    [SerializeField] float topViewRotY;
    [SerializeField] float topViewRotZ;

    [SerializeField] GameObject aimUI;
    private void Awake()
    {
        curMode = TankMode.TopView;
        cam = Camera.main;
        ChangeTopView();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cannon Mode"))
        {
            if (curMode == TankMode.TopView)
            {
                ChangeCannon();
            }
            else if (curMode == TankMode.Cannon)
            {
                ChangeTopView();
            }
        }
    }
    void ChangeCannon()
    {
        curMode = TankMode.Cannon;
        aimUI.SetActive(true);

        cam.transform.parent = transform;
        cam.transform.parent = turret.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cam.transform.position = CannonView.position;
        cam.transform.rotation = CannonView.rotation;
    }
    void ChangeTopView()
    {
        curMode = TankMode.TopView;
        aimUI.SetActive(false);

        turret.transform.parent = transform;
        cam.transform.parent = null;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        cam.transform.position = new Vector3(topViewPosX, topViewPosY, topViewPosZ);
        cam.transform.rotation = Quaternion.Euler(topViewRotX, topViewRotY, topViewRotZ);
    }
}
