using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        //슬롯에 아이템이 있을때는 서로의 슬롯을 바꿔줌 착용중인 장비가 들어오면 장착시키기
        //수정예정 1 타입고려안하고 장착시켜서 무기칸에 투구가 들어가지기도 함
        //타입이 같을경우에만 이동시키게 할 예정
        //수정완료
        else if (transform.GetComponentInChildren<InventoryItem>() != null)
        {
            InventoryItem mySlotItem = transform.GetComponentInChildren<InventoryItem>();
            InventoryItem dropItem = eventData.pointerDrag.GetComponentInChildren<InventoryItem>();
            int SlotNum = Gamemanager.instance.myUIManager.inventoryManager.FindEmptySlot();
            if(dropItem.GetComponent<EquipmentItem>() != null)
            {
                if( dropItem.item.MyEquipmentType == mySlotItem.item.MyEquipmentType)
                {
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
                if (SlotNum == -1)
                {
                    return;
                }
                else
                {
                    Gamemanager.instance.myUIManager.inventoryManager.InsertItem(dropItem, SlotNum);
                }
                dropItem.ChangeParent(transform);
            }
            
        }
    }

}
