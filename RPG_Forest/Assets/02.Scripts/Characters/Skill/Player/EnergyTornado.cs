using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergyTornado : Skill,ISkill
{
    [field: SerializeField]
    public SkillData skillData
    {
        get; set;
    }
    [SerializeField]
    LayerMask enemyMask;
    [SerializeField]
    float deltaTime;
    public float dist;
    bool isHit;
    public void Awake()
    {
        deltaTime = 0;
        dist = skillData.Distance;
    }
    public void Use(UnityAction e = null)
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

    private void OnTriggerExit(Collider other)
    {
        isHit = false;
    }
    private void OnTriggerStay(Collider other)
    {
       
        if(deltaTime> skillData.Value2)
        {
            if (((1 << other.gameObject.layer) & enemyMask) != 0)
            {
                other.GetComponent<IBattle>()?.OnDamage(skillData.Value1);
                Debug.Log($"Damage {skillData.Value1} ");
            }
            deltaTime = 0;
        }
        else
        {
            deltaTime += Time.deltaTime;
        }
    }
}
