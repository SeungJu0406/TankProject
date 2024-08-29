using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] public BoomPool boomPool;
    [SerializeField] float boomTime;
    [SerializeField] float boomSize;
    [SerializeField] int damage;   
    float curTime;
    float scaleX, scaleY, scaleZ;
    bool isHitBoom;
    private void OnEnable()
    {
        scaleX = boomSize;
        scaleY = boomSize;
        scaleZ = boomSize;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        curTime = 0;
    }
    void Update()
    {
        PrintBoom();
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public void SetTime(float boomTime)
    {
        this.boomTime = boomTime;
    }
    public void SetSize(float boomSize)
    {
        this.boomSize = boomSize;
    }
    void PrintBoom()
    {
        curTime += Time.deltaTime;
        scaleX -= boomSize / boomTime * Time.deltaTime;
        scaleY -= boomSize / boomTime * Time.deltaTime;
        scaleZ -= boomSize / boomTime * Time.deltaTime;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        if (curTime > boomTime)
        {
            boomPool.ReturnPool(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IHit target = other.gameObject.GetComponent<IHit>();
        if (target != null)
        {
            target.Hit(damage);
        }
    }
}
