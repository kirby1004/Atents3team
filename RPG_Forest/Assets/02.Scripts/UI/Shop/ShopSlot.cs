using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEditor.Timeline;

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
