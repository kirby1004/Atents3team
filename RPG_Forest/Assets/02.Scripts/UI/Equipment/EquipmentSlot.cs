using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EquipmentSlot : MonoBehaviour , IDropHandler
{

    ItemStatus item;
    public ItemType myEquipmentType;
    public EquipmentItem myEquipmentItem;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventorySlotItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if ( myEquipmentType == inventorySlotItem.item.MyItemType)
        {
            if (transform.GetComponentInChildren<EquipmentItem>() == null)
            {
                // �������� ��� ������
                // ���â�� �ڽ����� ���� , ������� �Ӽ� �߰�
                inventorySlotItem.ChangeParent(transform);
                inventorySlotItem.AddComponent<EquipmentItem>();
            }
            else if (transform.GetComponentInChildren<EquipmentItem>() != null) 
            {
                // �̹� �����մ� ��� �������� �����մ� ��� �����ų ����� ������������ �θ� �ٲ��ְ�
                // ������� �Ӽ��� �����ϰ� , ��������� ����� �θ� ���â���� �ٲٰ� ������� �Ӽ� �߰�
                transform.GetComponentInChildren<InventoryItem>().ChangeParent(inventorySlotItem.parentAfterDrag, true);
                Destroy(transform.GetComponentInChildren<EquipmentItem>());
                inventorySlotItem.ChangeParent(transform);
                inventorySlotItem.AddComponent<EquipmentItem>();
            }
        }
    }
}
