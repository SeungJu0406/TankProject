using UnityEngine;

public class BlackBullet : Bullet
{
    [SerializeField] BoomPool boomPool;
    [SerializeField] float boomTime;
    [SerializeField] float boomSize;
    [SerializeField] int boomDamage;

    private void OnDisable()
    {
        if (isHit)
        {
            Boom boom = boomPool.GetPool(transform.position, transform.rotation, boomTime, boomSize);
            if (boom == null)
            {
                return;
            }
            boom.SetDamage(boomDamage);
        }
    }
} 
