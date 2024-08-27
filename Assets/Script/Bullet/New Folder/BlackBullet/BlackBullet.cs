using UnityEngine;

public class BlackBullet : Bullet
{
    [SerializeField] BoomPool boomPool;
    [SerializeField] float boomTime;
    [SerializeField] float boomSize;
    private void OnDisable()
    {
        Boom boom = boomPool.GetPool(transform.position, transform.rotation, boomTime, boomSize);
    }
} 
