using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemList", menuName = "Scriptable Object/ShopItemList", order = int.MaxValue)]
public class ShopItemList : ScriptableObject
{

    public ListStatus myList;
    public List<ItemStatus> items;

    public List<int> cost;

    


}
 
public enum ListStatus
{
    Selling,SoldOut,Locked
}
