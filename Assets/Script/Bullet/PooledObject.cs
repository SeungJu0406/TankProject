using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Red, Yellow, Black }
public class PooledObject : MonoBehaviour
{
    
    public ObjectPool parentPool;
    public BulletType bulletType;
    [SerializeField] float returnTime;

    float curTime;

    private void OnEnable()
    {
        curTime = 0;
    }

    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime > returnTime) 
        {
            parentPool.ReturnPool(this);
        }
    }
}
