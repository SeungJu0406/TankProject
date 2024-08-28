using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] BoomPool boomPool;

    [SerializeField] Rigidbody rigidbody;

    [HideInInspector] public GrenadePool grenadePool;

    [Header("Gradade Statue")]
    [SerializeField] int damage;

    [SerializeField] float DelayTime;
    
    [SerializeField] float boomTime;

    [SerializeField] float boomSize;

    float curTime;

    bool isBump;

    private void OnEnable()
    {
        isBump = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        curTime = 0;
    }
    private void Update()
    {
        if (isBump)
        {
            curTime += Time.deltaTime;
            if (curTime > boomTime)
            {
                Boom();
            }
        }
        else
        {
            curTime += Time.deltaTime;
            if (curTime > boomTime * 3)
            {
                Boom();
            }
        }
    }

    void Boom()
    {
        Boom instance = boomPool.GetPool(transform.position, transform.rotation, boomTime, boomSize);
        if (instance == null) return;
        instance.SetDamage(damage);
        grenadePool.ReturnPool(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isBump = true;
        curTime = 0;
    }
}
