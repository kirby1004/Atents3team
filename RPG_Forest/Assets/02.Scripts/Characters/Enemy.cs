using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 에너미 스크립트 스테이트머신과 패턴 전부 분리하기 
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
