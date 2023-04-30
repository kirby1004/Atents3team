using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        //���Կ� �������� �������� ������ ������ �ٲ��� �������� ��� ������ ������Ű��
        //�������� 1 Ÿ�԰�����ϰ� �������Ѽ� ����ĭ�� ������ �����⵵ ��
        //Ÿ���� ������쿡�� �̵���Ű�� �� ����
        //�����Ϸ�
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
