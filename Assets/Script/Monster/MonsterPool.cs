using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] Monster monster;

    [HideInInspector] Stack<Monster> monsters;

    [SerializeField] int size;

    [SerializeField] float spawnPos;

    [SerializeField] float cycleTime;

    float curTime;
    private void Awake()
    {
       monsters = new Stack<Monster>(size);
        for (int i = 0; i < size; i++) 
        {
            Monster instance = Instantiate(monster);
            instance.gameObject.SetActive(false);
            instance.transform.parent = transform;
            instance.monsterPool = this;
            monsters.Push(instance);
        }
        curTime = cycleTime;
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > cycleTime) 
        {
            GetPool();
            curTime = 0;
        }
    }

    public Monster GetPool()
    {
        if(monsters.Count > 0)
        {
            Vector3 pos = new Vector3(
                Random.Range(transform.position.x-spawnPos, transform.position.x+spawnPos),
                0,
                Random.Range(transform.position.z- spawnPos, transform.position.x+ spawnPos));
            Quaternion rot = Quaternion.Euler(
                0,
                Random.Range(transform.rotation.y - spawnPos,transform.rotation.y + spawnPos),
                0);

            Monster instance = monsters.Pop();
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.parent = null;
            instance.gameObject.SetActive (true);
            return instance;
        }
        return null;
    }

    public void ReturnPool(Monster instance)
    {
        instance.transform.parent = transform;
        instance.gameObject.SetActive(false);
        monsters.Push(instance);
    }
}
