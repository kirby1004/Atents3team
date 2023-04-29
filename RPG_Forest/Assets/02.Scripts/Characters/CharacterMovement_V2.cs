using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterMovement_V2 : CharacterProperty
{
    protected void CoCheckStop(Coroutine coroutine) // �Է¹��� �ڷ�ƾ�� �����ϰ��մ��� Ȯ���ϰ� �����Ű��
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    #region ����Ƽ �̺�Ʈ �Լ�

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
    // �̵� ���� - �÷��̾�, ���� �Ѵ� ���
    // �߻� �Լ��� ���� -> �� �÷��̾�, ���ʹ� ��ũ��Ʈ���� override�� ���� �ʿ�
    public abstract void MoveToPos(Vector3 pos, UnityAction done = null); // ���� ��ũ��Ʈ�� Move() �Լ� �����ϸ� �� ������ ����
    #endregion



    /// [Summary] Target Tracing�� Attack State ���� �Ϸ�

    #region TargetTracing 
    /*
     * Enemy Script�� �̵� �ʿ� public overide
     * ���� ��ų ���(?)�� ����ϱ����ؼ� �÷��̾ �ʿ��� �Լ����� ��� �ʿ�?
     * 
    protected void TraceTarget(Transform target)
    {
        CoCheckStop(coFollow);
        coFollow = StartCoroutine(TracingTarget(target));
    }

    // TracingTarget���� �̸� ����
    IEnumerator TracingTarget(Transform target) // Ÿ���� �����ϰ� �Ÿ��� ������ �����ϱ�
                                                // ������ ���� �и� �ʿ� (�ذ�)
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
        Collider[] list = Physics.OverlapSphere(attackPoint.position, 0.75f, enemyLayer); //���� ����Ʈ�� ĳ���� ������ ��ġ�� ������ ����.
                                                                                         //  �� ��ġ�� 0.75 �������� ���� ���� ���ݹ����� ������ �ִ�. (���߿� ���� ����)
                                                                                         // overLap�� ������ �ݶ��̴����� list�� ����.
        Debug.DrawLine(attackPoint.position, attackPoint.position + new Vector3(0.5f, 0.5f, 0.5f)); //�̰� ������ Ȯ���ϱ� ���� DrawLine (���߿� ���� ����)
        foreach (Collider col in list) //list�� �ݶ��̴����� �ϳ��ϳ� ������ IBattle ������Ʈ�� ������ �ִ��� Ȯ���ϰ� ������ => OnDamage �Լ� ����.
        {
            col.transform.GetComponent<IBattle>()?.OnDamage(AttackPoint);
        }
    }

    //// �÷��̾�� 
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


    #region ���� state 
    /*
     *  ���� -> ���ʹ����׸� �ʿ��� ���ͷ� �̵��Ͽ��� ���� ������Ʈ�ӽ� Recall State�� protected void �Լ��� ����
     *  ���ʹ̽�ũ��Ʈ���� public override �ʿ�
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
