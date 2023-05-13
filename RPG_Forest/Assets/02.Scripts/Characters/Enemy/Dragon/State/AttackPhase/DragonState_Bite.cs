using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_Bite : AttackPhase
{
    //Transform target;
    //Transform atkPoint;

    public DragonState_Bite(Dragon dragon) : base(dragon)
    {
    //    this.target = dragon.myTarget;
    //    this.atkPoint = dragon.headPoint;
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
                    dragon.myAnim.SetTrigger("Bite");
                    dragon.Attack(dragon.headPoint);
                }
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
}
