using UnityEngine;

public enum BulletType { Red, Yellow, Black }
public class Bullet : MonoBehaviour
{
    [HideInInspector] public ObjectPool parentPool;
    public BulletType bulletType;
    [SerializeField] float returnTime = 2;
    bool isBump;
    float curTime;

    [SerializeField] public Vector3 speed;
    [SerializeField] protected Rigidbody rigidbody;
    [SerializeField] protected int damage;

    protected bool isHit;
    private void OnEnable()
    {
        isBump = false;
        curTime = 0;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;   
    }
    void Update()
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
    void Fire()
    {
        rigidbody.AddForce(speed, ForceMode.Impulse); 
    }
    public void SetSpeed(Vector3 speed)
    {
        this.speed = speed;
    }
    public void SetIsHit(bool isHit)
    {
        this.isHit = isHit;
    }
    private void OnCollisionEnter(Collision collision)
    {
        isBump = true;
    }
}
