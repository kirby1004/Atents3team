using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterProperty : MonoBehaviour
{
    public UnityAction DeathAlarm;
    public float MoveSpeed = 3.0f;
    public float RotSpeed = 360.0f; //1초에 한바퀴.
    public float AttackRange = 1.0f;
    public float AttackDelay = 1.0f;
    protected float playTime = 0.0f;
    public float AttackPoint = 35.0f;
    public float MaxHp = 100.0f;
    float _curHp = -100.0f; //캐릭터 프로퍼티는 최상위부모. MonoBehaviour가 부모라서 생성자 x,생성자를 이용해서 초기화 X

    public LayerMask enemyLayer;

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
            if (_anim == null)
            {
                _anim = GetComponent<Animator>(); //자기자신의 것부터 찾아봄.
                if (_anim == null) //없으면 자식의 컴포넌트를 가져옴.
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    Camera _camera = null;
    protected Camera myCamera //Camera를 가져오는 프로퍼티, 캐릭터 프로퍼티를 상속받는 오브젝트라면 자신의 자식에 있는 Camera를 myCamera로 접근 할 수 있게 된다.
    {
        get
        {
            if (_camera == null)
            {
                _camera = GetComponent<Camera>(); //자기자신의 것부터 찾아봄.
                if (_camera == null) //없으면 자식의 컴포넌트를 가져옴.
                {
                    _camera = GetComponentInChildren<Camera>();
                }
            }
            return _camera;
        }
    }

    

}
