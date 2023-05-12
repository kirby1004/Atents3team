using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_Landing : State
{
    Dragon dragon;
    float offset = 5.0f;

    public DragonState_Landing(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
    }

    public override void Enter()
    {
        base.Enter();
        dragon.StopAllCoroutines();
    }

    public override void Exit()
    {
        base.Exit();
        dragon.myAnim.SetBool("isFlying", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (dragon.transform.position.y <= dragon.orgPos.y)
        {
            if(dragon.transform.position.y <= dragon.orgPos.y + offset) dragon.myAnim.SetTrigger("Landing");
            stateMachine.ChangeState(monster.m_states[Dragon.eState.Idle]);
        }
        else
        {
            Vector3 moveDir = -monster.transform.up;
            monster.transform.position += moveDir * dragon.landingSpeed * Time.deltaTime;
        }
    }
}
