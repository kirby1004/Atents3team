using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_Bite : AttackPhase
{
    //Transform target;
    Transform atkPoint;
    //float atkDelay;

    public DragonState_Bite(Dragon dragon) : base(dragon)
    {
        this.atkPoint = dragon.headPoint;
        //this.atkDelay = dragon.AttackDelay;
    }

    public override IEnumerator DoPhase()
    {
        
        float delayTime = 3.0f;
        while (dragon.myTarget != null)
        {
            if (!dragon.myAnim.GetBool("isAttacking"))
            {
                // playTime �� nextAttackTime���� ���� �ʿ�
                dragon.playTime += Time.deltaTime;
                if (dragon.playTime >= dragon.AttackDelay)
                {
                    dragon.playTime = 0.0f;
                    dragon.myAnim.SetTrigger("Bite");
                    Debug.Log("����");
                    dragon.Attack(atkPoint);
                    yield break;
                }
            }
            yield return new WaitForSeconds(delayTime);
            dragon.playTime += delayTime;
        }
    }
}
