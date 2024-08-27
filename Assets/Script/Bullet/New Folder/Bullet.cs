using UnityEngine;

public enum BulletType { Red, Yellow, Black }
public class Bullet : MonoBehaviour
{
    [HideInInspector] public ObjectPool parentPool;
    public BulletType bulletType;
    [SerializeField] float returnTime;
    protected bool isBump;
    float curTime;

    [SerializeField] public Vector3 speed;
    [SerializeField] protected Rigidbody rigidbody;
    [SerializeField] protected int damage;
    protected bool isHit;
    protected bool isAttack;
    protected void OnEnable()
    {
        isBump = false;
        isAttack = false;
        curTime = 0;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;   
    }
    void Update()
    {
        CheckTime();
    }
    public void SetSpeed(Vector3 speed)
    {
        this.speed = speed;
    }
    void Fire()
    {
        rigidbody.AddForce(speed, ForceMode.Impulse); 
    }
    void CheckTime()
    {
        if (isBump)
        {
            curTime += Time.deltaTime;

            if (curTime > returnTime || isAttack)
            {
                isHit = true;
                parentPool.ReturnPool(this);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isAttack)
            return;
        isBump = true;       
        IHit target = collision.gameObject.GetComponent<IHit>();
        if (target != null) 
        {
            target.Hit(damage);
            isAttack = true;        
        }
    }
}
