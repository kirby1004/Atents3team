using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour , IDropHandler
{

    ItemStatus item;
    public EquipmentType myEquipmentType;
    public EquipmentItem myEquipmentItem;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventorySlotItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if ( myEquipmentType == inventorySlotItem.item.MyEquipmentType)
        {
            if (transform.childCount == 0)
            {
                // 착용중인 장비가 없을때
                // 장비창의 자식으로 설정 , 착용장비 속성 추가
                inventorySlotItem.ChangeParent(transform);
                inventorySlotItem.AddComponent<EquipmentItem>();
            }
            else
            {
                // 이미 끼고잇는 장비가 있을때는 끼고잇던 장비를 착용시킬 장비의 원래슬롯으로 부모를 바꿔주고
                // 착용장비 속성을 삭제하고 , 착용시켜줄 장비의 부모를 장비창으로 바꾸고 착용장비 속성 추가
                InventoryItem mySlotItem = transform.GetComponentInChildren<InventoryItem>();
                if (mySlotItem != null)
                {
                    mySlotItem.ChangeParent(inventorySlotItem.parentAfterDrag, true);
                    Destroy(mySlotItem.GetComponentInChildren<EquipmentItem>());
                }
                inventorySlotItem.ChangeParent(transform);
                inventorySlotItem.AddComponent<EquipmentItem>();
            }
        }
    }
}
