using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    protected override void Start()
    {
        base.Start();
        m_states.Add(eState.Fly, new DragonState_Fly(this, m_monsterSM));

        //m_monsterSM.ChangeState(m_states[eState.Fly]);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    #region Fly

    public Vector3 flyPos;
    public bool isFlying = false;
    public float flyHeight = 10.0f;


    //public bool startFlyBoosting = false;

    #endregion
}
