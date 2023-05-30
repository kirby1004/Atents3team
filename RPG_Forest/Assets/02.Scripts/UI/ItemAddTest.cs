using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddTest : MonoBehaviour
{

    public ItemStatus[] itemsList;

    public ItemDropTable[] itemDropTables;

    public ShopItemList[] myShopList;

    public void ItemList(int id)
    {
        InventoryManager.Inst.AddItem(itemsList[id]);
    }

    public void Getmoney(int money)
    {
        Gamemanager.Inst.economy.GetMoney(money);
    }

    public void OpenShops(ShopItemList shops)
    {
        ShopManager.Inst.OpenShop(shops);
    }
}
