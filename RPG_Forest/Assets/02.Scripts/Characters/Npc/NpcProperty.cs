using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcProperty : MonoBehaviour
{
    [SerializeField]
    protected NpcType npctype;

    public NpcType npcType 
    {
        get
        {
            return npctype;
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
// NPC ������Ƽ�� �ִ� Enum type �� ShopManager�� �̵���
// Enum ���ÿ� Ŭ���� ���ο� �����ϸ� �ܺ������� �����!

