using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicCircleImage : Skill, ISkill
{
    [field:SerializeField]
    public SkillData skillData { get; set; }

    float dist;
    Vector3 MagicCircleMaxScale;
    Vector3 MagicCircleMinScale;
    Vector3 SpitFireRotation;

    [SerializeField]
    Animator animator;
    public void Awake()
    {
        dist = skillData.Distance;
        MagicCircleMaxScale = new Vector3(50, 50, 50);
        MagicCircleMinScale = new Vector3(35, 35,35);
        SpitFireRotation = new Vector3(135.0f, 0f, 0f);
    }

    public void Use(UnityAction e = null)
    {
        StartCoroutine(Using());
    }

    IEnumerator Using()
    {      
        animator.SetTrigger("Intro");
        var wfs = new WaitForSeconds(18.0f);
        yield return wfs;
        animator.SetTrigger("Outro");
        while (!animator.GetBool("OutroEnd"))
        {
            //Debug.Log($"{animator.GetBool("OutroEnd")}");
            yield return null;
        }
        ObjectPoolingManager.instance.ReturnObject(gameObject);

    }
}
