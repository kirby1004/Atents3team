using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    public Transform headPoint;
    public DragonAttackPattern pattern;

    public bool isBerserk = false;                 // 광폭화

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

    #region OnCreate

    public Transform devilEye;

    public override void OnCreate()
    {
        SkillManager.instance.RegisterSkill(MonsterSkillName.DevilEye, devilEye);

        //GameObject obj = (GameObject)Instantiate(Resources.Load("DevilEye"));
        //obj.SetActive(true);
        //Destroy(obj, 5.0f);
    }
    #endregion

    #region Find

    public override void Find(Transform target)
    {
        myTarget = target;
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => { if (IsLive) m_monsterSM.ChangeState(m_states[eState.Idle]); }; // Death Alarm 후에 구현
        if(!isFlying && (m_monsterSM.CurrentState != m_states[eState.Create])) m_monsterSM.ChangeState(m_states[eState.Trace]);
    }
    #endregion

    #region Battle

    public override void OnBattle()
    {
        m_monsterSM.ChangeState(m_states[Dragon.eState.BattleDragon]);
    }

    #endregion

    #region Fly

    public Vector3 flyPos;
    public bool isFlying = false;
    public float flyHeight = 25.0f;
    public float flySpeed = 7.0f;
    public float landingDuration = 4.0f;
    public float spitFireCnt;
    public float spitFireDelay = 2.0f;

    public Transform flyToBackPos;          // 뒤로 날 지점 
    
    #endregion
}
