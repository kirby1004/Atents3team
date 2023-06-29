using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryItem : MonoBehaviour , IItems
{
    public Component myState
    {
        get => this as Component;
    }
    private void Start()
    {
        transform.parent.GetComponent<Slot>().mySlotItems = transform;
    }
    private void OnDestroy()
    {
        //transform.GetComponent<Item>().parentBeforeDrag.GetComponent<Slot>().mySlotItems = null;

    }


}
