using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text bulletUI;

    [SerializeField] FPSController player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<FPSController>();
    }

    private void Update()
    {
        switch (player.curMode) 
        {
            case FPSController.Mode.Bullet:
                PrintBulletMode();
                break;
            case FPSController.Mode.Grenade:
                PrintGranadeMode();
                break;
        }

    }

    void PrintBulletMode()
    {
        if (player.curBulletCount == 0)
        {
            bulletUI.color = Color.red;
        }
        else
        {
            bulletUI.color = Color.black;
        }
        if (player.isReload) 
        {
            bulletUI.text = $"{player.curBulletCount}/{player.maxBulletCount}\n ¿Â¿¸¡ﬂ";
        }
        else
        {
            bulletUI.text = $"{player.curBulletCount}/{player.maxBulletCount}";
        }
    }

    void PrintGranadeMode()
    {
        bulletUI.text = "";
    }
}
