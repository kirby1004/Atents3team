using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler , IPointerClickHandler
{

    public ItemStatus myItem;
    public int myCost;
    public Image myImage;
    public Button myButton;

    public void BuyItem(ItemStatus item,bool isDone)
    {
        if (isDone)
        {
            //Destroy(GetComponent<ShopItem>());
            Gamemanager.instance.myUIManager.inventoryManager.AddItem(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OFF");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("On");
        Gamemanager.instance.myUIManager.shopManager.itemInfo.RefreshItemInfo
            (eventData.pointerEnter.GetComponent<ShopItem>().myItem);
        Gamemanager.instance.myUIManager.shopManager.itemInfo.myCost = myCost;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        myButton.gameObject.SetActive(true);
        myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(() =>gameObject.GetComponent<ShopItem>().BuyItem(myItem,true));
    }
}
