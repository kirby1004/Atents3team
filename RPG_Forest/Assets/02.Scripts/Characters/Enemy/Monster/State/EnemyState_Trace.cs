using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Trace : EnemyState_Idle
{
    public EnemyState_Trace(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StartCoroutine(TracingTarget(enemy.myTarget));
    }

    public override void Exit()
    {
        enemy.StopAllCoroutines();
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

    // TracingTarget으로 이름 변경
    IEnumerator TracingTarget(Transform target) // 타겟을 추적하고 거리가 됫을때 공격하기
    {
        while (target != null)
        {
            if (!enemy.myAnim.GetBool("isAttacking")) enemy.playTime += Time.deltaTime;
            if (!enemy.myAnim.GetBool("isAttacking"))
            {
                enemy.myAnim.SetBool("isMoving", false);
                Vector3 dir = target.position - enemy.transform.position;
                float dist = dir.magnitude - enemy.AttackRange;
                dir.Normalize();
                float delta = 0.0f;

                // MoveToPos
                if (dist > 0.0f)
                {
                    delta = enemy.MoveSpeed * Time.deltaTime;
                    if (dist <= delta)
                    {
                        delta = dist;
                    }
                    enemy.myAnim.SetBool("isMoving", true);
                    enemy.transform.Translate(dir * delta, Space.World);
                }

                // Rotation
                float angle = Vector3.Angle(enemy.transform.forward, dir);
                float rotDir = 1.0f;
                if (Vector3.Dot(enemy.transform.right, dir) < 0.0f)
                {
                    rotDir = -1.0f;
                }
                delta = enemy.RotSpeed * Time.deltaTime;
                if (angle < delta)
                {
                    delta = angle;
                }
                enemy.transform.Rotate(enemy.transform.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }
}
