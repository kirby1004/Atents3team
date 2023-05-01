using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // �κ��丮 ���� �迭���� ����Ʈ�� ���� ���ҽ�����ȭ�� �迭�� ���氡��
    public List<InventorySlot> slots = new List<InventorySlot>();   

    // �κ��丮 ���� , ������ �⺻Ʋ
    public GameObject inventoryItemPrefab;
    public GameObject inventorySlotPrefab;
    // ���� �κ��丮 ���� ����
    public int startSlotcount = 12;

    // ���Ե��� �θ�
    public Transform slotParent;

    // �տ������� �󽽷� Ȯ�� �� �� ���Կ� ������ �߰�
    public void AddItem(ItemStatus item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }

    // ������ ���� ����
    public void SpawnNewSlot()
    {
        GameObject newSlotGo = Instantiate(inventorySlotPrefab , slotParent);
        slots.Add(newSlotGo.GetComponent<InventorySlot>());
    }
    // �ټ��� ���� ����
    public void SpawnNewSlots(int count)
    {
        for(int i = 0;i < count; i++)
        {
            SpawnNewSlot();
        }
    }
    //�� ������ ���� ����
    public void DeleteSlot()
    {
        Destroy(slots[slots.Count-1].gameObject);   //�κ��丮���� ����� �Ǹ����� ������Ʈ�� �����ϰ� ����Ʈ���� ����
        slots.RemoveAt(slots.Count-1);
    }

    // ���ο� ������ ������Ʈ ����
    void SpawnNewItem(ItemStatus item ,InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);    //������ ������Ʈ ���� �޾ƿͼ� ������ �ڽ����� ����
        newItemGo.GetComponent<InventoryItem>().InitialiseItem(item);      //������ ������Ʈ�� InventoryItem �Ӽ� �߰� �� item���� ��������
    }


    // ���� ���۽� ������ ���ڸ�ŭ ������ ���� �����ϰ� �ϱ�
    void Start()
    {
        SpawnNewSlots(startSlotcount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
