using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour
{
    public EquipmentSlot[] equipmentSlots;
    public GameObject equipmentSlotPrefab;
    public GameObject equipmentItemPrefab;


    public float equipmentHP;
    public float equipmentAP;
    public float equipmentDP;
    public float equipmentAS;
    public float equipmentSpeed;

    public void RefreshStat()
    {
        StartCoroutine(RefreshStaus());
    }

    IEnumerator RefreshStaus()
    {
        yield return new WaitForSeconds(0.5f);
        equipmentHP = equipmentAP = equipmentDP = equipmentAS = equipmentSpeed = 0;
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].GetComponentInChildren<InventoryItem>() != null)
            {
                equipmentHP += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.MaxHpIncrese;
                equipmentAP += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.AttackPoint;
                equipmentDP += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.DefensePoint;
                equipmentAS += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.AttackSpeed;
                equipmentSpeed += equipmentSlots[i].GetComponentInChildren<InventoryItem>().item.MoveSpeed;
            }
        }
    }

}
