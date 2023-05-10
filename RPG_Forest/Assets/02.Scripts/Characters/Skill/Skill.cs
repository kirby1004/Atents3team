using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected SkillData _skillData;
    public SkillData skillData { get { return _skillData; }  set { _skillData = value;  } }

    protected float AttackDamage;
    protected float ccTime;
    protected float BuffStatus;
    protected float BuffTime;

    protected void SetValue(SkillType type)
    {
        switch (type)
        {
            case SkillType.Attack:
                AttackDamage = skillData.SkillValue1;
                ccTime = skillData.SkillValue2;
                break;
            case SkillType.Buff:
                BuffStatus = skillData.SkillValue1;
                BuffTime = skillData.SkillValue2;
                break;

        }
    }

    public abstract void Use();
}
