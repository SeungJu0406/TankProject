using UnityEngine;

public class Boom : MonoBehaviour
{
    [HideInInspector] public BoomPool returnPool;
    [SerializeField] float boomTime;
    [SerializeField] float boomSize;
    [SerializeField] int damage;
    float curTime;
    float scaleX, scaleY, scaleZ;
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
            returnPool.ReturnPool(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IHit target = other.transform.parent.GetComponent<IHit>(); // 해당 콜라이더의 부모를 찾아서 그 부모의 컴포넌트 사용
        if (target != null)
        {
           Debug.Log($"검사 후{other.gameObject.name}");
           target.Hit(damage);
        }
    }
}
