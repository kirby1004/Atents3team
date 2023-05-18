using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public Dragon myDragon = null;
    public Slider mySlider = null;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<Dragon>()!=null)
        {
            myDragon = FindObjectOfType<Dragon>();
        }
        else
        {
            Destroy(transform.gameObject);
        }
        mySlider = transform.GetComponent<Slider>();
        mySlider.maxValue = myDragon.MaxHp;
        mySlider.value = myDragon.curHp;
        myDragon.UpdateHp.RemoveAllListeners();
        myDragon.UpdateHp.AddListener(RefreshHPBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshHPBar(float hp)
    {
        mySlider.value = hp;
    }

}
