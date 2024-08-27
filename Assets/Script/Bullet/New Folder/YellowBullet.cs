using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet : Bullet
{
    [SerializeField] float maxFlyTime;
    float curFlyTime;

    private void OnEnable()
    {
        curFlyTime = 0;
    }
    private void Update()
    {
        CheckTime();
    }
    void CheckTime()
    {
        curFlyTime += Time.deltaTime;
        if (curFlyTime > maxFlyTime)
        {
            parentPool.ReturnPool(this);
        }
    }
}
