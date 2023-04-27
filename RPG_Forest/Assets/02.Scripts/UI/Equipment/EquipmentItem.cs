using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentItem : MonoBehaviour
{
    
    // ������Ʈ�� ���� �� ������ ����â ����
    private void Start()
    {
        Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
    }
    private void Update()
    {
        if(Application.isPlaying == false)
        {
            GameObject.Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if(Application.isPlaying == true)
        {
            Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
        }
    }



}
