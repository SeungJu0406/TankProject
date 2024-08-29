using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{
    [SerializeField] Target target;

    [SerializeField] Queue<Target> targets;

    [SerializeField] int size;



    private void Awake()
    {
        targets = new Queue<Target>(size);
        for (int i = 0; i < size; i++) 
        {
            Target instance = Instantiate(target);
            instance.gameObject.SetActive(false);
            instance.transform.parent = transform;
            instance.targetPool = this;
            targets.Enqueue(instance);
        }
    }

    public Target GetPool(Vector3 pos, Quaternion rot)
    {
        if(targets.Count > 0)
        {
            Target instance = targets.Dequeue();
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.parent = null; 
            instance.gameObject.SetActive (true);
            return instance;
        }
        return null;
    }

    public void ReturnPool(Target instance)
    {
        instance.transform.parent = transform;
        instance.gameObject.SetActive(false);
        targets.Enqueue(instance);
    }
}
