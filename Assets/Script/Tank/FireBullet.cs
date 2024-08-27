using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] ObjectPool bulletPool;
    float bulletSpeed;
    [SerializeField] float minBulletSpped;
    [SerializeField] float maxBulletSpeed;
    [SerializeField] Transform Point;
    [SerializeField] BulletType nowType;
    private void Awake()
    {
        bulletSpeed = minBulletSpped;
    }
    private void Update()
    {
        SelectBullet();
        Fire();
    }
    
    void Fire()
    {
        if (Input.GetButton("Jump"))
        {
            bulletSpeed += 5 * Time.deltaTime;
            if (bulletSpeed > maxBulletSpeed)
                bulletSpeed = maxBulletSpeed;
        }
        if (Input.GetButtonUp("Jump"))
        {
            PooledObject instance = bulletPool.GetPool(nowType, Point.position, Point.rotation);
            if (instance == null)
                return;
            Bullet bullet = instance.GetComponent<Bullet>();
            Rigidbody rbBullet = instance.GetComponent<Rigidbody>();
            bullet.SetSpeed(Point.forward * bulletSpeed);
            rbBullet.AddForce(bullet.speed, ForceMode.Impulse);
            bulletSpeed = minBulletSpped;
        }
    }
    void SelectBullet()
    {
        if (Input.GetButtonDown("SelectRed"))
        {
            nowType = BulletType.Red;
        }
        else if (Input.GetButtonDown("SelectYellow"))
        {
            nowType = BulletType.Yellow;
        }
        else if (Input.GetButtonDown("SelectBlack"))
        {
            nowType = BulletType.Black;
        }
    }
}
