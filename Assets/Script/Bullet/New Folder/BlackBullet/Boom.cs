using UnityEngine;

public class Boom : MonoBehaviour
{
    [HideInInspector] public BoomPool returnPool;
    [SerializeField] float boomTime;
    [SerializeField] float boomSize;
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

}
