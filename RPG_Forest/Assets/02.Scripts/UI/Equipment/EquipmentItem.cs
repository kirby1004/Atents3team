using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentItem : MonoBehaviour , IItems
{
    public Component myState
    {
        get => this as Component;
    }

    // ������� ����� ����â ����
    private void Start()
    {
        transform.parent.GetComponent<Slot>().mySlotItems = transform;
        EquipmentManager.Inst.RefreshStat(transform.gameObject.GetComponent<Item>().item.MaxHpIncrese); // ��� �Ŵ����� ���ݰ��� ����
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
    
    //������� ������ ���� ����
    private void OnDestroy()
    {
        if (!isQuit)
        {
            //Gamemanager.Inst.myPlayer.curHp -= transform.gameObject.GetComponent<Item>().item.MaxHpIncrese;
            transform.parent.GetComponent<Slot>().mySlotItems = null;
            EquipmentManager.Inst.RefreshStat(-transform.gameObject.GetComponent<Item>().item.MaxHpIncrese); // ��� �Ŵ����� ���Ȱ��� ����
        }       
    }
}
