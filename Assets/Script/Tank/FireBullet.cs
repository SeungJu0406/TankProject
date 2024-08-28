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

    [SerializeField] ChangeMode changeMode;
    private void Awake()
    {
        bulletSpeed = minBulletSpped;
    }
    private void Update()
    {
        SelectBullet();
        if (changeMode.curMode == TankMode.TopView )
        {
            TopViewFire();
        }
        else if(changeMode.curMode == TankMode.Cannon)
        {
            CannonFire();
        }
    }
    
    void TopViewFire()
    {
        if (Input.GetButton("Jump"))
        {
            bulletSpeed += 5 * Time.deltaTime;
            if (bulletSpeed > maxBulletSpeed)
                bulletSpeed = maxBulletSpeed;
        }
        if (Input.GetButtonUp("Jump"))
        {
            Bullet bullet = bulletPool.GetPool(nowType, Point.position, Point.rotation);
            if (bullet == null)
                return;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bullet.SetSpeed(Point.forward * bulletSpeed);
            bulletRb.AddForce(bullet.speed, ForceMode.Impulse);
            bulletRb.useGravity = true;
            bulletSpeed = minBulletSpped;
        }
    }

    void CannonFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.DrawRay(Point.position, Point.forward * 100f, Color.red);
            nowType = BulletType.Black;
            int laymast = 1 << LayerMask.NameToLayer("Monster");
            
            if(Physics.Raycast(Point.position, Point.forward,100f, laymast))
            {
                Bullet bullet = bulletPool.GetPool(nowType, Point.position, Point.rotation);
                if (bullet == null) return;
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.velocity = Point.forward * bulletSpeed*3;
                bulletRb.useGravity = false;
            }
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
