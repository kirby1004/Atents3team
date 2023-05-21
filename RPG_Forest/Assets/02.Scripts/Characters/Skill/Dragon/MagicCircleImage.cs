using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleImage : Skill, ISkill
{
    [field:SerializeField]
    public SkillData skillData { get; set; }

    float dist;
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
        //float offset = 4.0f;
        while(dist < 0.0f)
        {
            float delta = Time.deltaTime * skillData.Speed;
            if(dist < delta)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(transform.forward * delta, Space.World);
            //Mathf.Lerp(10.0f, 30.0f, Time.deltaTime * offset);
            yield return null;
        }

        var wfs = new WaitForSeconds(20.0f);
        yield return wfs;
        ObjectPoolingManager.instance.ReturnObject(gameObject);
    }
}
