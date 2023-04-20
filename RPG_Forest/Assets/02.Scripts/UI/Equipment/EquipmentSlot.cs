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
                inventorySlotItem.parentAfterDrag = transform;
                inventorySlotItem.AddComponent<EquipmentItem>();
            }
            else
            {
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
        //else if (inventorySlotItem.GetComponentInChildren<EquipmentItem>() == null)
        //{

        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
