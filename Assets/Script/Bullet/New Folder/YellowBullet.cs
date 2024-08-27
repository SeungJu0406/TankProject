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
        CheckTime();
    }
    void CheckTime()
    {
        curTime += Time.deltaTime;
        if (curTime > maxTime)
        {
            pooledObject.parentPool.ReturnPool(pooledObject);
        }
    }
}
