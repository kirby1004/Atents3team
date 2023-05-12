using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_BattleDragon : State
{
    Dragon dragon;

    IEnumerator attackCoroutine;

    float dist;
    float attackRadiusOffset = 0.05f;


    public DragonState_BattleDragon(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
        
    }

    public override void Enter()
    {
        base.Enter();
        dragon.StopAllCoroutines();
        Debug.Log("DragonBattleState");

        //dragon.StartCoroutine(pattern.DoAttackPattern());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dist = Vector3.Distance(dragon.transform.position, dragon.myTarget.transform.position);

        // �Ÿ� üũ�� �ϴ� �Լ��� ����
        if (dragon.myTarget != null && dist > dragon.AttackRange + attackRadiusOffset)
        {
            stateMachine.ChangeState(dragon.m_states[Dragon.eState.Trace]);
        }
        
        if(dragon.curHp < dragon.MaxHp * 0.2f)
        {
            stateMachine.ChangeState(dragon.m_states[Dragon.eState.Fly]);
        }
        else if(attackCoroutine == null || !attackCoroutine.MoveNext())
        {
            attackCoroutine = dragon.pattern.DoAttackPattern();
            dragon.StartCoroutine(attackCoroutine);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
