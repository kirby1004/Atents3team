using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDropTable", menuName = "Scriptable Object/DropTable", order = int.MaxValue)]
public class ItemDropTable : ScriptableObject
{
    // �������� ����� �迭�� �ޱ�
    public ItemStatus[] myDropTable;
    // �������� ����� �������ֱ�
    [Range(0,100)] public List<float> myDropRate;

    [Range(0,1000)] public int mySoulDrop;
    public Vector2Int mySoulDropRange;

}
