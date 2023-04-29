using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStat : ScriptableObject
{
    [SerializeField]
    private string _playerName;
    public string PlayerName { get { return _playerName; } }
    [SerializeField]
    private int _maxhp;
    public int MaxHp { get { return _maxhp; } }
    [SerializeField]
    private int _attackPoint;
    public int AttackPoint { get { return _attackPoint; } }
    [SerializeField]
    private float _sightRange;
    public float SightRange { get { return _sightRange; } }
    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }
    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed { get { return _attackSpeed; } }
    [SerializeField]
    private float _defensePoint;
    public float DefensePoint { get { return _defensePoint; } }
    [SerializeField]
    private Dictionary<int, string> _dropTable;
    public Dictionary<int, string> DropTable { get { return _dropTable; } }
}
