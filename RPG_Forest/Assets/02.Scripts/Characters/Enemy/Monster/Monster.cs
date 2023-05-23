using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ���� ��ũ��Ʈ ������Ʈ�ӽŰ� ���� ���� �и��ϱ� 
public class Monster : CharacterMovement_V2, IPerception, IBattle
{
    public enum eState
    {
        Create,
        Idle,
        Trace,
        Battle,
        Recall,
        Die,

        Fly,
        FlyToBackPos,
        FlySpitFire,
        Landing,
        BattleDragon
    }

    [SerializeField]
    private eState State;

    public static int TotalCount;

    protected StateMachine m_monsterSM; // ������Ʈ �ӽ� ��ũ��Ʈ ����

    public Dictionary<eState, State> m_states = new Dictionary<eState, State>(); // Enum ���� ���¸� Key, value ������ ���� Dictionary�� ����

    public Vector3 orgPos; // ������ ���� ������, Fly State���� Land -> Idle�� ���ƿ� �� y�� ���� �ʿ�
    public Transform myTarget = null; // ������ Ÿ�� -> Player

    public bool canFly = false;         // �� �� �ִ� ������ ���
    //���� ������̺� ����
    public ItemDropTable myDropTable;
    //AIPerception ��ġ ����
    public Transform myAI;

    public bool IsLive => m_monsterSM.CurrentState != m_states[eState.Die];

    protected override void Start()
    {
        m_monsterSM = new StateMachine(); // ������Ʈ �ӽ� ��ũ��Ʈ �ν��Ͻ� ���� 
        
        m_states.Add(eState.Create, new MonsterState_Create(this, m_monsterSM)); 
        m_states.Add(eState.Idle, new MonsterState_Idle(this, m_monsterSM));
        m_states.Add(eState.Trace, new MonsterState_Trace(this, m_monsterSM));
        m_states.Add(eState.Battle, new MonsterState_Battle(this, m_monsterSM));
        m_states.Add(eState.Recall, new MonsterState_Recall(this, m_monsterSM));
        m_states.Add(eState.Die, new MonsterState_Die(this, m_monsterSM));

        m_monsterSM.Initialize(m_states[eState.Create]);
        

        orgPos = this.transform.position;

        TotalCount = 3;

        AttackRange = 2.0f;
        AttackDelay = 1.0f;
    }

    protected override void Update()
    {
        base.Update();
        m_monsterSM.CurrentState.LogicUpdate();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        m_monsterSM.CurrentState.PhysicsUpdate();
    }

    #region Create
    public virtual void OnCreate()
    {

    }
    #endregion

    #region Movement
    // ������ �̵��� ���� �Լ�
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

    #region Find, LostTarget

    public virtual void Find(Transform target)
    {
        myTarget = target;
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => { if (IsLive) m_monsterSM.ChangeState(m_states[eState.Idle]); }; // Death Alarm �Ŀ� ����
        m_monsterSM.ChangeState(m_states[eState.Trace]);
    }

    public void LostTarget()
    {
        myTarget = null;
        if(!canFly) m_monsterSM.ChangeState(m_states[eState.Recall]);
    }



    #endregion

    #region Battle
    public Transform leftClawPoint;

    // IBattle Interface
    public void OnDamage(float dmg)
    {
        if (IsLive)
        {

            Debug.Log("OnDamage");
            curHp -= GameManager.instance.DamageDecrease(dmg,DefensePoint);
            if (Mathf.Approximately(curHp, 0.0f))
            {
                m_monsterSM.ChangeState(m_states[eState.Die]);
            }
            else
            {
                myAnim.SetTrigger("Damage");
            }
        }
    }

    public virtual void OnBattle()
    {
        m_monsterSM.ChangeState(m_states[eState.Battle]);
    }

    #endregion

    #region Die

    UnityEvent deadAction = null;
    public UnityAction ColDelete = null;

    public virtual void OnDie()
    {

    }

    public void OnDisappear()
    {
        StartCoroutine(Disappearing());
    }

    public IEnumerator Disappearing()
    {
        var wfs = new WaitForSeconds(3.0f);
        yield return wfs;
        float dist = 0.0f;
        while (dist < 1.0f)
        {
            dist += Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime);
            yield return null;
        }
        deadAction?.Invoke();
        Destroy(gameObject);
        TotalCount--;
    }

    #endregion
}


