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
        dragon.StartCoroutine(SpitFire());
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

    IEnumerator SpitFire()
    {
        while(dragon.spitFireCnt < 5)
        {
            dragon.spitFireCnt++;
            dragon.myAnim.SetTrigger("FlySpitFire");
            Debug.Log($"{dragon.spitFireCnt}");
            // ½ºÅ³ ÀÌÆåÆ® 
            //GameObject fireEffect = ObjectPoolManager.Instance.GetObject("FireEffect", dragon.firePoint.position, dragon.firePoint.rotation);
            //fireEffect.SetActive(true);
            yield return new WaitForSeconds(dragon.spitFireDelay);
        }
        stateMachine.ChangeState(dragon.m_states[Dragon.eState.Landing]);
    }

}
