using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentItem : MonoBehaviour , IItems
{
    public Component myState
    {
        get => this as Component;
    }

    // 착용상태 적용시 스텟창 갱신
    private void Start()
    {
        transform.parent.GetComponent<Slot>().mySlotItems = transform;
        EquipmentManager.Inst.RefreshStat(transform.gameObject.GetComponent<Item>().item.MaxHpIncrese); // 장비 매니저의 스텟갱신 동작
        //Gamemanager.Inst.myPlayer.curHp += transform.gameObject.GetComponent<Item>().item.MaxHpIncrese;
    }
    private void Update()
    {        
    }

    // 
    bool isQuit = false;
    private void OnApplicationQuit()
    {
        isQuit = true;
    }
    
    //착용상태 해제시 스텟 갱신
    private void OnDestroy()
    {
        if (!isQuit)
        {
            //Gamemanager.Inst.myPlayer.curHp -= transform.gameObject.GetComponent<Item>().item.MaxHpIncrese;
            transform.parent.GetComponent<Slot>().mySlotItems = null;
            EquipmentManager.Inst.RefreshStat(-transform.gameObject.GetComponent<Item>().item.MaxHpIncrese); // 장비 매니저의 스탯갱신 동작
        }       
    }
}
