using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemList", menuName = "Scriptable Object/ShopItemList", order = int.MaxValue)]
public class ShopItemList : ScriptableObject
{
    //���� ������ ���

    // ������ ���� �Ǹ��� , ���� , ���
    // ���� ��� �߰�����
    public ListStatus myList;

    // �Ǹž������� ���
    public List<ItemStatus> items;
    // �Ǹž������� ����
    public List<int> cost;


}
 
public enum ListStatus
{
    Selling,SoldOut,Locked
}
