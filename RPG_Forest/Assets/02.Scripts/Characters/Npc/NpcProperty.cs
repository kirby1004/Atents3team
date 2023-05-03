using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcProperty : MonoBehaviour
{
    public enum NpcType //Npc타입을 저장함. 게임매니저에서 NPC 타입을 확인하고 그에 맞는 UI연결.
    {
        Shop, SecretShop
    }

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

    public Transform ViewPoint //NPC 카메라 뷰의 위치
    {
        get
        {
            return viewPoint;
        }
    }

    public Transform PlayerPoint //NPC와 상호작용할 때 플레이어가 있어야 할 위치.
    {
        get
        {
            return playerPoint;
        }
    }
}
