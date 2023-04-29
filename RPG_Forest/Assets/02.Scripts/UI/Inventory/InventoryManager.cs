using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // 인벤토리 슬롯 배열에서 리스트로 변경 리소스과부화시 배열로 변경가능
    public List<InventorySlot> slots = new List<InventorySlot>();   

    // 인벤토리 슬롯 , 아이템 기본틀
    public GameObject inventoryItemPrefab;
    public GameObject inventorySlotPrefab;
    // 최초 인벤토리 슬롯 갯수
    public int startSlotcount = 12;

    // 슬롯들의 부모
    public Transform slotParent;

    // 앞에서부터 빈슬롯 확인 후 빈 슬롯에 아이템 추가
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

    // 아이템 슬롯 생성
    public void SpawnNewSlot()
    {
        GameObject newSlotGo = Instantiate(inventorySlotPrefab , slotParent);
        slots.Add(newSlotGo.GetComponent<InventorySlot>());
    }
    // 다수의 슬롯 생성
    public void SpawnNewSlots(int count)
    {
        for(int i = 0;i < count; i++)
        {
            SpawnNewSlot();
        }
    }
    //맨 마지막 슬롯 삭제
    public void DeleteSlot()
    {
        Destroy(slots[slots.Count-1].gameObject);   //인벤토리슬롯 목록의 맨마지막 오브젝트를 제거하고 리스트에서 제거
        slots.RemoveAt(slots.Count-1);
    }

    // 새로운 아이템 오브젝트 생성
    void SpawnNewItem(ItemStatus item ,InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);    //아이템 오브젝트 원본 받아와서 슬롯의 자식으로 생성
        newItemGo.GetComponent<InventoryItem>().InitialiseItem(item);      //생성한 오브젝트에 InventoryItem 속성 추가 후 item으로 정보주입
    }


    // 게임 시작시 설정된 숫자만큼 아이템 슬롯 생성하게 하기
    void Start()
    {
        SpawnNewSlots(startSlotcount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
