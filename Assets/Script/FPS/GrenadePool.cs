using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePool : MonoBehaviour
{
    [SerializeField] Grenade grenade;

    [SerializeField] Queue<Grenade> grenades;

    [SerializeField] int size;

    private void Awake()
    {
        grenades = new Queue<Grenade>(size);
        for(int i = 0; i< size; i++)
        {
            Grenade instance = Instantiate(grenade);
            instance.gameObject.SetActive(false);
            instance.grenadePool = this;
            instance.transform.parent = transform;
            grenades.Enqueue(instance);
        }
    }

    public Grenade GetPool(Vector3 pos, Quaternion rot)
    {
        if(grenades.Count > 0)
        {
            Grenade instance = grenades.Dequeue();
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.parent = null;
            instance.gameObject.SetActive(true);
            return instance;
        }
        return null;
    }

    public void ReturnPool(Grenade instance) 
    {
        instance.gameObject.SetActive(false);
        instance.transform.parent = transform;
        grenades.Enqueue(instance);
    }
}
