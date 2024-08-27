using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] Bullet redBullet;
    [SerializeField] Bullet yellowBullet;
    [SerializeField] Bullet blackBullet;
    [SerializeField] Transform redPool;
    [SerializeField] Transform yellowPool;
    [SerializeField] Transform blackPool;
    Queue<Bullet> redBullets;
    Queue<Bullet> yellowBullets;
    Queue<Bullet> blackBullets;
    [SerializeField] bool isInfinityBullet;
    [SerializeField] int size;

    BulletType bulletType;
    
    

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        redBullets = new Queue<Bullet>(size);
        yellowBullets = new Queue<Bullet>(size);
        blackBullets = new Queue<Bullet>(size);

        for (int i = 0; i < size; i++)
        {
            Bullet red = Instantiate(redBullet);
            red.gameObject.SetActive(false);
            red.bulletType = BulletType.Red;
            red.transform.parent = redPool;
            red.parentPool = this;
            redBullets.Enqueue(red);
        }
        for (int i = 0; i < size; i++)
        {
            Bullet yellow = Instantiate(yellowBullet);
            yellow.gameObject.SetActive(false);
            yellow.bulletType = BulletType.Yellow;
            yellow.transform.parent = yellowPool;
            yellow.parentPool = this;
            yellowBullets.Enqueue(yellow);
        }
        for (int i = 0; i < size; i++)
        {
            Bullet black = Instantiate(blackBullet);
            black.gameObject.SetActive(false);
            black.bulletType = BulletType.Black;
            black.transform.parent = blackPool;
            black.parentPool = this;
            blackBullets.Enqueue(black);
        }

        bulletType = BulletType.Red;
    }
    public Bullet GetPool(BulletType type,Vector3 pos, Quaternion rot)
    {
        Bullet bullet = null;
        bulletType = type;
        switch (bulletType)
        {
            case BulletType.Red:
                if(redBullets.Count > 0)
                {
                    bullet = redBullets.Dequeue();
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
                    bullet = yellowBullets.Dequeue();
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
                    bullet = blackBullets.Dequeue();
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

    public void ReturnPool(Bullet returnBullet)
    {
        returnBullet.gameObject.SetActive(false);
        if(returnBullet.bulletType == BulletType.Red)
        {
            returnBullet.transform.parent = redPool;
            redBullets.Enqueue(returnBullet);
        }
        else if(returnBullet.bulletType == BulletType.Yellow)
        {
            returnBullet.transform.parent = yellowPool;
            yellowBullets.Enqueue(returnBullet);
        }
        else if (returnBullet.bulletType == BulletType.Black)
        {
            returnBullet.transform.parent = blackPool;
            blackBullets.Enqueue(returnBullet);
        }
    }
}
