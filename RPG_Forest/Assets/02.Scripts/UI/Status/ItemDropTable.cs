using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDropTable", menuName = "Scriptable Object/DropTable", order = int.MaxValue)]
public class ItemDropTable : ScriptableObject
{
    public ItemStatus[] myDropTable;

    public Dictionary<int, ItemStatus> myItems;


}
