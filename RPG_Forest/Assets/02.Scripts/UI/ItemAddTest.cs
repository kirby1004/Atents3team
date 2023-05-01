using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddTest : MonoBehaviour
{

    public ItemStatus[] itemsList;


    public void ItemList(int id)
    {
        Gamemanager.instance.myUIManager.inventoryManager.AddItem(itemsList[id]);
    }

}
