using UnityEngine;

public enum BulletType { Red, Yellow, Black }
public class Bullet : MonoBehaviour
{
    [HideInInspector] public ObjectPool parentPool;
    public BulletType bulletType;
    [SerializeField] float returnTime;
    bool isBump;
    float curTime;

    [SerializeField] public Vector3 speed;
    [SerializeField] protected Rigidbody rigidbody;
    [SerializeField] protected int damage;
    protected bool isHit;
    bool isAttack;
    private void OnEnable()
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
    public void SetIsHit(bool isHit)
    {
        this.isHit = isHit;
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

            if (curTime > returnTime)
            {
                SetIsHit(true);
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
            parentPool.ReturnPool(this);
            target.Hit(damage); 
            isAttack = true;
        }
    }
}
