using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Battle : State
{
    float dist; 
    float attackRadiusOffset = 0.05f;

    public EnemyState_Battle(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StartCoroutine(AttackTarget(enemy.myTarget, enemy.leftClawPoint));
    }

    public override void Exit()
    {
        base.Exit();
        enemy.StopAllCoroutines();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dist = Vector3.Distance(enemy.transform.position, enemy.myTarget.transform.position);

        // 거리 체크를 하는 함수를 구현
        if (enemy.myTarget != null && dist > enemy.AttackRange + attackRadiusOffset)
        {
            stateMachine.ChangeState(enemy.m_states[Enemy.eState.Trace]);
        }
        else if (enemy.myTarget == null)
        {
            stateMachine.ChangeState(enemy.m_states[Enemy.eState.Recall]);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public IEnumerator AttackTarget(Transform target, Transform atkPoint)
    {
        while (target != null)
        {
            if (!enemy.myAnim.GetBool("isAttacking"))
            {
                // playTime 을 nextAttackTime으로 변경 필요
                enemy.playTime += Time.deltaTime;
                if (enemy.playTime >= enemy.AttackDelay)
                {
                    enemy.playTime = 0.0f;
                    enemy.myAnim.SetTrigger("Attack");
                    enemy.Attack(atkPoint);
                }
            }
            yield return null;
        }
    }


}
