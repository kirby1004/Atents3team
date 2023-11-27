using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterMovement_V2 : CharacterProperty
{
    #region ����Ƽ �̺�Ʈ �Լ�

    // Awake �Լ� �߰�
    protected virtual void Awake()
    {

    }

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

    #region Attack

    public void Attack(Transform attackPoint)
    {
        Collider[] list = Physics.OverlapSphere(attackPoint.position, AttackRange, enemyLayer); //���� ����Ʈ�� ĳ���� ������ ��ġ�� ������ ����.
                                                                                          //  �� ��ġ�� AttackRange�� ���������� �� �� ������ ����.
                                                                                          // ���� ������ �ݶ��̴����� list�� ����.
        foreach (Collider col in list) //list�� �ݶ��̴����� �ϳ��ϳ� ������ IBattle ������Ʈ�� ������ �ִ��� Ȯ���ϰ� ������ => OnDamage �Լ� ����.
        {
            col.transform.GetComponent<IBattle>()?.OnDamage(AttackPoint);
        }
    }

    //// �÷��̾�� 
    //public abstract void Attack(Transform target = null);


    #endregion



}
