using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStatus", menuName = "Scriptable Object/ItemStatus", order = int.MaxValue)]
public class ItemStatus : ScriptableObject
{
    public enum EquipmentType
    {
        Weapon , Armor , Leggins , Headgear , Boots
    }
    //public EquipmentType myequipmentType;
    [SerializeField]
    private EquipmentType _myequipType;
    public EquipmentType MyEquipmentType { get { return _myequipType; } }
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
            if (MyEquipmentType != EquipmentType.Boots)
                return _moveSpeed;
            else return 0;
        }
    }




}
