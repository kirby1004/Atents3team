using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcProperty : MonoBehaviour
{
    public enum NPCType //NpcŸ���� ������. ���ӸŴ������� NPC Ÿ���� Ȯ���ϰ� �׿� �´� UI����.
    {
        Shop, SecretShop
    }

    protected NPCType _npctype;

    public NPCType NpcType 
    {
        get
        {
            return _npctype;
        }
    }

    public Transform viewPoint;
    public Transform playerPoint;

    public Transform ViewPoint //NPC ī�޶� ���� ��ġ
    {
        get
        {
            return viewPoint;
        }
    }

    public Transform PlayerPoint //NPC�� ��ȣ�ۿ��� �� �÷��̾ �־�� �� ��ġ.
    {
        get
        {
            return playerPoint;
        }
    }
}
