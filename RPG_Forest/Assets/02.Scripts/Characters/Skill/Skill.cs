using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void Use()
    {

    }
}

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected SkillData skillData;

    [SerializeField]
    protected Transform HitPoint;
}
