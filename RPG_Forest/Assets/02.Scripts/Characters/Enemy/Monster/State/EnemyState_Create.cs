using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Create : State
{
    public EnemyState_Create(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //stateMachine.ChangeState(enemy.m_states[Enemy.eState.Idle]);
        enemy.StartCoroutine(CreateDelay());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    IEnumerator CreateDelay()
    {
        yield return new WaitForSeconds(2.0f);
        stateMachine.ChangeState(enemy.m_states[Enemy.eState.Trace]);
    }

}
