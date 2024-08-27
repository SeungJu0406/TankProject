using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet : Bullet
{
    [SerializeField] PooledObject pooledObject;
    [SerializeField] float maxTime;
    float curTime;
    private void OnEnable()
    {
        curTime = 0;
    }
    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > maxTime) 
        {
            pooledObject.parentPool.ReturnPool(pooledObject);
        }
    }
}
