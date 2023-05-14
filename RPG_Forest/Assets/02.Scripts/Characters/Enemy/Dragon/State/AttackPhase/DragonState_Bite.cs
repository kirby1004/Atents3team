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

        atkDelay = 10.0f;                           // �̷������� ¥�� ���� ���� Delay�� 5���� ��� ���Ȱ��ݸ� ������ ��...
                                                    // Phase�� Phase ���̿� �����̸� �ִ� ������� ¥�������� WaitForSecond�� ������ ����...

        while (dragon.myTarget != null)
        {
            if (!dragon.myAnim.GetBool("isAttacking"))
            {
                // playTime �� nextAttackTime���� ���� �ʿ�
                dragon.playTime += Time.deltaTime;
                if (dragon.playTime >= atkDelay)
                {
                    dragon.playTime = 0.0f;
                    dragon.myAnim.SetTrigger("Bite");
                    Debug.Log("����");
                    dragon.Attack(atkPoint);
                }
            }
            yield return new WaitForSeconds(10.0f);
        }
    }
}
