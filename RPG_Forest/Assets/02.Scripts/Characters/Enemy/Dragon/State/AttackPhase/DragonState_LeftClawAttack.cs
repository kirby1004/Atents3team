using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_LeftClawAttack : AttackPhase
{
    //Transform target;
    Transform atkPoint;

    public DragonState_LeftClawAttack(Dragon dragon) : base(dragon) 
    {
        this.atkPoint = dragon.leftClawPoint;
    }

    public override IEnumerator DoPhase()
    {
        //if (dragon.myTarget != null)
        //{
        //    this.target = dragon.myTarget;
        //    this.atkPoint = dragon.leftClawPoint;
        //}
        float delayTime = 2.0f;
        while (dragon.myTarget != null)
        {
            if (!dragon.myAnim.GetBool("isAttacking"))
            {
                // playTime �� nextAttackTime���� ���� �ʿ�
                dragon.playTime +=  Time.deltaTime;
                if (dragon.playTime >= dragon.AttackDelay)
                {
                    dragon.playTime = 0.0f;
                    dragon.myAnim.SetTrigger("Attack");
                    Debug.Log("���Ȱ���");
                    dragon.Attack(atkPoint);
                    yield break;
                }
            }
            yield return new WaitForSeconds(delayTime);
            dragon.playTime += delayTime;
        }
    }

}
