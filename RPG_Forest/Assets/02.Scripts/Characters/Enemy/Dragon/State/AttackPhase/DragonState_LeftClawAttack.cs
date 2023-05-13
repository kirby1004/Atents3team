using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_LeftClawAttack : AttackPhase
{
    //Transform target;
    //Transform atkPoint;

    public DragonState_LeftClawAttack(Dragon dragon) : base(dragon) 
    {
        //this.target = dragon.myTarget;
        //this.atkPoint = dragon.leftClawPoint; 
    }

    public override IEnumerator DoPhase()
    {
        while (dragon.myTarget != null)
        {
            if (!dragon.myAnim.GetBool("isAttacking"))
            {
                // playTime 을 nextAttackTime으로 변경 필요
                dragon.playTime += Time.deltaTime;
                if (dragon.playTime >= dragon.AttackDelay)
                {
                    dragon.playTime = 0.0f;
                    dragon.myAnim.SetTrigger("Attack");
                    dragon.Attack(dragon.leftClawPoint);
                }
            }
            yield return new WaitForSeconds(3.0f);
        }
    }

}
