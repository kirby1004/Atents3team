using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDropTable", menuName = "Scriptable Object/DropTable", order = int.MaxValue)]
public class ItemDropTable : ScriptableObject
{
    // 아이템의 목록을 배열로 받기
    public ItemStatus[] myDropTable;
    // 아이템의 드랍률 지정해주기
    [Range(0,100)] public List<float> myDropRate;

    [Range(0,1000)] public int mySoulDrop;
    public Vector2Int mySoulDropRange;

}
