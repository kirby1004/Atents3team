using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 에너미 스크립트 스테이트머신과 패턴 전부 분리하기 
public class Monster : CharacterMovement_V2, IPerception, IBattle
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

    public static int TotalCount;

    private StateMachine m_monsterSM; // 스테이트 머신 스크립트 참조

    public Dictionary<eState, State> m_states = new Dictionary<eState, State>(); // Enum 값과 상태를 Key, value 값으로 갖는 Dictionary로 생성

    public Vector3 orgPos; // 드래곤의 원래 포지션, Fly State에서 Land -> Idle로 돌아올 때 y값 저장 필요
    public Transform myTarget = null; // 에너미의 타겟 -> Player
    public ItemDropTable myDropTable; // 이 몬스터의 드랍테이블
    // IsLive 프로퍼티 구현
    //public bool IsLive
    //{

    //    get => m_enemySM.CurrentState(m_states[eState.Die]);
    //}

    public bool IsLive => throw new System.NotImplementedException();

    protected override void Start()
    {
        m_monsterSM = new StateMachine(); // 스테이트 머신 스크립트 인스턴스 생성 
        
        m_states.Add(eState.Create, new MonsterState_Create(this, m_monsterSM)); 
        m_states.Add(eState.Idle, new MonsterState_Idle(this, m_monsterSM));
        m_states.Add(eState.Trace, new MonsterState_Trace(this, m_monsterSM));
        m_states.Add(eState.Battle, new MonsterState_Battle(this, m_monsterSM));
        m_states.Add(eState.Recall, new MonsterState_Recall(this, m_monsterSM));
        m_states.Add(eState.Fly, new MonsterState_Fly(this, m_monsterSM));
        m_states.Add(eState.Die, new MonsterState_Die(this, m_monsterSM));

        m_monsterSM.Initialize(m_states[eState.Create]);

        orgPos = this.transform.position;

        TotalCount = 3;

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

    #region Movement
    // 드래곤의 이동에 관한 함수
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

    IEnumerator Rotating(Vector3 dir)  // 주의 : Normalize 된 벡터만 입력받게 할것
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
        //monster.myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => { if (IsLive) ChangeState(State.Normal); }; // Death Alarm 후에 구현
        m_monsterSM.ChangeState(m_states[eState.Trace]);
    }

    public void LostTarget()
    {
        myTarget = null;
        m_monsterSM.ChangeState(m_states[eState.Recall]);
    }



    #endregion


    #region Battle
    //public override void Attack(Transform target = null)
    //{

    //}

    public Transform leftClawPoint;

    // IBattle Interface
    public void OnDamage(float dmg)
    {
        curHp -= dmg;
        if (Mathf.Approximately(curHp, 0.0f))
        {
            m_monsterSM.ChangeState(m_states[eState.Die]);
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }

    //public void AttackExit()
    //{
    //    if (myTarget == null)
    //    {
    //        Debug.Log("AttackExit");
    //    }
    //    if(myTarget)
    //    {
    //        m_enemySM.ChangeState(m_states[eState.Trace]);
    //    }
    //    else
    //    {
    //        m_enemySM.ChangeState(m_states[eState.Idle]);
    //    }
    //}

    // 쿨타임 체크 함수? 코루틴? 구현 필요

    #endregion


}


