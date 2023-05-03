using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState_Fly : State
{

    public MonsterState_Fly(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        monster.StopAllCoroutines();
        monster.myAnim.SetTrigger("Fly");
        monster.myAnim.SetBool("isFlying", true);
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
        monster.isFlying = true;
        monster.StartCoroutine(StartFlyingBoost());
    }

    IEnumerator StartFlyingBoost()
    {
        //enemy.startFlyBoosting = false;
        float boostTime = 0f;
        float deltaTime = 0.5f;
        while (boostTime < 1f)
        {
            boostTime += Time.deltaTime;
            monster.flyPos = new Vector3(monster.transform.position.x, monster.flyHeight, monster.transform.position.z);
            monster.transform.position = Vector3.Lerp(monster.transform.position, monster.flyPos, deltaTime * Time.deltaTime);
            yield return null;
        }
        //enemy.startFlyBoosting = true;
    }

}
