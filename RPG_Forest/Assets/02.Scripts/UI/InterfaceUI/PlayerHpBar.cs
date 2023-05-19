using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHpBar : MonoBehaviour
{
    public Slider mySlider;
    public PlayerController myPlayer;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            myPlayer = FindObjectOfType<PlayerController>();
            mySlider = transform.GetComponent<Slider>();
            mySlider.maxValue = myPlayer.MaxHp;
            mySlider.value = myPlayer.curHp;
            myPlayer.UpdateHp.RemoveAllListeners();
            myPlayer.UpdateHp.AddListener(RefreshHPBar);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
    public void RefreshHPBar(float hp)
    {
        mySlider.value = hp;
    }
}
