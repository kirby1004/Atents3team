using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemStatus", menuName = "Scriptable Object/ItemStatus", order = int.MaxValue)]
public class ItemStatus : ScriptableObject
{
    // ����� ���� ex) ���� , ���� ,����
    [SerializeField]
    private ItemType _myItemType;
    public ItemType MyItemType { get { return _myItemType; } }

    // ����� Ÿ�� ex) ���� , �� , �ھ� (�߰����� ��� , ��ȭ�� ���?) �߰��� ���ݺκ��� �����۾� �ʿ� 
    [SerializeField]
    private ActionType _actionType;
    public ActionType actionType { get { return _actionType; } }

    // �������� �̸�
    [SerializeField]
    private string _itemName;
    public string ItemName { get { return _itemName; } }

    //���ݷ�
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

    //���ݼӵ�
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

    //����
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

    //�̵��ӵ�
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

    //�ִ�ü������
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

    //��ø���ɿ��� => ��ȭ��ᳪ ���� �����ſ� ��� ����
    public bool stackable;

    //������ �̹���
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