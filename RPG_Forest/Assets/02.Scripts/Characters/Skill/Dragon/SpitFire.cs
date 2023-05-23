using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SpitFire : Skill, ISkill
{
    [field:SerializeField]
    public SkillData skillData { get; set; }
    [SerializeField]
    SpitFireTrigger trigger;
    public void Awake()
    {
       
    }

    public void Use(UnityAction e = null)
    {
        StartCoroutine(Using());
    }

    IEnumerator Using(UnityAction e = null)
    {
        var wfs = new WaitForSeconds(5.0f);
        yield return wfs;
        ObjectPoolingManager.instance.ReturnObject(gameObject);
        yield return null;

    }

    public void Update()
    {
        //if (trigger.isHit)
        //{
        //    trigger.hitObject.GetComponent<IBattle>()?.OnDamage(5);
        //}
    }
}
