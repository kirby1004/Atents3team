using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour
{
    public List<EquipmentSlot> equipslot = new List<EquipmentSlot>();
    public GameObject equipmentSlotPrefab;
    public GameObject equipmentItemPrefab;

    //public UnityAction StatusRefreshing;
    public float equipmentHP;
    public float equipmentAP;
    public float equipmentDP;
    public float equipmentAS;
    public float equipmentSpeed;

    public void RefreshStat()
    {
        //StatusRefreshing += Gamemanager.instance.myUIManager.statusManager.RefreshingStat;
        StartCoroutine(RefreshStsusTest());
        //StartCoroutine(RefreshStaus());
    }

    //IEnumerator RefreshStaus()
    //{
    //    yield return new WaitForEndOfFrame();
    //    equipmentHP = equipmentAP = equipmentDP = equipmentAS = equipmentSpeed = 0;
    //    for (int i = 0; i < equipmentSlots.Length; i++)
    //    {
    //        if (equipmentSlots[i].GetComponentInChildren<InventoryItem>() != null)
    //        {
    //            equipmentHP += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.MaxHpIncrese;
    //            equipmentAP += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.AttackPoint;
    //            equipmentDP += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.DefensePoint;
    //            equipmentAS += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.AttackSpeed;
    //            equipmentSpeed += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.MoveSpeed;
    //        }
    //    }
    //}
    IEnumerator RefreshStsusTest()
    {
        yield return new WaitForEndOfFrame();
        equipmentHP = equipmentAP = equipmentAS = equipmentSpeed = equipmentDP = 0;
        for(int i = 0;i < equipslot.Count;i++)
        {
            if (equipslot[i].GetComponentInChildren<InventoryItem>() != null)
            {
                equipmentHP += equipslot[i].GetComponentInChildren<InventoryItem>().item.MaxHpIncrese;
                equipmentAP += equipslot[i].GetComponentInChildren<InventoryItem>().item.AttackPoint;
                equipmentDP += equipslot[i].GetComponentInChildren<InventoryItem>().item.DefensePoint;
                equipmentAS += equipslot[i].GetComponentInChildren<InventoryItem>().item.AttackSpeed;
                equipmentSpeed += equipslot[i].GetComponentInChildren<InventoryItem>().item.MoveSpeed;
            }
        }
    }

}
