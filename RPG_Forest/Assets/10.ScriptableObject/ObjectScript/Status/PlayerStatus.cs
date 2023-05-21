using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Object/PlayerStatus", order = int.MaxValue)]
public class PlayerStatus : ScriptableObject
{
    // 플레이어의 스텟 저장용... 사용방식 고민중 몬스터에 사용할듯

    // 캐릭터 이름
    [SerializeField]
    private string _playerName;
    public string PlayerName { get { return _playerName; } }
    // 최대체력
    [SerializeField]
    private int _maxhp;
    public int MaxHp { get { return _maxhp; } }
    // 공격력
    [SerializeField]
    private int _attackPoint;
    public int AttackPoint { get { return _attackPoint; } }

    // 이동속도
    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }
    //방어력
    [SerializeField]
    private float _defensePoint;
    public float DefensePoint { get { return _defensePoint; } }


}

