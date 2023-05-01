using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour , IDropHandler
{

    // 아이템을 드래그 후 드랍 햇을때
    public void OnDrop(PointerEventData eventData)
    {
        // 슬롯에 아이템이 없을때는 그 슬롯으로 이동
        if (transform.GetComponentInChildren<InventoryItem>() == null)
        {

            InventoryItem inventorySlotItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventorySlotItem.parentAfterDrag = transform;
            if (inventorySlotItem.GetComponent<EquipmentItem>() != null)
            {
                Destroy(inventorySlotItem.GetComponent<EquipmentItem>());
            }
        }
        // 슬롯에 아이템이 존재할때의 동작 
        // 1. 마우스로 드랍한 아이템이 장착중인아이템일때 타입비교 후 
        //

        else if (transform.GetComponentInChildren<InventoryItem>() != null)
        {
            InventoryItem mySlotItem = transform.GetComponentInChildren<InventoryItem>();           // 이 슬롯의 아이템
            InventoryItem dropItem = eventData.pointerDrag.GetComponentInChildren<InventoryItem>(); // 마우스로 집은 아이템
            if(dropItem.GetComponent<EquipmentItem>() != null)
            {
                int SlotNum = Gamemanager.instance.myUIManager.inventoryManager.FindEmptySlot();    // 빈 슬롯을 찾기
                if(dropItem.item.MyItemType == mySlotItem.item.MyItemType)                // 장착 해제 시키는 아이템의 종류가 슬롯에 잇는 아이템과 다르면
                {                                                                                   // 가장 앞에있는 빈슬롯을 찾아서 그곳으로 보내주기
                    transform.GetComponentInChildren<InventoryItem>().ChangeParent(dropItem.parentAfterDrag, true);
                    Destroy(dropItem.GetComponent<EquipmentItem>());
                    dropItem.ChangeParent(transform);
                    transform.GetComponentInChildren<InventoryItem>().AddComponent<EquipmentItem>();
                }
                else
                {
                    if(SlotNum == -1)
                    {
                        return;
                    }
                    else
                    {
                        Gamemanager.instance.myUIManager.inventoryManager.InsertItem(dropItem, SlotNum);
                        Destroy(dropItem.GetComponent<EquipmentItem>());
                    }
                }
            }
            else if (dropItem.GetComponent<EquipmentItem>() == null)
            {
                mySlotItem.ChangeParent(dropItem.parentAfterDrag, true);
                dropItem.ChangeParent(transform);
            }
            
        }
    }

}
