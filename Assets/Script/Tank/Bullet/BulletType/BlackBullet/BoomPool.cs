using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPool : MonoBehaviour
{
    [SerializeField] Boom boom;
    [HideInInspector] Queue<Boom> booms;
    [SerializeField] int size;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        booms = new Queue<Boom>(size);
        for (int i = 0; i < size; i++) 
        {
            Boom instance = Instantiate(boom);
            instance.gameObject.SetActive(false);
            instance.transform.parent = transform;
            instance.boomPool = this;
            booms.Enqueue(instance);
        }
    }
    public Boom GetPool(Vector3 pos, Quaternion rot, float boomTime, float boomSize)
    {
        if (booms.Count > 0)
        {
            Boom instance = booms.Dequeue();
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.parent = null;
            instance.SetTime(boomTime);
            instance.SetSize(boomSize);
            instance.gameObject.SetActive(true);
            return instance;
        }
       return null;
    }

    public void ReturnPool(Boom returnBoom)
    {
        returnBoom.gameObject.transform.parent = transform;
        returnBoom.gameObject.SetActive(false);
        booms.Enqueue(returnBoom);
    }
}
