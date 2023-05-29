using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class PlayerHpBar : MonoBehaviour
{
    public Slider mySlider;
    public PlayerController myPlayer;
    public TMP_Text myText;
    public int myIndex;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            myPlayer = FindObjectOfType<PlayerController>();
            mySlider = transform.GetComponent<Slider>();
            mySlider.maxValue = myPlayer.MaxHp;
            mySlider.value = myPlayer.curHp;
            myText.text = myPlayer.curHp.ToString("F0")+$"/ {myPlayer.MaxHp}";
            myPlayer.UpdateHp.RemoveAllListeners();
            myPlayer.UpdateHp.AddListener(RefreshHPBar);
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
        myText.text = hp.ToString("F0") +$"/ {myPlayer.MaxHp}";
        mySlider.maxValue = myPlayer.MaxHp;
        Debug.Log($"Max = {myPlayer.MaxHp},Now = {hp}");
    }
}
