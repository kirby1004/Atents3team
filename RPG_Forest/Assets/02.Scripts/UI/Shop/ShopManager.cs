using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // ������� ��ũ��Ʈ , ������Ʈ ��ũ
    public ShopList ShopList;
    public Transform myShopList;
    // �Ǹ� ������ ���� ������Ʈ ��ũ
    public ItemInfo itemInfo;

    // ���� ���� ������Ʈ ���� 
    public GameObject ShopSlotPrefab;
    public GameObject ShopUI;

    // ���Ź�ư
    public Button BuyButton;
    // ���� ����� ����
    public List<ShopItemList> ShopItemList;


    public void AddListInfo(ItemStatus item)
    {
        
    }

    // ���� ���� ver1 ���� ����� �޾Ƽ� ����
    public void OpenShop(ShopItemList itemList)
    {
        ShopUI.SetActive(true);
        Gamemanager.instance.myUIManager.shopManager.myShopList.GetComponent<ShopList>().myShopItemList = itemList;
        Gamemanager.instance.myUIManager.shopManager.ShopList.ShopItemRefreshing(itemList);
    }
    // ���� ���� ver2 ���� Ÿ���� �޾Ƽ� ����
    public void OpenShop(NpcType npcType)
    {
        ShopUI.SetActive(true);
        Gamemanager.instance.myUIManager.shopManager.myShopList.GetComponent<ShopList>().myShopItemList = ShopItemList[(int)npcType];
        Gamemanager.instance.myUIManager.shopManager.ShopList.ShopItemRefreshing(ShopItemList[(int)npcType]);
    }

    private void Start()
    {
        
    }
}

//�̵�����
public enum NpcType //NpcŸ���� ������.
{
    Shop, SecretShop
}