using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Npc : NpcProperty
{
    // 상점열기 유니티 액션은 추가완료
    public UnityAction<Transform> ShopOpen;
    // 상점을 닫을때의 동작 추가 필요함!
    public UnityAction ShopClose;

    // ShopList를 받아서 열때 사용할 상점 판매목록
    public ShopItemList ShopItemList;
    private void Awake()
    {
        npctype = NpcType.Shop;
    }

    // 상점열기 ShopList로 열기 , NPC Type로 열기 두가지 다 가능
    public void OpenMyShop(Transform NPC)
    {
        ShopManager.Inst.OpenShop(ShopItemList);
    }
    public void OpenMyShop2(Transform NPC)
    {
        ShopManager.Inst.OpenShop(npcType);
    }

}
