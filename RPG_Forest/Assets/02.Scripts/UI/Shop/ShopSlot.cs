using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEditor.Timeline;

public class ShopSlot : MonoBehaviour ,IPointerExitHandler, IPointerEnterHandler
{
    public GameObject Iteminfo;
    public ItemStatus items;
    public int ItemNum;

    private void Start()
    {
        Iteminfo = Gamemanager.instance.myUIManager.shopManager.itemInfo.gameObject;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OFF");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("On");
        //Gamemanager.instance.myUIManager.shopManager.itemInfo.RefreshItemInfo(items);
    }
}
