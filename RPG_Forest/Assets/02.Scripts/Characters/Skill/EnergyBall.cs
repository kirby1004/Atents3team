using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : Skill
{
    public Transform HitPoint;
    
    private void Awake()
    {
        SetValue(skillData.skillType);
    }

    public override void Use()
    {        


    }

}
