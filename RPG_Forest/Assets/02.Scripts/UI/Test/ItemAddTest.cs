using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddTest : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemStatus[] itemsList;


    public void ItemList(int id)
    {
        inventoryManager.AddItem(itemsList[id]);
    }

    public void SlotList(int id)
    {
        //inventoryManager.inventorySlots.
        
    }

}
