using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    // ������ ������ ���� ����������� , �ʿ䰡�ɼ��� �־ ���ܳ���
    //public ShopItem Selectitems;
    //public ShopSlot SelectShopItems;
    //public GameObject ShopSlotPrefab;

    // ���� ������ ������Ʈ ����
    public GameObject ShopItemPrefab;

    // ���� ���� ������ ���
    public ShopItemList myShopItemList;
    // ������ ���� ���
    public List<ShopSlot> shopSlots;

    // ���� ����� ����â ���� �� ��Ȱ��ȭ ���·� ��ȯ
    // �⺻����� �����ϰ��־�� ���۷��� �������ȳ��� �߰��ص�
    private void Start()
    {
        ShopItemRefreshing(myShopItemList);
        ShopManager.Inst.ShopUI.SetActive(false);
    }

    // �Է¹��� ���ڸ�ŭ �ڿ������� ���� ���� ����
    public void ResetSlots(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Destroy(shopSlots[shopSlots.Count-1].GetComponentInChildren<ShopItem>().gameObject);
            Destroy(shopSlots[shopSlots.Count - 1].gameObject);
            shopSlots.RemoveAt(shopSlots.Count - 1);
        }
    }
    // ���� ���� �����߰� ShopSlotAdd , �����߰� SlotAddItems
    public void ShopSlotAdd()
    {
        GameObject obj = Instantiate(ShopManager.Inst.ShopSlotPrefab, transform);
        shopSlots.Add(obj.GetComponent<ShopSlot>());
    }
    public void SlotAddItems(int index)
    {
        GameObject obj = Instantiate(ShopManager.Inst.ShopList.ShopItemPrefab,
            shopSlots[index].transform);
        obj.GetComponent<ShopItem>().myItem = myShopItemList.items[index];
        obj.GetComponent<ShopItem>().myCost = myShopItemList.cost[index];
        obj.GetComponent<ShopItem>().myImage.sprite = myShopItemList.items[index].Image;
        obj.GetComponent<ShopItem>().buyButton = ShopManager.Inst.BuyButton;
    }

    //���� ��� �����ڷ�ƾ ������� ��ũ���ͺ� ������Ʈ�� �Է¹���
    IEnumerator ShopItemRefresh(ShopItemList shopItemList)
    {
        ResetSlots(shopSlots.Count);
        yield return new WaitForEndOfFrame();
        for(int i = 0;i < shopItemList.items.Count;i++)
        {
            ShopSlotAdd();
            SlotAddItems(i);
        }
    }
    // ���� ��� ���� �ڷ�ƾ ��ũ���ͺ� ������Ʈ�� �Է¹ް� �ڷ�ƾ�� ����
    public void ShopItemRefreshing(ShopItemList shopItemList)
    {
        StartCoroutine(ShopItemRefresh(shopItemList));
    }

    
}
