using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Transform mySlotItems;
    public ItemSlotType mySlotType;
    public ItemType myItemType;

    public ItemSlotType tempSlotType;

    public void OnDrop(PointerEventData eventData)
    {
        Item myItem = transform.GetComponentInChildren<Item>();
        Item dropItem = eventData.pointerDrag.GetComponent<Item>();
        int SlotNum = InventoryManager.Inst.FindEmptySlot();

        if (dropItem.slotType == ItemSlotType.Shop) return;  // �����������ϰ��� �ƹ����������ʰ� �ϱ�
        if (myItem == null) // �� �����϶�
        {
            if (dropItem.GetComponent<IItems>() != null) // �̵������� �������ϰ��
            {
                
                switch (mySlotType)
                {
                    case ItemSlotType.Inventory:
                        dropItem.parentAfterDrag = transform;
                        Destroy(dropItem.GetComponent<IItems>().myState);
                        dropItem.AddComponent<InventoryItem>();
                        dropItem.slotType = ItemSlotType.Inventory;
                        myItemType = dropItem.item.MyItemType;
                        break;
                    case ItemSlotType.Equipment:
                        if ((dropItem.item.actionType == ActionType.Attack || dropItem.item.actionType == ActionType.Defense)&&
                            (myItemType == dropItem.item.MyItemType))
                        {
                            dropItem.parentAfterDrag = transform;
                            Destroy(dropItem.GetComponent<IItems>().myState);
                            dropItem.AddComponent<EquipmentItem>();
                            dropItem.slotType = ItemSlotType.Equipment;
                            myItemType = dropItem.item.MyItemType;
                        }
                        break;
                    case ItemSlotType.Quick:
                        if (dropItem.item.actionType == ActionType.UsingItems)
                        {
                            dropItem.parentAfterDrag = transform;
                            Destroy(dropItem.GetComponent<IItems>().myState);
                            dropItem.AddComponent<QuickItem>();
                            dropItem.slotType = ItemSlotType.Quick;
                            myItemType = dropItem.item.MyItemType;
                        }
                        break;
                    case ItemSlotType.Soul:
                        if (dropItem.item.actionType == ActionType.Inchent)
                        {
                            dropItem.parentAfterDrag = transform;
                            Destroy(dropItem.GetComponent<IItems>().myState);
                            dropItem.AddComponent<SoulItem>();
                            dropItem.slotType = ItemSlotType.Soul;
                            myItemType = dropItem.item.MyItemType;
                        }
                        break;
                }
            }
        }
        else // �󽽷��� �ƴҶ�
        {
            // 1. �κ��丮�������� ������� �̵�
            // ���â <-> �κ��丮 , ������ <-> �κ��丮 , �κ��丮 <-> �ҿ� �� �̵��� �����ϰ� ����

            if (dropItem.GetComponent<IItems>() != null)  // �̵� ������ �������ϰ��
            {
                if ((mySlotType == ItemSlotType.Inventory) ^ (dropItem.slotType == ItemSlotType.Inventory)) // ���� �����̳� �ű�� ������ �����ϳ��� �κ��丮�ϰ��
                {
                    if ((mySlotType == ItemSlotType.Equipment) ^ (dropItem.slotType == ItemSlotType.Equipment)) // �ٸ������� ���â�϶� �Ӽ���
                    {
                        if (myItem.item.MyItemType == dropItem.item.MyItemType)
                        {
                            tempSlotType = myItem.slotType;
                            myItem.slotType = dropItem.slotType;
                            dropItem.slotType = tempSlotType;
                        }
                        else
                        {
                            return; // �ٸ� �Ӽ��϶��� ����
                        }
                    }
                    else if ((mySlotType == ItemSlotType.Quick) ^ (dropItem.slotType == ItemSlotType.Quick)) // �������϶� ����
                    {
                        if (dropItem.slotType == ItemSlotType.Quick)
                        {
                            tempSlotType = myItem.slotType;
                            myItem.slotType = dropItem.slotType;
                            dropItem.slotType = tempSlotType;
                        }
                        else return;
                    }
                    else if ((mySlotType == ItemSlotType.Soul) ^ (dropItem.slotType == ItemSlotType.Soul)) // �ҿ�â�϶� ����
                    {
                        if (dropItem.slotType == ItemSlotType.Soul)
                        {
                            tempSlotType = myItem.slotType;
                            myItem.slotType = dropItem.slotType;
                            dropItem.slotType = tempSlotType;
                        }
                        else return;
                    }
                    Destroy(dropItem.GetComponent<IItems>().myState);
                    Destroy(myItem.GetComponent<IItems>().myState);
                    AddItemType(dropItem.slotType, dropItem);
                    AddItemType(myItem.slotType, myItem);
                    myItem.ChangeParent(dropItem.parentAfterDrag, true);
                    dropItem.ChangeParent(transform);
                }
            }
        }

    }

    public void AddItemType(ItemSlotType type, Item item)
    {
        switch (type) 
        {
            case ItemSlotType.Inventory:
                item.transform.AddComponent<InventoryItem>();
                break;
            case ItemSlotType.Equipment:
                item.transform.AddComponent<EquipmentItem>();
                break;
            case ItemSlotType.Quick:
                item.transform.AddComponent<QuickItem>();
                break;
            case ItemSlotType.Soul:
                item.transform.AddComponent<SoulItem>();
                break;
            default:
                break;
        }
    }

}
