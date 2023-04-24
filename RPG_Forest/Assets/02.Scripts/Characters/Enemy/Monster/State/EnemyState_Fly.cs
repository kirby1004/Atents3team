using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Fly : State
{

    public EnemyState_Fly(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StopAllCoroutines();
        enemy.myAnim.SetTrigger("Fly");
        enemy.myAnim.SetBool("isFlying", true);
        StartFlying();
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

    public void StartFlying()
    {
        enemy.isFlying = true;
        enemy.StartCoroutine(StartFlyingBoost());
    }

    IEnumerator StartFlyingBoost()
    {
        //enemy.startFlyBoosting = false;
        float boostTime = 0f;
        float deltaTime = 0.5f;
        while (boostTime < 1f)
        {
            boostTime += Time.deltaTime;
            enemy.flyPos = new Vector3(enemy.transform.position.x, enemy.flyHeight, enemy.transform.position.z);
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, enemy.flyPos, deltaTime * Time.deltaTime);
            yield return null;
        }
        //enemy.startFlyBoosting = true;
    }

}
