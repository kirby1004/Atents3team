using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour , IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {

            InventoryItem inventorySlotItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventorySlotItem.parentAfterDrag = transform;
            if (inventorySlotItem.GetComponent<EquipmentItem>() != null)
            {
                Destroy(inventorySlotItem.GetComponent<EquipmentItem>());
                Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
            }
        }
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
                    Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
                }
            }
            inventorySlotItem.ChangeParent(transform);
            if (inventorySlotItem.GetComponent<EquipmentItem>() != null)
            {
                Destroy(inventorySlotItem.GetComponent<EquipmentItem>());
                Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
            }
        }
    }

}
