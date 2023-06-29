using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

interface IItems  // �ٸ������� ���������̵������� �����۸� ��ӹް� �ϱ�
{
    Component myState
    {
        get;
    }

}

public class InventoryManager : Singleton<InventoryManager>
{
    public List<Slot> slots = new();

    // �κ��丮 ���� , ������ �⺻Ʋ
    public GameObject inventoryItemPrefab;
    public GameObject inventorySlotPrefab;
    // ���� �κ��丮 ���� ����
    public int startSlotcount = 12;

    //public float myMoney;
    public TMP_Text myMoneyText;
    // ���Ե��� �θ�
    public Transform slotParent;
    private void Awake()
    {
        base.Initialize();

        
    }

    // �տ������� �󽽷� Ȯ�� �� �� ���Կ� ������ �߰� / ������ �������� ����ó�� �� ������ �۾� �Ϸ�
    public void AddItem(ItemStatus item)
    {
        int slotIndex = FindEmptySlot();
        if (slotIndex == -1) return;
        else
        {
            SpawnNewItem(item, slots[slotIndex]);
        }
    }

    // �� ������ ��ȣ ã�� -1 ���Ͻ� ����
    public int FindEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponentInChildren<Item>() == null)
            {
                return i;
            }
        }
        return -1;
    }

    // Ư�� ���Կ� ������ �ű��
    public void InsertItem(Item item, int SlotNum)
    {
        if (slots[SlotNum].transform.GetComponentInChildren<Item>() == null)
        {
            item.ChangeParent(slots[SlotNum].transform, true);
        }
        else return;
    }

    // ������ ���� ����
    public void SpawnNewSlot()
    {
        GameObject newSlotGo = Instantiate(inventorySlotPrefab, slotParent);
        newSlotGo.GetComponent<Slot>().mySlotType = ItemSlotType.Inventory;
        slots.Add(newSlotGo.GetComponent<Slot>());
    }

    // �ټ��� ���� ����
    public void SpawnNewSlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnNewSlot();
        }
    }

    //�� ������ ���� ����
    public void DeleteSlot()
    {
        Destroy(slots[slots.Count - 1].gameObject);   //�κ��丮���� ����� �Ǹ����� ������Ʈ�� �����ϰ� ����Ʈ���� ����
        slots.RemoveAt(slots.Count - 1);
    }

    // ���ο� ������ ������Ʈ ����
    public void SpawnNewItem(ItemStatus item, Slot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);    //������ ������Ʈ ���� �޾ƿͼ� ������ �ڽ����� ����
        newItemGo.GetComponent<Item>().InitialiseItem(item);      //������ ������Ʈ�� InventoryItem �Ӽ� �߰� �� item���� ��������
        newItemGo.AddComponent<InventoryItem>();
    }

    public ItemStatus MakeItemFromItemCode(DataSaverManager.ItemCodes itemcodes)
    {
        switch (itemcodes)
        {
            case DataSaverManager.ItemCodes.T1Weapon:
                break;
            case DataSaverManager.ItemCodes.T1Helmet:
                break;
            case DataSaverManager.ItemCodes.T1Armor:
                break;
            case DataSaverManager.ItemCodes.T1Leggins:
                break;
            case DataSaverManager.ItemCodes.T1Boots:
                break;
            case DataSaverManager.ItemCodes.T2Weapon:
                break;
            case DataSaverManager.ItemCodes.T2Helmet:
                break;
            case DataSaverManager.ItemCodes.T2Armor:
                break;
            case DataSaverManager.ItemCodes.T2Leggins:
                break;
            case DataSaverManager.ItemCodes.T2Boots:
                break;
            case DataSaverManager.ItemCodes.T3Weapon:
                break;
            case DataSaverManager.ItemCodes.T3Helmet:
                break;
            case DataSaverManager.ItemCodes.T3Armor:
                break;
            case DataSaverManager.ItemCodes.T3Leggins:
                break;
            case DataSaverManager.ItemCodes.T3Boots:
                break;
            default:
                break;
        }
        //Resources.Load();

        string ItemName = itemcodes.ToString();

        return null;
    }


    // ���� ���۽� ������ ���ڸ�ŭ ������ ���� �����ϰ� �ϱ�
    void Start()
    {
        SpawnNewSlots(startSlotcount);
        Gamemanager.Inst.UpdateMoney.AddListener(UpdateMyMoney);
        UpdateMyMoney(Gamemanager.Inst.Money);
        DataSaverManager.Inst.LoadInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMyMoney(int money)
    {
        myMoneyText.text = money.ToString();
    }

}
