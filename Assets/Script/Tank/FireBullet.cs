using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] ObjectPool bulletPool;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform Point;

    private void Update()
    {
        Fire();
    }
    
    void Fire()
    {
        if (Input.GetButtonDown("Jump"))
        {
            PooledObject instance = bulletPool.GetPool(Point.position, Point.rotation);
            if (instance != null)
                return;
            Bullet bullet = instance.GetComponent<Bullet>();
            bullet.SetSpeed(bulletSpeed);
        }
    }
}
