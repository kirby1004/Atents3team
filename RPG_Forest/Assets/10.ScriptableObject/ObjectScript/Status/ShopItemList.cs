using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemList", menuName = "Scriptable Object/ShopItemList", order = int.MaxValue)]
public class ShopItemList : ScriptableObject
{
    //상점 아이템 목록

    // 상점의 상태 판매중 , 매진 , 잠김
    // 관련 기능 추가예정
    public ListStatus myList;

    // 판매아이템의 목록
    public List<ItemStatus> items;
    // 판매아이템의 가격
    public List<int> cost;


}
 
public enum ListStatus
{
    Selling,SoldOut,Locked
}
