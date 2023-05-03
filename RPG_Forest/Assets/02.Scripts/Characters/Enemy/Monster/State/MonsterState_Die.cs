using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState_Die : State
{
    public MonsterState_Die(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
    }

    public override void Enter()
    {
        DisableCollider();
        monster.DeathAlarm?.Invoke();
        monster.StopAllCoroutines();
        monster.myAnim.SetTrigger("Die");
        base.Enter();
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

    void DisableCollider()
    {
        Collider[] list = monster.transform.GetComponentsInChildren<Collider>();
        foreach (Collider col in list) col.enabled = false;
    }

}
