using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] TargetPool targetPool;

    [Header("Target Spawner")]
    [SerializeField] float spawnTime;

    [SerializeField] int spawnRange;

    float curTime;

    private void Awake()
    {
        curTime = 0;


    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > spawnTime)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange),
                Random.Range(transform.position.y - spawnRange, transform.position.y + spawnRange),
                transform.position.z);
            Target instance = targetPool.GetPool(spawnPos, transform.rotation);
            curTime = 0;
        }
    }
}
