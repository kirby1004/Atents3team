using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState_Idle : State
{
    public MonsterState_Idle(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        monster.myAnim.SetBool("isMoving", false);
        monster.StartCoroutine(Roaming(Random.Range(1.0f, 3.0f)));
    }

    public override void Exit()
    {
        base.Exit();
        monster.StopAllCoroutines();
    }

    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    IEnumerator Roaming(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 pos = monster.orgPos;
        pos.x += Random.Range(-5.0f, 5.0f);
        pos.z += Random.Range(-5.0f, 5.0f);
        monster.MoveToPos(pos, () => monster.StartCoroutine(Roaming(Random.Range(1.0f, 3.0f))));
    }

    
}
