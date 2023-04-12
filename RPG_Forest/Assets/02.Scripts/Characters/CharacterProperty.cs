using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterProperty : MonoBehaviour
{
    public UnityAction DeathAlarm;

    //이동관련 상수
    public float MoveSpeed = 2.0f;
    public float RotSpeed = 360.0f;

    //공격 관련 상수
    public float AttackRange = 1.0f;
    public float AttackDelay = 1.0f;
    public float AttackPoint = 35.0f;
    //방어 관련 상수
    public float DefensePoint = 10.0f;
    //체력 관련 상수
    public float MaxHp = 100.0f;
    float _curHp = -100.0f;

    //쿨타임 관련 상수
    protected float playTime = 0.0f;

    //레벨 관련 상수
    public int Level = 0;

    //로밍몬스터 관련 상수
    public int AdTime = 5;
    public int AdCounter = 0;

    protected float curHp
    {
        get
        {
            if (_curHp < 0.0f) _curHp = MaxHp;
            return _curHp;
        }
        set => _curHp = Mathf.Clamp(value, 0.0f, MaxHp);
    }


    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
                if(_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }
}
