using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterMovement_V2 : CharacterProperty
{
    protected void CoCheckStop(Coroutine coroutine) // 입력받은 코루틴이 동작하고잇는지 확인하고 종료시키기
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    #region 유니티 이벤트 함수

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void LateUpdate()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    #endregion

    #region Move
    // 이동 동작 - 플레이어, 몬스터 둘다 사용
    // 추상 함수로 선언 -> 각 플레이어, 에너미 스크립트에서 override로 정의 필요
    public abstract void MoveToPos(Vector3 pos, UnityAction done = null); // 수민 스크립트의 Move() 함수 변형하면 될 것으로 보임
    #endregion

    #region Enemy - MoveToPos, Rotating 
    /*
     * 에너미 스크립트에서 public override 필요
     * 
    protected void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        CoCheckStop(coMoving);
        coMoving = StartCoroutine(MovingToPos(pos, done));
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

    */
    #endregion


    /// [Summary] Target Tracing과 Attack State 구분 완료

    #region TargetTracing 
    /*
     * Enemy Script로 이동 필요 public overide
     * 유도 스킬 기능(?)을 사용하기위해서 플레이어도 필요한 함수인지 고민 필요?
     * 
    protected void TraceTarget(Transform target)
    {
        CoCheckStop(coFollow);
        coFollow = StartCoroutine(TracingTarget(target));
    }

    // TracingTarget으로 이름 변경
    IEnumerator TracingTarget(Transform target) // 타겟을 추적하고 거리가 됫을때 공격하기
                                                // 추적과 공격 분리 필요 (해결)
    {
        while (target != null)
        {
            if (!myAnim.GetBool("isAttacking")) playTime += Time.deltaTime;
            if (!myAnim.GetBool("isAttacking"))
            {
                myAnim.SetBool("isMoving", false);
                Vector3 dir = target.position - transform.position;
                float dist = dir.magnitude - AttackRange;
                dir.Normalize();
                float delta = 0.0f;

                // MoveToPos
                if (dist > 0.0f)
                {
                    delta = MoveSpeed * Time.deltaTime;
                    if (dist <= delta)
                    {
                        delta = dist;
                    }
                    myAnim.SetBool("isMoving", true);
                    transform.Translate(dir * delta, Space.World);
                }

                // Rotation
                float angle = Vector3.Angle(transform.forward, dir);
                float rotDir = 1.0f;
                if (Vector3.Dot(transform.right, dir) < 0.0f)
                {
                    rotDir = -1.0f;
                }
                delta = RotSpeed * Time.deltaTime;
                if (angle < delta)
                {
                    delta = angle;
                }
                transform.Rotate(transform.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }
    */
    #endregion


    #region Attack

    public void Attack(Transform attackPoint)
    {
        Collider[] list = Physics.OverlapSphere(attackPoint.position, 0.75f, enemyLayer); //웨폰 포인트는 캐릭터 무기의 위치를 가지고 있음.
                                                                                         //  그 위치의 0.75 반지름을 가진 구의 공격범위를 가지고 있다. (나중에 수정 예정)
                                                                                         // overLap에 감지된 콜라이더들을 list에 저장.
        Debug.DrawLine(attackPoint.position, attackPoint.position + new Vector3(0.5f, 0.5f, 0.5f)); //이건 범위를 확인하기 위한 DrawLine (나중에 지울 예정)
        foreach (Collider col in list) //list의 콜라이더들을 하나하나 꺼내서 IBattle 컴포넌트를 가지고 있는지 확인하고 맞으면 => OnDamage 함수 실행.
        {
            col.transform.GetComponent<IBattle>()?.OnDamage(AttackPoint);
        }
    }

    //// 플레이어는 
    //public abstract void Attack(Transform target = null);
    

    IEnumerator AttackTarget(Transform target)
    {
        while (target != null)
        {
            if (!myAnim.GetBool("isAttacking"))
            {

                if (playTime >= AttackDelay)
                {
                    playTime = 0.0f;
                    myAnim.SetTrigger("Attacking");
                }
            }
        }
        yield return null;
    }
    #endregion


    #region 리콜 state 
    /*
     *  리콜 -> 에너미한테만 필요함 몬스터로 이동하여서 몬스터 스테이트머신 Recall State에 protected void 함수로 선언
     *  에너미스크립트에서 public override 필요
     * 
    protected void RecallToPos(Vector3 pos, UnityAction done = null)
    {
        CoCheckStop(coMoving);
        CoCheckStop(coFollow);
        coRecall = StartCoroutine(RecallBack(pos, done));
        //myAnim.SetBool("isMoving", true);
        //myAnim.ResetTrigger("isAttack");
        curHp = MaxHp;
    }
    

    protected IEnumerator RecallBack(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        myAnim.SetBool("isMoving", true);

        while (dist > 0.0f)
        {

            float delta = MoveSpeed * Time.deltaTime * 2;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            curHp = MaxHp;

            yield return null;
        }

        //myAnim.SetBool("isMoving", true);
        //myAnim.ResetTrigger("isAttack");
        //curHp = MaxHp;
        //coMoving = StartCoroutine(MovingToPos(pos, done));

        done?.Invoke();
    }
    */
    #endregion


}
