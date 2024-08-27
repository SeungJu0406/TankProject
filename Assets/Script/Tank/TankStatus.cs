using UnityEngine;

public class TankStatus : MonoBehaviour
{
    [SerializeField] int maxHp;

    [SerializeField] int curHp;

    [SerializeField] float maxHitTime;

    [SerializeField] float curHitTime;

    private void Awake()
    {
        curHp = maxHp;
        curHitTime = 0;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Monster>() == null)
        {
            return;
        }
        curHitTime += Time.deltaTime;
        if (curHitTime > maxHitTime)
        {
            Hit();
            curHitTime = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Monster>() == null)
        {
            return;
        }
        curHitTime = 0;
    }
    void Hit()
    {
        curHp--;
        if (curHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
