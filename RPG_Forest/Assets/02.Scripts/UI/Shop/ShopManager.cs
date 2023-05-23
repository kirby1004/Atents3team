using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
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
    #region ���� ����
    // ���� ���� ver1 ���� ����� �޾Ƽ� ����
    public void OpenShop(ShopItemList itemList)
    {
        ShopUI.SetActive(true);
        ShopManager.Inst.myShopList.GetComponent<ShopList>().myShopItemList = itemList;
        ShopManager.Inst.ShopList.ShopItemRefreshing(itemList);
    }
    //���� ���� ver2 ���� Ÿ���� �޾Ƽ� ����
    public void OpenShop(NpcProperty.NPCType npcType,UnityAction e=null)
    {
        e?.Invoke();
        ShopUI.SetActive(true);
        ShopManager.Inst.myShopList.GetComponent<ShopList>().myShopItemList = ShopItemList[(int)npcType];
        ShopManager.Inst.ShopList.ShopItemRefreshing(ShopItemList[(int)npcType]);
    }
    #endregion

    #region ���� �ݱ�
    public void CloseShop(UnityAction camera=null,UnityAction interplay=null)
    {
        ShopUI.SetActive(false);
        camera?.Invoke();
        interplay?.Invoke();
    }
    #endregion

    private void Awake()
    {
        base.Initialize();
    }
    private void Start()
    {
        
    }
}
