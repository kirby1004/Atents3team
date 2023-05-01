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
        if (transform.childCount == 0)
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
        else if (transform.childCount == 1)
        {
            InventoryItem mySlotItem = transform.GetComponentInChildren<InventoryItem>();
            InventoryItem inventorySlotItem = eventData.pointerDrag.GetComponentInChildren<InventoryItem>();
            if(mySlotItem != null)
            {
                mySlotItem.ChangeParent(inventorySlotItem.parentAfterDrag,true);
                if (inventorySlotItem.GetComponent<EquipmentItem>() != null)
                {
                    mySlotItem.AddComponent<EquipmentItem>();
                }
            }
            inventorySlotItem.ChangeParent(transform);
            if (inventorySlotItem.GetComponent<EquipmentItem>() != null)
            {
                Destroy(inventorySlotItem.GetComponent<EquipmentItem>());
            }
        }
    }

}
