using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemStatus", menuName = "Scriptable Object/ItemStatus", order = int.MaxValue)]
public class ItemStatus : ScriptableObject
{

    [SerializeField]
    private EquipmentType _myequipType;
    public EquipmentType MyEquipmentType { get { return _myequipType; } }
    [SerializeField]
    private ActionType _actionType;
    public ActionType actionType { get { return _actionType; } }
    [SerializeField]
    private string _itemName;
    public string ItemName { get { return _itemName; } }

    [SerializeField]
    private int _attackPoint;
    public int AttackPoint
    {
        get
        {
            if (MyEquipmentType == EquipmentType.Weapon)
                return _attackPoint;
            else return 0;
        }
    }
    [SerializeField] 
    private float _attackSpeed;
    public float AttackSpeed 
    { 
        get 
        {
            if (MyEquipmentType == EquipmentType.Weapon)
                return _attackSpeed;
            else return 0;
        } 
    }
    [SerializeField]
    private float _defensePoint;
    public float DefensePoint 
    { 
        get 
        {
            if(MyEquipmentType != EquipmentType.Weapon)
            return _defensePoint; 
            else return 0;
        } 
    }
    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed
    {
        get
        {
            if (MyEquipmentType == EquipmentType.Boots)
                return _moveSpeed;
            else return 0;
        }
    }
    [SerializeField]
    private float _maxHpIncrese;
    public float MaxHpIncrese
    {
        get 
        {
            if (MyEquipmentType != EquipmentType.Weapon)
                return _maxHpIncrese;
            else return 0;
        }
    }

    public bool stackable;

    [SerializeField]
    private Sprite _image;
    public Sprite Image { get { return _image; } }

    
}

public enum EquipmentType
{
    Weapon, Armor, Leggins, Headgear, Boots , Soul
}
public enum ActionType
{
    Attack , Defense , Inchent
}