using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    public Transform headPoint;
    public DragonAttackPattern pattern;

    public bool isBerserk = false;                 // ±¤ÆøÈ­

    protected override void Awake()
    {
        pattern = new DragonAttackPattern(this);

        canFly = true;
        

    }

    protected override void Start()
    {
        base.Start();
        m_states.Add(eState.Fly, new DragonState_Fly(this, m_monsterSM));
        m_states.Add(eState.FlySpitFire, new DragonState_FlySpitFire(this, m_monsterSM));
        m_states.Add(eState.Landing, new DragonState_Landing(this, m_monsterSM));
        m_states.Add(eState.BattleDragon, new DragonState_BattleDragon(this, m_monsterSM));


        AttackRange = 5.1f;
        //m_monsterSM.ChangeState(m_states[eState.Create]);
        //StartCoroutine(TestLanding());
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

    #region

    public override void OnCreate()
    {

    }
    #endregion

    #region Find

    public override void Find(Transform target)
    {
        myTarget = target;
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += () => { if (IsLive) m_monsterSM.ChangeState(m_states[eState.Idle]); }; // Death Alarm ÈÄ¿¡ ±¸Çö
        if(!isFlying) m_monsterSM.ChangeState(m_states[eState.Trace]);
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
    public float flyHeight = 20.0f;
    public float landingDuration = 1.0f;
    public float spitFireCnt;
    public float spitFireDelay = 3.0f;

    IEnumerator TestLanding()
    {
        yield return new WaitForSeconds(2.0f);
        m_monsterSM.ChangeState(m_states[Dragon.eState.FlySpitFire]);
        yield return new WaitForSeconds(5.0f);
        m_monsterSM.ChangeState(m_states[Dragon.eState.Landing]);
        yield return null;
    }

    
    #endregion
}
