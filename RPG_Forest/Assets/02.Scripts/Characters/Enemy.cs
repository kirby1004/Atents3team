using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ���ʹ� ��ũ��Ʈ ������Ʈ�ӽŰ� ���� ���� �и��ϱ� 
public class Enemy : CharacterMovement_V2
{
    //private StateMachine enemySM;

    // Dictionary<States, StateMachine>

    protected override void Start()
    {
        //enemySM = new StateMachine();    

        //var idleState = new IdleState(enemySM, this);
    }

    protected override void Update()
    {
        
    }

    public override void Attack(Transform target = null)
    {
        throw new System.NotImplementedException();
    }

    public override void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        throw new System.NotImplementedException();
    }
}
