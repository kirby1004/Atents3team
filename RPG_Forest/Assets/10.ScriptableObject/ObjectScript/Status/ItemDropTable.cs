using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDropTable", menuName = "Scriptable Object/DropTable", order = int.MaxValue)]
public class ItemDropTable : ScriptableObject
{
    // 아이템의 목록을 배열로 받기
    public ItemStatus[] myDropTable;

    //딕셔너리로 만들고싶다...
    //public Dictionary<int, ItemStatus> myItems;


}
