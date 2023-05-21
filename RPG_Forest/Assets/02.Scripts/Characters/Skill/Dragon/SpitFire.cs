using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitFire : Skill, ISkill
{
    [field:SerializeField]
    public SkillData skillData { get; set; }

    public ParticleSystem ps;

    public void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void Use()
    {
        StartCoroutine(Using());
    }

    IEnumerator Using()
    {
        var wfs = new WaitForSeconds(5.0f);
        yield return wfs;
        ObjectPoolingManager.instance.ReturnObject(gameObject);
        yield return null;

    }
}
