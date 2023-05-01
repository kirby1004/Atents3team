using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Object/PlayerStatus", order = int.MaxValue)]
public class PlayerStatus : ScriptableObject
{
    // �÷��̾��� ���� �����... ����� ����� ���Ϳ� ����ҵ�

    // ĳ���� �̸�
    [SerializeField]
    private string _playerName;
    public string PlayerName { get { return _playerName; } }
    // �ִ�ü��
    [SerializeField]
    private int _maxhp;
    public int MaxHp { get { return _maxhp; } }
    // ���ݷ�
    [SerializeField]
    private int _attackPoint;
    public int AttackPoint { get { return _attackPoint; } }
    // �þ�
    [SerializeField]
    private float _sightRange;
    public float SightRange { get { return _sightRange; } }
    // �̵��ӵ�
    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }
    //���ݼӵ�
    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed { get { return _attackSpeed; } }
    //����
    [SerializeField]
    private float _defensePoint;
    public float DefensePoint { get { return _defensePoint; } }

    //������̺� �ް��ϸ� ���Ϳ����� ��ȭ ����
    //[SerializeField]
    //private Dictionary<int, string> _dropTable;
    //public Dictionary<int, string> DropTable { get {  return _dropTable; } }

}

