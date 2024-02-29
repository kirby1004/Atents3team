using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcProperty : MonoBehaviour
{
    public enum NPCType
    {
        Shop, SecretShop
    }

    protected NPCType _npcType;

    public NPCType NpcType
    {
        get { return _npcType; }
    }

    public Transform viewPoint;
    public Transform playerPoint;

    public Transform ViewPoint
    {
        get { return viewPoint; }
    }

    public Transform PlayerPoint
    {
        get { return playerPoint; }
    }


}
