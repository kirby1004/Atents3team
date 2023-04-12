using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Monster : CharacterMovement, IPerception, IBattle, I_Ad
{
    public static int TotalCount = 0;
    public enum State // Recall State 추가
    {
        Create, Normal, Battle, Death , Recall
    }
    public State myState = State.Create;

    protected Transform Spawn; // 복귀지점을 잡기위하여 시작지점 입력받기
    protected Vector3 orgPos;
    public Transform myTarget = null;
    public Transform myReturn = null;

    Coroutine coRoaming = null;
    //Coroutine coFollow = null;
    Coroutine coRecall = null;

    public bool IsLive
    {
        get => myState != State.Death;
    }
    public void StateChange(State s) // 현재상태에서 S 로 상태 변화
    {
        if (myState == s) return;
        ExitState(myState);
        EnterState(s);
    }

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case State.Normal:
                myAnim.SetBool("isMoving", false);
                coCheckStop(coRoaming);
                coRoaming = StartCoroutine(Roaming(Random.Range(1.0f, 3.0f)));
                break;
            case State.Battle:                
                StopAllCoroutines();
                FollowTarget(myTarget);
                break;
            case State.Death:
                Collider[] list = transform.GetComponentsInChildren<Collider>();
                foreach (Collider col in list) col.enabled = false;
                DeathAlarm?.Invoke();
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
                break;
            case State.Recall:
                StopAllCoroutines();
                MoveToPos(myReturn.position);
                break;
            default:
                Debug.Log("처리 되지 않는 상태 입니다.");
                break;
        }
    }

    void EnterState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Create:
                //정보 로딩
                break;
            case State.Normal:
                coCheckStop(coRoaming);
                myAnim.SetBool("isMoving", false);
                coRoaming = StartCoroutine(Roaming(Random.Range(1.0f, 3.0f)));
                break;
            case State.Battle:
                
                StopAllCoroutines();
                FollowTarget(myTarget);
                break;
            case State.Death: // 충돌 판정 제거 , 사망 알람 전달 , 사망 애니메이션 동작 
                // 충돌 판정 제거
                Collider[] list = transform.GetComponentsInChildren<Collider>();
                foreach (Collider col in list) col.enabled = false; 
                // 사망알람 전달
                DeathAlarm?.Invoke();
                //동작중인 움직임 및 판정 제거
                StopAllCoroutines();          
                // 사망 애니메이션 동작
                myAnim.SetTrigger("Dead");
                break;
            case State.Recall:
                break;
            default:
                break;
        }
    }
    void ExitState(State S)
    {
        switch (myState) 
        {
            case State.Create:
            break;
            case State.Normal: 
                // 로밍상태 해제
                coCheckStop(coRoaming);
                // 이동 애니메이션 제거
                myAnim.SetBool("isMoving", false);
                break;
            case State.Battle:
                StopAllCoroutines();
                break;
            case State.Death: // 일정시간 경과 및 루팅이 끝낫을때 사체 소멸시키기
                OnDisappear(); 
            break;
            case State.Recall:
                break;
            default:
            break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                break;
            case State.Battle:
                break;
            case State.Death:
                break;
            case State.Recall:

                break;
            default:
                Debug.Log("처리 되지 않는 상태 입니다.");
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TotalCount++;
        orgPos = transform.position;
        Spawn = Gamemanager.Instance.mySpawnner.transform;
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    IEnumerator Roaming(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 pos = orgPos;
        pos.x += Random.Range(-5.0f, 5.0f);
        pos.z += Random.Range(-5.0f, 5.0f);
        MoveToPos(pos, ()=> StartCoroutine(Roaming(Random.Range(1.0f,3.0f))));
    }

    public void Find(Transform target)
    {
        myTarget = target;
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => { if (IsLive) ChangeState(State.Normal); };
        ChangeState(State.Battle);
    }

    public void LostTarget()
    {
        myTarget = null;
        Rangeout(Spawn);
       // ChangeState(State.Recall);
    }

    public void OnAttack()
    {
        myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);
    }

    public void OnDamage(float dmg)
    {
        curHp -= dmg;
        if (Mathf.Approximately(curHp, 0.0f))
        {
            ChangeState(State.Death);            
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }

    public void OnDisappear()
    {
        StartCoroutine(Disappearing());
    }

    IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(3.0f);
        float dist = 0.0f;
        while(dist < 1.0f)
        {
            dist += Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
        TotalCount--;
    }

    public void Rangeout(Transform target)
    {
        if (transform == target)
        {
            ChangeState(State.Normal);
            return;
        }
        myReturn = target;
        ChangeState(State.Recall);
    }

}
