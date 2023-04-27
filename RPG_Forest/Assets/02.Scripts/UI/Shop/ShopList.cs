using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    public ShopItem Selectitems;
    public ShopSlot SelectShopItems;
    public GameObject ShopSlotPrefab;
    public GameObject ShopItemPrefab;

    public ShopItemList myShopItemList;
    public List<ShopSlot> shopSlots;

    private void Start()
    {
        ShopItemRefreshing(myShopItemList);
        Gamemanager.instance.myUIManager.shopManager.ShopUI.SetActive(false);
    }


    public void ResetSlots(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Destroy(shopSlots[shopSlots.Count-1].GetComponentInChildren<ShopItem>().gameObject);
            Destroy(shopSlots[shopSlots.Count - 1].gameObject);
            shopSlots.RemoveAt(shopSlots.Count - 1);
        }
    }

    public void ShopSlotAdd()
    {
        GameObject obj = Instantiate(Gamemanager.instance.myUIManager.shopManager.ShopSlotPrefab, transform);
        shopSlots.Add(obj.GetComponent<ShopSlot>());
    }

    public void SlotAddItems(int index)
    {
        GameObject obj = Instantiate(Gamemanager.instance.myUIManager.shopManager.ShopList.ShopItemPrefab,
            shopSlots[index].transform);
        obj.GetComponent<ShopItem>().myItem = myShopItemList.items[index];
        obj.GetComponent<ShopItem>().myCost = myShopItemList.cost[index];
        obj.GetComponent<ShopItem>().myImage.sprite = myShopItemList.items[index].Image;
        obj.GetComponent<ShopItem>().myButton = Gamemanager.instance.myUIManager.shopManager.BuyButton;
    }

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
    public void ShopItemRefreshing(ShopItemList shopItemList)
    {
        StartCoroutine(ShopItemRefresh(shopItemList));
    }

    
}
