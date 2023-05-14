using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_Bite : AttackPhase
{
    //Transform target;
    Transform atkPoint;
    float atkDelay;

    public DragonState_Bite(Dragon dragon) : base(dragon)
    {
        this.atkPoint = dragon.headPoint;
        this.atkDelay = dragon.AttackDelay;
    }

    public override IEnumerator DoPhase()
    {
        //if (dragon.myTarget != null)
        //{
        //    this.target = dragon.myTarget;
        //    this.atkPoint = dragon.headPoint;
        //}

        atkDelay = 10.0f;                           // 이런식으로 짜면 왼팔 공격 Delay가 5초인 경우 왼팔공격만 들어오게 됨...
                                                    // Phase와 Phase 사이에 딜레이를 주는 방식으로 짜야하지만 WaitForSecond가 먹히지 않음...

        while (dragon.myTarget != null)
        {
            if (!dragon.myAnim.GetBool("isAttacking"))
            {
                // playTime 을 nextAttackTime으로 변경 필요
                dragon.playTime += Time.deltaTime;
                if (dragon.playTime >= atkDelay)
                {
                    dragon.playTime = 0.0f;
                    dragon.myAnim.SetTrigger("Bite");
                    Debug.Log("물기");
                    dragon.Attack(atkPoint);
                }
            }
            yield return new WaitForSeconds(10.0f);
        }
    }
}
