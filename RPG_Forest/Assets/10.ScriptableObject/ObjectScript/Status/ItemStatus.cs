using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemStatus", menuName = "Scriptable Object/ItemStatus", order = int.MaxValue)]
public class ItemStatus : ScriptableObject
{
    // 장비의 부위 ex) 무기 , 갑옷 ,투구
    [SerializeField]
    private ItemType _myItemType;
    public ItemType MyItemType { get { return _myItemType; } }

    // 장비의 타입 ex) 무기 , 방어구 , 코어 (추가예정 재료 , 강화석 등등?) 추가시 스텟부분은 수정작업 필요 
    [SerializeField]
    private ActionType _actionType;
    public ActionType actionType { get { return _actionType; } }

    // 아이템의 이름
    [SerializeField]
    private string _itemName;
    public string ItemName { get { return _itemName; } }

    //공격력
    [SerializeField]
    private int _attackPoint;
    public int AttackPoint
    {
        get
        {
            if (MyItemType == ItemType.Weapon)
                return _attackPoint;
            else return 0;
        }
    }

    //공격속도
    [SerializeField] 
    private float _attackSpeed;
    public float AttackSpeed 
    { 
        get 
        {
            if (MyItemType == ItemType.Weapon)
                return _attackSpeed;
            else return 0;
        } 
    }

    //방어력
    [SerializeField]
    private float _defensePoint;
    public float DefensePoint 
    { 
        get 
        {
            if(MyItemType != ItemType.Weapon && MyItemType != ItemType.Others)
            return _defensePoint; 
            else return 0;
        } 
    }

    //이동속도
    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed
    {
        get
        {
            if (MyItemType == ItemType.Boots)
                return _moveSpeed;
            else return 0;
        }
    }

    //최대체력증가
    [SerializeField]
    private float _maxHpIncrese;
    public float MaxHpIncrese
    {
        get 
        {
            if (MyItemType != ItemType.Weapon && MyItemType != ItemType.Others)
                return _maxHpIncrese;
            else return 0;
        }
    }

    //중첩가능여부 => 강화재료나 잡템 같은거에 사용 예정
    public bool stackable;

    //아이템 이미지
    [SerializeField]
    private Sprite _image;
    public Sprite Image { get { return _image; } }

    
}

public enum ItemType
{
    Weapon, Armor, Leggins, Headgear, Boots , Soul , Others
}
public enum ActionType
{
    Attack , Defense , Inchent , UsedItem
}