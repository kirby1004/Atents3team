using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDropTable", menuName = "Scriptable Object/DropTable", order = int.MaxValue)]
public class ItemDropTable : ScriptableObject
{
    // �������� ����� �迭�� �ޱ�
    public ItemStatus[] myDropTable;

    //��ųʸ��� �����ʹ�...
    //public Dictionary<int, ItemStatus> myItems;


}
