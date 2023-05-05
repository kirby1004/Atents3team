using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Npc : NpcProperty
{
    // �������� ����Ƽ �׼��� �߰��Ϸ�
    public UnityAction<Transform> ShopOpen;
    // ������ �������� ���� �߰� �ʿ���!
    public UnityAction ShopClose;

    // ShopList�� �޾Ƽ� ���� ����� ���� �ǸŸ��
    public ShopItemList ShopItemList;
    private void Awake()
    {
        npctype = NpcType.Shop;
    }

    // �������� ShopList�� ���� , NPC Type�� ���� �ΰ��� �� ����
    public void OpenMyShop(Transform NPC)
    {
        ShopManager.Inst.OpenShop(ShopItemList);
    }
    public void OpenMyShop2(Transform NPC)
    {
        ShopManager.Inst.OpenShop(npcType);
    }

}
