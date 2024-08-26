using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] ObjectPool bulletPool;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform Point;
    [SerializeField] BulletType nowType;

    private void Update()
    {
        SelectBullet();
        Fire();
    }
    
    void Fire()
    {
        if (Input.GetButtonDown("Jump"))
        {
            PooledObject instance = bulletPool.GetPool(nowType, Point.position, Point.rotation);      
            Bullet bullet = instance.GetComponent<Bullet>();
            bullet.SetSpeed(bulletSpeed);
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
