using System.Collections;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public enum Mode { Bullet, Grenade }

    [SerializeField] Transform muzzlePoint;

    [SerializeField] GrenadePool grenadePool;

    [SerializeField] GameObject gun;

    [SerializeField] GameObject grenade;

    [Header("Player Statue")]
    [SerializeField] float moveSpeed;

    [SerializeField] float rotateSpeed;

    [SerializeField] public Mode curMode;

    [Header("Gun Status")]
    [SerializeField] float range;

    [SerializeField] int damage;

    [SerializeField] float attackSpeed;

    [SerializeField] public int maxBulletCount;

    [SerializeField] public int curBulletCount;

    [SerializeField] float reloadTime;

    bool isFireAble;

    [HideInInspector] public bool isReload;

    [Header("Grenade Statue")]
    [SerializeField] float maxThrowPower;

    [SerializeField] float minThrowPower;

    float curThrowPower;

    [SerializeField] float maxChargeTime;

    float chargeTime;

    int layerMask;

    Vector3 moveDir;

    Vector3 rotateDir;

    Coroutine gunShooter;

    Coroutine reloader;

    Coroutine granadeCharger; 

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Monster");
        rotateSpeed *= 10;
        transform.eulerAngles = Vector3.zero;

        curBulletCount = maxBulletCount;
        isFireAble = true;

        chargeTime = (maxThrowPower - minThrowPower) / maxChargeTime;
        curThrowPower = minThrowPower;

        curMode = Mode.Bullet;
        grenade.SetActive(false);

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
        ChangeMode();
        switch (curMode)
        {
            case Mode.Bullet:
                Fire();
                break;
            case Mode.Grenade:
                ThrowGrenade();
                break;
            default:
                break;
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(x, 0, z);
        moveDir.Normalize();

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }


    void Rotate()
    {
        rotateDir.x = Input.GetAxisRaw("Mouse X");
        rotateDir.y = Input.GetAxisRaw("Mouse Y");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        transform.Rotate(Vector3.up * rotateDir.x * rotateSpeed * Time.deltaTime, Space.World);
        muzzlePoint.transform.Rotate(Vector3.left * rotateDir.y * rotateSpeed * Time.deltaTime);
    }

    void Fire()
    {
        Reload();
        if (!isFireAble)
        {
            if (gunShooter != null)
            {
                StopCoroutine(gunShooter);
                gunShooter = null;
            }
            return;
        }       
        if (Input.GetButton("Fire1"))
        {          
            if (gunShooter == null) 
            {
                gunShooter = StartCoroutine(ShootBullet());
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(gunShooter);
            gunShooter = null;
        }
        if (curBulletCount <= 0)
        {
            Debug.Log("총알이 없습니다. R을 눌러 장전하세요");
            isFireAble = false;
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReload)
            {
                reloader = StartCoroutine(ReloadBullet());
                isFireAble = false;
            }
        }
    }

    IEnumerator ShootBullet()
    {
        WaitForSeconds delay = new WaitForSeconds(attackSpeed);
        while (true)
        {
            if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, range, layerMask))
            {
                IHit monster = hit.transform.GetComponent<IHit>();
                monster.Hit(damage);
                curBulletCount--;
            }
            yield return delay;
        }
    }

    IEnumerator ReloadBullet()
    {
        WaitForSeconds delay = new WaitForSeconds(reloadTime);
        isReload = true;
        yield return delay;
        curBulletCount = maxBulletCount;
        isFireAble = true;
        isReload = false ;
    }
    void ThrowGrenade()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            granadeCharger = StartCoroutine(ChargeGrenade());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(granadeCharger);
            Grenade grenade = grenadePool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
            if (grenade == null) return;
            Rigidbody grenadeRb = grenade.GetComponent<Rigidbody>();
            grenadeRb.AddForce(muzzlePoint.forward * curThrowPower, ForceMode.Impulse);
            curThrowPower = minThrowPower;
        }
    }

    IEnumerator ChargeGrenade()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            curThrowPower += chargeTime / 10;
            yield return delay;
        }
    }

    void ChangeMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            curMode = Mode.Bullet;
            gun.SetActive(true);
            grenade.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            curMode = Mode.Grenade;
            grenade.SetActive(true);
            gun.SetActive(false);
        }
    }


}
