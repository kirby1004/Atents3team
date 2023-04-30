using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour
{
    public GameObject Iteminfo;
    //public ItemStatus items;
    //public int ItemNum;

    private void Start()
    {

        Iteminfo = Gamemanager.instance.myUIManager.shopManager.itemInfo.gameObject;
    }


}
