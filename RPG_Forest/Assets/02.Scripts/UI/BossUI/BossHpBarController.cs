using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBarController : MonoBehaviour
{
    public List<BossHpBar> bossHpBars = new List<BossHpBar>();

    public Image[] myColorImage;
    public Dragon myDragon = null;

    private void Start()
    {
        if (FindObjectOfType<Dragon>() != null)
        {
            myDragon = FindObjectOfType<Dragon>();
            //myDragon.UpdateHp.RemoveAllListeners();
            //myDragon.UpdateHp.AddListener(RefreshHpBars);
        }
    }

    public void RefreshHpBars(float value)
    {

        
    }




}
