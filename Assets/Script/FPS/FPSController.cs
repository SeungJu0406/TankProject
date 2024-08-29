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

    [SerializeField] Mode curMode;

    [Header("Gun Status")]
    [SerializeField] float range;

    [SerializeField] int damage;

    [SerializeField] float attackSpeed;

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

    Coroutine granadeCharger; 

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Monster");
        rotateSpeed *= 10;
        transform.eulerAngles = Vector3.zero;

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
                FireBullet();
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

    void FireBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gunShooter = StartCoroutine(ShootBullet());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(gunShooter);
            gunShooter = null;
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
            }
            yield return delay;
        }
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
            granadeCharger = null;
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
        if (Input.GetButtonDown("Fire Bullet"))
        {
            curMode = Mode.Bullet;
            gun.SetActive(true);
            grenade.SetActive(false);
        }
        else if (Input.GetButtonDown("Throw Grenade"))
        {
            curMode = Mode.Grenade;
            grenade.SetActive(true);
            gun.SetActive(false);
        }
    }


}
