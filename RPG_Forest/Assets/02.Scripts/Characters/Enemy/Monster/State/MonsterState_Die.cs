using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterState_Die : State
{
    public MonsterState_Die(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
    }

    public override void Enter()
    {
        monster.StopAllCoroutines();
        Transform transform = monster.transform;
        monster.ColDelete += DisableCollider;
        monster.myAnim.SetTrigger("Die");
        LootingManager.Inst.ReadyLootWindow(monster);
        //Object.Destroy(transform.GetComponentInChildren<AIPerception>());
        monster.myAI.AddComponent<LootingPerception>();
        monster.DeathAlarm?.Invoke();
        //monster.OnDisappear();
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
