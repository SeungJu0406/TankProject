using UnityEngine;

public class Target : MonoBehaviour, IHit
{
    [SerializeField] public TargetPool targetPool;

    [SerializeField] int maxHp;

    [SerializeField] int curHp;
    private void OnEnable()
    {
        curHp = maxHp;
    }

    public void Hit(int damage)
    {
        curHp -= damage;
        if(curHp <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        targetPool.ReturnPool(this);
    }
}
