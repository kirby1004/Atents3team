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
        if (transform.childCount == 0)
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
