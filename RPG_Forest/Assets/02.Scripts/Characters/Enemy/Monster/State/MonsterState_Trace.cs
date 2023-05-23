using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState_Trace : State
{
    public MonsterState_Trace(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        monster.StartCoroutine(TracingTarget(monster.myTarget));
    }

    public override void Exit()
    {
        monster.StopAllCoroutines();
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

    IEnumerator TracingTarget(Transform target) // Ÿ���� �����ϰ� �Ÿ��� ������ �����ϱ�
    {
        while (target != null)
        {
            if (!monster.myAnim.GetBool("isAttacking"))
            {
                monster.myAnim.SetBool("isMoving", false);
                Vector3 dir = target.position - monster.transform.position;
                float dist = dir.magnitude - monster.AttackRange;
                dir.Normalize();
                float delta;

                // MoveToPos
                if (dist > 0.0f)
                {
                    delta = monster.MoveSpeed * Time.deltaTime;
                    if (dist <= delta)
                    {
                        delta = dist;
                    }
                    monster.myAnim.SetBool("isMoving", true);
                    monster.transform.Translate(dir * delta, Space.World);
                }
                else
                {
                    monster.OnBattle();
                }

                // Rotation
                // ȸ�� �� Jittering ������ �������� LookRotation�� Quaternion.Slerp �޼��带 �̿�
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                delta = monster.RotSpeed * Time.deltaTime;
                monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, targetRotation, delta / 120.0f);

            }
            yield return null;
        }
    }
}
