using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : CharacterProperty
{
    Coroutine coMoving;
    Coroutine coFollow;
    Coroutine coRecall;

    public void coCheckStop(Coroutine coroutine) // 입력받은 코루틴이 동작하고잇는지 확인하고 종료시키기
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    protected void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        coCheckStop(coMoving);
        coMoving = StartCoroutine(MovingToPos(pos, done));
    }

    protected void FollowTarget(Transform target)
    {
        coCheckStop(coFollow);
        coFollow = StartCoroutine(FollowingTarget(target));
    }

    protected void RecallToPos(Vector3 pos, UnityAction done = null) 
    { 
        coCheckStop(coMoving);
        coCheckStop(coFollow);
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

    Coroutine myCombo = null;
    protected IEnumerator MovingToPos(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        StartCoroutine(Rotating(dir));

        myAnim.SetBool("isMoving", true);

        while(dist > 0.0f)
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
        if(Vector3.Dot(transform.right,dir) < 0.0f)
        {
            rotDir = -1.0f;
        }
        while(angle > 0.0f)
        {
            float delta = RotSpeed * Time.deltaTime;
            if(angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;
        }
    }

    public float ComboCount = 0.0f;

    IEnumerator FollowingTarget(Transform target) // 타겟을 추적하고 거리가 됫을때 공격하기
        // 추적과 공격 분리 필요
    {
        while(target != null)
        {            
            if(!myAnim.GetBool("isAttacking")) playTime += Time.deltaTime;
            if (!myAnim.GetBool("isAttacking"))
            {
                myAnim.SetBool("isMoving", false);
                Vector3 dir = target.position - transform.position;
                float dist = dir.magnitude - AttackRange;                
                dir.Normalize();
                float delta = 0.0f;

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
                else
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
                
                //Rotation
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

    protected void TargetAttack()
    {
        if (myAnim.GetBool("isAttacking")) 
        {

        }
    }

}
