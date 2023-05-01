using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Recall : State
{
    public EnemyState_Recall(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.MoveToPos(enemy.orgPos, () =>
        {
            if (!enemy.myAnim.GetBool("isAttacking")) stateMachine.ChangeState(enemy.m_states[Enemy.eState.Idle]);
        });
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
}
