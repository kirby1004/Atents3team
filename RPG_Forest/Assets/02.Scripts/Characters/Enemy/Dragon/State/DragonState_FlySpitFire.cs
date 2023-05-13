using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_FlySpitFire : State
{
    Dragon dragon;

    public DragonState_FlySpitFire(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
    }

    public override void Enter()
    {
        base.Enter();
        dragon.StopAllCoroutines();
        dragon.myAnim.SetTrigger("FlySpitFire");
    }

    public override void Exit()
    {
        base.Exit();
        //stateMachine.ChangeState(monster.m_states[Dragon.eState.Fly]);
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
