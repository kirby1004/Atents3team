using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState_Battle : State
{
    float dist; 
    float attackRadiusOffset = 0.05f;

    public MonsterState_Battle(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //monster.StartCoroutine() // ���� ���� �ʿ�
        monster.StartCoroutine(AttackTarget(monster.myTarget, monster.leftClawPoint));
    }

    public override void Exit()
    {
        base.Exit();
        monster.StopAllCoroutines();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dist = Vector3.Distance(monster.transform.position, monster.myTarget.transform.position);

        

        // �Ÿ� üũ�� �ϴ� �Լ��� ����
        if (monster.myTarget != null && dist > monster.AttackRange + attackRadiusOffset)
        {
            stateMachine.ChangeState(monster.m_states[Monster.eState.Trace]);
        }
        else if (monster.myTarget == null)
        {
            stateMachine.ChangeState(monster.m_states[Monster.eState.Recall]);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public IEnumerator AttackTarget(Transform target, Transform atkPoint = null)
    {
        while (target != null)
        {
            if (!monster.myAnim.GetBool("isAttacking"))
            {
                // playTime �� nextAttackTime���� ���� �ʿ�
                monster.playTime += Time.deltaTime;
                if (monster.playTime >= monster.AttackDelay)
                {
                    monster.playTime = 0.0f;
                    monster.myAnim.SetTrigger("Attack");
                    monster.Attack(atkPoint);
                }
            }
            yield return null;
        }
    }


}
