using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public Dragon myDragon = null;
    public Slider mySlider = null;
    public TMP_Text myText = null;

    public int myIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<Dragon>()!=null)
        {
            myDragon = FindObjectOfType<Dragon>();
            mySlider = transform.GetComponent<Slider>();
            mySlider.maxValue = myDragon.MaxHp;
            mySlider.value = myDragon.curHp;
            myDragon.UpdateHp.RemoveAllListeners();
            myDragon.UpdateHp.AddListener(RefreshHPBar);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshHPBar(float hp)
    {
        mySlider.value = hp;
        myText.text =  ((hp/myDragon.MaxHp)*100).ToString("F1")+ "%";
    }
    public float RefreshHPBar(float hp, float start , float end)
    {
        float curMax = end - start;
        float curValue = hp - start;
        float temp = 0;
        if(curValue < 0)
        {

        }

        return 0;
    }

}
