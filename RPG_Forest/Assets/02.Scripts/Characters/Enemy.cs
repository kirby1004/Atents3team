using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ���ʹ� ��ũ��Ʈ ������Ʈ�ӽŰ� ���� ���� �и��ϱ� 
public class Enemy : CharacterMovement_V2, IPerception
{
    public enum eState
    {
        Create,
        Idle,
        Trace,
        Battle,
        Recall,
        Fly,
        Die
    }

    private StateMachine m_enemySM; // ������Ʈ �ӽ� ��ũ��Ʈ ����

    public Dictionary<eState, State> m_states = new Dictionary<eState, State>(); // Enum ���� ���¸� Key, value ������ ���� Dictionary�� ����

    public Vector3 orgPos; // �巡���� ���� ������, Fly State���� Land -> Idle�� ���ƿ� �� y�� ���� �ʿ�
    public Transform myTarget = null; // ���ʹ��� Ÿ�� -> Player

    //// IsLive ������Ƽ ����
    //public bool IsLive => throw new System.NotImplementedException();


    protected override void Start()
    {
        m_enemySM = new StateMachine(); // ������Ʈ �ӽ� ��ũ��Ʈ �ν��Ͻ� ���� 
        
        m_states.Add(eState.Create, new EnemyState_Create(this, m_enemySM)); 
        m_states.Add(eState.Idle, new EnemyState_Idle(this, m_enemySM));
        m_states.Add(eState.Trace, new EnemyState_Trace(this, m_enemySM));
        m_states.Add(eState.Battle, new EnemyState_Battle(this, m_enemySM));
        m_states.Add(eState.Recall, new EnemyState_Recall(this, m_enemySM));
        m_states.Add(eState.Fly, new EnemyState_Fly(this, m_enemySM));
        m_states.Add(eState.Die, new EnemyState_Recall(this, m_enemySM));

        m_enemySM.Initialize(m_states[eState.Create]);
    }

    protected override void Update()
    {
        base.Update();
        m_enemySM.CurrentState.LogicUpdate();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        m_enemySM.CurrentState.PhysicsUpdate();
    }

    #region Movement
    // �巡���� �̵��� ���� �Լ�
    public override void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        StartCoroutine(MovingToPos(pos, done));
    }

    protected IEnumerator MovingToPos(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        StartCoroutine(Rotating(dir));

        myAnim.SetBool("isMoving", true);

        while (dist > 0.0f)
        {
            if (!myAnim.GetBool("isAttacking"))
            {
                float delta = MoveSpeed * Time.deltaTime;
                if (dist - delta < 0.0f)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }

        myAnim.SetBool("isMoving", false);
        done?.Invoke();
    }

    IEnumerator Rotating(Vector3 dir)  // ���� : Normalize �� ���͸� �Է¹ް� �Ұ�
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }
        while (angle > 0.0f)
        {
            float delta = RotSpeed * Time.deltaTime;
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;
        }
    }
    #endregion


    
    #region Fly

    public Vector3 flyPos;
    public bool isFlying = false;
    public float flyHeight = 10.0f;
    //public bool startFlyBoosting = false;

    #endregion


    #region Find, LostTarget

    public void Find(Transform target)
    {
        myTarget = target;
        //enemy.myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => { if (IsLive) ChangeState(State.Normal); }; // Death Alarm �Ŀ� ����
        m_enemySM.ChangeState(m_states[eState.Battle]);
    }

    public void LostTarget()
    {
        myTarget = null;
        //Rangeout(Spawn); // Recall State -> Battle State ���Ŀ� ����
        m_enemySM.ChangeState(m_states[eState.Recall]);
    }

    #endregion


    #region Battle
    //public override void Attack(Transform target = null)
    //{
    //    throw new System.NotImplementedException();
    //}

    // ��Ÿ�� üũ �Լ�? �ڷ�ƾ? ���� �ʿ�

    #endregion


}


