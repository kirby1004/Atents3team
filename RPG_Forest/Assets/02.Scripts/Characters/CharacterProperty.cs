using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterProperty : MonoBehaviour
{
    public UnityAction DeathAlarm;
    public PlayerStatus myBaseStatus;

    [HideInInspector]
    public float MoveSpeed { get { return myBaseStatus.MoveSpeed; } }
    [HideInInspector]
    public float RotSpeed = 360.0f; //1�ʿ� �ѹ���.
    public float AttackRange = 1.0f;
    public float AttackDelay = 1.0f;

    [HideInInspector]
    public float playTime = 0.0f;
    public float AttackPoint { get { return myBaseStatus.AttackPoint; }}
    public float DefensePoint { get { return myBaseStatus.DefensePoint;}}
    public float MaxHp { get { return myBaseStatus.MaxHp; } }
    float _curHp = -100.0f; //ĳ���� ������Ƽ�� �ֻ����θ�. MonoBehaviour�� �θ�� ������ x,�����ڸ� �̿��ؼ� �ʱ�ȭ X

    public LayerMask enemyLayer;

    [HideInInspector]
    public UnityEvent<float> UpdateHp;
    public float curHp
    {
        get
        {
            if (_curHp < 0.0f) _curHp = MaxHp;
            return _curHp;
        }
        set 
        {
            _curHp = Mathf.Clamp(value, 0.0f, MaxHp);
            UpdateHp?.Invoke(Mathf.Approximately(MaxHp, 0.0f) ? 0.0f : _curHp);
        }
    }
    Animator _anim = null;
    public Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>(); //�ڱ��ڽ��� �ͺ��� ã�ƺ�.
                if (_anim == null) //������ �ڽ��� ������Ʈ�� ������.
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    Camera _camera = null;
    protected Camera myCamera //Camera�� �������� ������Ƽ, ĳ���� ������Ƽ�� ��ӹ޴� ������Ʈ��� �ڽ��� �ڽĿ� �ִ� Camera�� myCamera�� ���� �� �� �ְ� �ȴ�.
    {
        get
        {
            if (_camera == null)
            {
                _camera = GetComponent<Camera>(); //�ڱ��ڽ��� �ͺ��� ã�ƺ�.
                if (_camera == null) //������ �ڽ��� ������Ʈ�� ������.
                {
                    _camera = GetComponentInChildren<Camera>();
                }
            }
            return _camera;
        }
    }

    

}
