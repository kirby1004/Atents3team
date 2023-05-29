using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dragon : Monster
{
    public Transform headPoint;
    public Transform rightClawPoint;
    public DragonAttackPattern pattern;

    public bool isBerserk = false;                 // ����ȭ


    protected override void Awake()
    {
        pattern = new DragonAttackPattern(this);

        canFly = true;
    }

    protected override void Start()
    {
        base.Start();
        m_states.Add(eState.Fly, new DragonState_Fly(this, m_monsterSM));
        m_states.Add(eState.FlyToBackPos, new DragonState_FlyToBackPos(this, m_monsterSM));
        m_states.Add(eState.FlySpitFire, new DragonState_FlySpitFire(this, m_monsterSM));
        m_states.Add(eState.Landing, new DragonState_Landing(this, m_monsterSM));
        m_states.Add(eState.BattleDragon, new DragonState_BattleDragon(this, m_monsterSM));

        GameObject obj = Instantiate(Resources.Load("UIResource/System/BossHpHigh"), UIManager.instance.BossHPBar) as GameObject;
        AttackRange = 5.1f;

    }

    protected override void Update()
    {
        base.Update();
        m_monsterSM.CurrentState.LogicUpdate();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        m_monsterSM.CurrentState.PhysicsUpdate();
    }

    #region OnCreate - Create State�� �巡�� �ߵ� �Լ� �������̵�

    public override void OnCreate()
    {
        StartCoroutine(EncounterCutScene());
    }

    IEnumerator EncounterCutScene()
    {
        Vector3 pos = transform.position + new Vector3(0, 13, 0);
        ObjectPoolingManager.instance.GetObject("DevilEye", pos, Quaternion.identity, 5.0f);
        yield return new WaitForSeconds(3.5f);
        m_monsterSM.ChangeState(m_states[eState.Idle]);
    }
    #endregion

    #region Find

    public override void Find(Transform target)
    {
        myTarget = target;
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => { if (IsLive) m_monsterSM.ChangeState(m_states[eState.Idle]); }; // Death Alarm �Ŀ� ����
        if(!isFlying && (m_monsterSM.CurrentState != m_states[eState.Create])) m_monsterSM.ChangeState(m_states[eState.Trace]);         // �������� �ʰ�, ���� ���� Create �ƴ� ���� Trace ���·� õ��
    }
    #endregion

    #region Battle

    public float fieldOfView = 30f;
    public float viewDistance = 10.0f;

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, AttackRange);

        var leftRayRotation = Quaternion.AngleAxis(-fieldOfView * 0.5f, Vector3.up);
        var leftRayDirection = leftRayRotation * transform.forward;
        Handles.color = new Color(1f, 1f, 1f, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, leftRayDirection, fieldOfView, viewDistance);
    }

#endif

    public override void OnBattle()
    {
        m_monsterSM.ChangeState(m_states[Dragon.eState.BattleDragon]);      // Dragon�� BattleDragon - AI ��ũ��Ʈ�� �������̵� �Ͽ� ���
    }

    #endregion

    #region Fly

    public Vector3 flyPos;
    public bool isFlying = false;
    public float flyHeight = 25.0f;
    public float flySpeed = 7.0f;
    public float landingDuration = 4.0f;
    public float spitFireCnt= 0;
    public float spitFireDelay = 0.6f;

    public Transform flyToBackPos;          // �ڷ� �� ���� 
    public Transform spitFirePos;

    #endregion

    #region Die
    // �巡�� �ƾ� ������ ���� OnDie virtaul �Լ� overriding
    public override void OnDie()
    {
        SceneLoader.Inst.SceneLoadAdditive(3);
    }

    #endregion
}
