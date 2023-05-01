using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour , IDropHandler
{

    // �������� �巡�� �� ��� ������
    public void OnDrop(PointerEventData eventData)
    {
        // ���Կ� �������� �������� �� �������� �̵�
        if (transform.GetComponentInChildren<InventoryItem>() == null)
        {

            InventoryItem inventorySlotItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventorySlotItem.parentAfterDrag = transform;
            if (inventorySlotItem.GetComponent<EquipmentItem>() != null)
            {
                Destroy(inventorySlotItem.GetComponent<EquipmentItem>());
            }
        }
        // ���Կ� �������� �����Ҷ��� ���� 
        // 1. ���콺�� ����� �������� �������ξ������϶� Ÿ�Ժ� �� 
        //

        else if (transform.GetComponentInChildren<InventoryItem>() != null)
        {
            InventoryItem mySlotItem = transform.GetComponentInChildren<InventoryItem>();           // �� ������ ������
            InventoryItem dropItem = eventData.pointerDrag.GetComponentInChildren<InventoryItem>(); // ���콺�� ���� ������
            if(dropItem.GetComponent<EquipmentItem>() != null)
            {
                int SlotNum = Gamemanager.instance.myUIManager.inventoryManager.FindEmptySlot();    // �� ������ ã��
                if(dropItem.item.MyItemType == mySlotItem.item.MyItemType)                // ���� ���� ��Ű�� �������� ������ ���Կ� �մ� �����۰� �ٸ���
                {                                                                                   // ���� �տ��ִ� �󽽷��� ã�Ƽ� �װ����� �����ֱ�
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
