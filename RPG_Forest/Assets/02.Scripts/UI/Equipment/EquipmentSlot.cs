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
                // �������� ��� ������
                // ���â�� �ڽ����� ���� , ������� �Ӽ� �߰�
                inventorySlotItem.ChangeParent(transform);
                inventorySlotItem.AddComponent<EquipmentItem>();
                //Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
            }
            else
            {
                // �̹� �����մ� ��� �������� �����մ� ��� �����ų ����� ������������ �θ� �ٲ��ְ�
                // ������� �Ӽ��� �����ϰ� , ��������� ����� �θ� ���â���� �ٲٰ� ������� �Ӽ� �߰�
                InventoryItem mySlotItem = transform.GetComponentInChildren<InventoryItem>();
                if (mySlotItem != null)
                {
                    mySlotItem.ChangeParent(inventorySlotItem.parentAfterDrag, true);
                    Destroy(mySlotItem.GetComponentInChildren<EquipmentItem>());
                    //Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
                }
                inventorySlotItem.ChangeParent(transform);
                inventorySlotItem.AddComponent<EquipmentItem>();
                //Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
            }
        }

        //else if (inventorySlotItem.GetComponentInChildren<EquipmentItem>() == null)
        //{

        //}
    }
}
