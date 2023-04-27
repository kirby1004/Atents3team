using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ShopList ShopList;

    public Transform myShopList;
    public ItemInfo itemInfo;

    public GameObject ShopSlotPrefab;
    public GameObject ShopUI;

    public Button BuyButton;
    public void AddListInfo(ItemStatus item)
    {
        
        
    }

    public void OpenShop(ShopItemList itemList)
    {
        ShopUI.SetActive(true);
        Gamemanager.instance.myUIManager.shopManager.myShopList.GetComponent<ShopList>().myShopItemList = itemList;
        Gamemanager.instance.myUIManager.shopManager.ShopList.ShopItemRefreshing(itemList);
    }

    private void Start()
    {
        
    }

    //public List<ShopSlot> slotList;
    //public void AddSlot()
    //{
    //    GameObject obj = Instantiate(ShopSlotPrefab, myShopList);
    //    slotList.Add(obj.GetComponent<ShopSlot>());
    //}
    //public void DeleteSlot()
    //{
    //    Destroy(slotList[slotList.Count - 1].gameObject);
    //    slotList.RemoveAt(slotList.Count - 1);
    //}


}
