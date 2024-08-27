using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IHit
{
    [HideInInspector] public MonsterPool monsterPool;
    [SerializeField] Rigidbody rbMonster;
    [SerializeField] int maxHp;
    [SerializeField] int curHp;
    void OnEnable()
    {
        rbMonster.velocity = Vector3.zero;
        rbMonster.angularVelocity = Vector3.zero;
        curHp = maxHp;
    }
    public void Hit(int damage)
    {
        curHp -= damage;
        if(curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }
    void Die()
    {
        monsterPool.ReturnPool(this);
    }

}
