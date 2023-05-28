using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_BattleDragon : State
{
    Dragon dragon;
    private IEnumerator attackCoroutine;    // ���� ������ ���� �ڷ�ƾ

    float dist;
    float attackRadiusOffset = 0.05f;

    public DragonState_BattleDragon(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
        attackCoroutine = null;
    }

    public override void Enter()
    {
        base.Enter();
        dragon.StopAllCoroutines();
        Debug.Log("DragonBattleState");
        if (check != null) check = dragon.StartCoroutine(attackCoroutine);
    }

    public override void Exit()
    {
        base.Exit();
        dragon.StopCoroutine(attackCoroutine);
    }

    Coroutine check = null;         // �̹� ���� ���� ���� ������ Ȯ���ϴ� �ڷ�ƾ

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dist = Vector3.Distance(dragon.transform.position, dragon.myTarget.transform.position);

        // �Ÿ� üũ�� �ϴ� �Լ��� ����
        if (!dragon.isFlying && dragon.myTarget != null && dist > dragon.AttackRange + attackRadiusOffset)
        {
            stateMachine.ChangeState(dragon.m_states[Dragon.eState.Trace]);
        }

        // ���� ü�¿� ����ؼ� ���� ���� �б�
        if (dragon.curHp < dragon.MaxHp * 0.2f && !dragon.isBerserk)
        {
            Debug.Log("Test");
            dragon.isBerserk = true;
            stateMachine.ChangeState(dragon.m_states[Dragon.eState.Fly]);
        }
        else if (check == null)
        {
            attackCoroutine = dragon.pattern.DoAttackPattern();
            check = dragon.StartCoroutine(attackCoroutine);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // ���� �þ� �ȿ� �����ִ� �� üũ
        if (!IsTargetOnSight(dragon.myTarget))
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);

            dragon.myAnim.SetBool("isMoving", true);

            delta = monster.RotSpeed * Time.deltaTime;
            monster.transform.rotation = Quaternion.Slerp(dragon.transform.rotation, targetRotation, delta / 120.0f);
        }
        else
        {
            dragon.myAnim.SetBool("isMoving", false);
        }
    }

    Vector3 dir;        // �巡��� Ÿ�� ������ ����
    float delta;        // ȸ�� Slerp�� ��Ÿ��

    // Ÿ���� �þ߰Ÿ� �ȿ� ���� �ִ��� üũ
    bool IsTargetOnSight(Transform target)
    {
        dir = target.position - dragon.transform.position;

        if (Vector3.Angle(dir, dragon.transform.forward) > dragon.fieldOfView * 0.5f)
        {
            return false;
        }

        if(Physics.Raycast(dragon.transform.position, dir, out RaycastHit hit, dragon.viewDistance, dragon.enemyLayer))
        {
            if (hit.transform == dragon.myTarget) return true;
        }
        return false;
    }


}
