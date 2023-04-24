using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : State
{
    public EnemyState_Idle(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        enemy.myAnim.SetBool("isMoving", false);
        enemy.StartCoroutine(Roaming(Random.Range(1.0f, 3.0f)));
    }

    public override void Exit()
    {
        base.Exit();
        enemy.StopAllCoroutines();
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
        Vector3 pos = enemy.orgPos;
        pos.x += Random.Range(-5.0f, 5.0f);
        pos.z += Random.Range(-5.0f, 5.0f);
        enemy.MoveToPos(pos, () => enemy.StartCoroutine(Roaming(Random.Range(1.0f, 3.0f))));
    }

    
}
