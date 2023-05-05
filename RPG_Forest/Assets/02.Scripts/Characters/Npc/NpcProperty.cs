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
// NPC 프로퍼티에 있는 Enum type 는 ShopManager로 이동함
// Enum 사용시에 클래스 내부에 선언하면 외부참조가 어려움!

