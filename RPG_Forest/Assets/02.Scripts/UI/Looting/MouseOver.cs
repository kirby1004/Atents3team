using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public Item myItem = null;
    public GameObject myObj = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(myObj);
        myItem = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        myItem = eventData.pointerEnter.GetComponent<Item>();
        myObj = Instantiate(Resources.Load("UIResource/MouseOverDisplay") as GameObject);
        myObj.AddComponent<Item>();
        myObj.GetComponent<Item>().item = myItem.item;
    }

    
}
