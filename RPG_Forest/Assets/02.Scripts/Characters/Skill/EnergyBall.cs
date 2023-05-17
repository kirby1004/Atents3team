using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Rendering;
using UnityEngine;

public class EnergyBall : Skill,ISkill
{
    [field:SerializeField]
    public SkillData skillData
    {
        get;set;
    }

    public float dist;
    public void Awake()
    {
        dist = skillData.Distance;
    }
    public void Use()
    {
            StartCoroutine(Using());
    }

    IEnumerator Using()
    {
        while (dist > 0.0f)
        {
            float delta = skillData.Speed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(transform.forward * delta, Space.World);
            yield return null;
        }
        dist = skillData.Distance;
        ObjectPoolingManager.instance.ReturnObject(gameObject);
    }
}
