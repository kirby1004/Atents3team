using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // 인벤토리 슬롯 배열에서 리스트로 변경 리소스과부화시 배열로 변경가능
    public List<InventorySlot> slots = new List<InventorySlot>();   
    //public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject inventorySlotPrefab;
    public int startSlotcount = 12;

    public Transform slotParent;
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

    public void SpawnNewSlot()
    {
        GameObject newSlotGo = Instantiate(inventorySlotPrefab , slotParent);
        slots.Add(newSlotGo.GetComponent<InventorySlot>());
    }
    public void SpawnNewSlots(int count)
    {
        for(int i = 0;i < count; i++)
        {
            SpawnNewSlot();
        }
    }
    public void DeleteSlot()
    {
        
        Destroy(slots[slots.Count-1].gameObject);
        slots.RemoveAt(slots.Count-1);
    }

    void SpawnNewItem(ItemStatus item ,InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewSlots(startSlotcount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
