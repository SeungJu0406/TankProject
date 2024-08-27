using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IHit
{
    [HideInInspector] public MonsterPool monsterPool;
    [SerializeField] int maxHp;
    [SerializeField] int curHp;
    void OnEnable()
    {
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
