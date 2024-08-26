using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PooledObject redBullet;
    [SerializeField] PooledObject yellowBullet;
    [SerializeField] PooledObject blackBullet;
    [SerializeField] Transform redPool;
    [SerializeField] Transform yellowPool;
    [SerializeField] Transform blackPool;
    Stack<PooledObject> redBullets;
    Stack<PooledObject> yellowBullets;
    Stack<PooledObject> blackBullets;
    [SerializeField] bool isInfinityBullet;
    [SerializeField] int size;

    BulletType bulletType;
    
    

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        redBullets = new Stack<PooledObject>(size);
        yellowBullets = new Stack<PooledObject>(size);
        blackBullets = new Stack<PooledObject>(size);

        for (int i = 0; i < size; i++)
        {
            PooledObject red = Instantiate(redBullet);
            red.gameObject.SetActive(false);
            red.bulletType = BulletType.Red;
            red.transform.parent = redPool;
            red.parentPool = this;
            redBullets.Push(red);
        }
        for (int i = 0; i < size; i++)
        {
            PooledObject yellow = Instantiate(yellowBullet);
            yellow.gameObject.SetActive(false);
            yellow.bulletType = BulletType.Yellow;
            yellow.transform.parent = yellowPool;
            yellow.parentPool = this;
            yellowBullets.Push(yellow);
        }
        for (int i = 0; i < size; i++)
        {
            PooledObject black = Instantiate(blackBullet);
            black.gameObject.SetActive(false);
            black.bulletType = BulletType.Black;
            black.transform.parent = blackPool;
            black.parentPool = this;
            blackBullets.Push(black);
        }

        bulletType = BulletType.Red;
    }

    public PooledObject GetPool(BulletType type,Vector3 pos, Quaternion rot)
    {
        PooledObject bullet = null;
        bulletType = type;
        switch (bulletType)
        {
            case BulletType.Red:
                if(redBullets.Count > 0)
                {
                    bullet = redBullets.Pop();
                    bullet.transform.position = pos;
                    bullet.transform.rotation = rot;
                    bullet.transform.parent = null;
                    bullet.gameObject.SetActive(true);
                }
                else if(isInfinityBullet)
                {
                    bullet = Instantiate(redBullet, pos, rot);
                    bullet.bulletType = BulletType.Red;
                    bullet.parentPool = this;               
                }
                break;
            case BulletType.Yellow:
                if (yellowBullets.Count > 0)
                {
                    bullet = yellowBullets.Pop();
                    bullet.transform.position = pos;
                    bullet.transform.rotation = rot;
                    bullet.transform.parent = null;
                    bullet.gameObject.SetActive(true);
                }
                else if(isInfinityBullet)
                {
                    bullet = Instantiate(yellowBullet, pos, rot);
                    bullet.bulletType = BulletType.Yellow;
                    bullet.parentPool = this;
                }
                break;
            case BulletType.Black:
                if (blackBullets.Count > 0)
                {
                    bullet = blackBullets.Pop();
                    bullet.transform.position = pos;
                    bullet.transform.rotation = rot;
                    bullet.transform.parent = null;
                    bullet.gameObject.SetActive(true);
                }
                else if(isInfinityBullet)
                {
                    bullet = Instantiate(blackBullet, pos, rot);
                    bullet.bulletType = BulletType.Black;
                    bullet.parentPool = this;

                }
                break;
            default:
                break;
        }
        return bullet;
    }

    public void ReturnPool(PooledObject returnBullet)
    {
        returnBullet.gameObject.SetActive(false);
        if(returnBullet.bulletType == BulletType.Red)
        {
            returnBullet.transform.parent = redPool;
            redBullets.Push(returnBullet);
        }
        else if(returnBullet.bulletType == BulletType.Yellow)
        {
            returnBullet.transform.parent = yellowPool;
            yellowBullets.Push(returnBullet);
        }
        else if (returnBullet.bulletType == BulletType.Black)
        {
            returnBullet.transform.parent = blackPool;
            blackBullets.Push(returnBullet);
        }
    }
}
