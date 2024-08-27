using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Red, Yellow, Black }
public class PooledObject : MonoBehaviour
{
    
    [HideInInspector]public ObjectPool parentPool;
    public BulletType bulletType;
    [SerializeField] float returnTime = 2;
    bool isBump;
    float curTime;

    private void OnEnable()
    {
        isBump = false;
        curTime = 0;
    }

    void Update()
    {
        if(isBump)
        {
            curTime += Time.deltaTime;

            if (curTime > returnTime)
            {
                parentPool.ReturnPool(this);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    { 
         isBump = true;
    }
}
