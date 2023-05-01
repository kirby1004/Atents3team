using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour
{
    // 장비창 슬롯
    public List<EquipmentSlot> equipslot = new();
    // 장비 슬롯 , 아이템 원본
    public GameObject equipmentSlotPrefab;
    public GameObject equipmentItemPrefab;

    // 장착한 장비의 스텟 합 상수
    public float equipmentHP;
    public float equipmentAP;
    public float equipmentDP;
    public float equipmentAS;
    public float equipmentSpeed;


    // 스텟 갱신 코루틴 작동
    public void RefreshStat()
    {
        StartCoroutine(RefreshStsus());
    }

    //스텟 갱신 코루틴
    IEnumerator RefreshStsus()
    {
        yield return new WaitForEndOfFrame();
        //스텟 초기화 후 합산
        equipmentHP = equipmentAP = equipmentAS = equipmentSpeed = equipmentDP = 0; // 모든스텟 0으로 초기화
        for(int i = 0;i < equipslot.Count;i++)
        {
            // 장착중인 장비의 스텟 합산
            if (equipslot[i].GetComponentInChildren<InventoryItem>() != null)
            {
                equipmentHP += equipslot[i].GetComponentInChildren<InventoryItem>().item.MaxHpIncrese;  // 체력
                equipmentAP += equipslot[i].GetComponentInChildren<InventoryItem>().item.AttackPoint;   //공격력
                equipmentDP += equipslot[i].GetComponentInChildren<InventoryItem>().item.DefensePoint;  //방어력
                equipmentAS += equipslot[i].GetComponentInChildren<InventoryItem>().item.AttackSpeed;   //공격속도
                equipmentSpeed += equipslot[i].GetComponentInChildren<InventoryItem>().item.MoveSpeed;  //이동속도
            }
        }
    }

}
