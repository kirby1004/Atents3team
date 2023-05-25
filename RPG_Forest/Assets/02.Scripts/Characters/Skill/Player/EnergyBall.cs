using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TreeEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class EnergyBall : Skill,ISkill
{
    [field:SerializeField]
    public SkillData skillData
    {
        get;set;
    }
    [SerializeField]
    LayerMask enemyMask;
    [SerializeField]
    LayerMask crashMask;
    [SerializeField]
    Transform hitEffect;
    public float dist;
    public void Awake()
    {
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

   void Update()
    {
        Ray ray = new Ray();
        ray.origin = HitPoint.position;
        ray.direction = transform.forward;
        if (Physics.Raycast(ray, out RaycastHit hit, 1.4f,crashMask))
        {
            if (((1 << hit.transform.gameObject.layer) & enemyMask) != 0)
            {
                hitEffect.gameObject.SetActive(true);
                hit.transform.GetComponent<IBattle>()?.OnDamage(skillData.Value1);
            }
            ObjectPoolingManager.instance.ReturnObject(this.gameObject);
            Debug.Log($"Damage {skillData.Value1}");
        }

        Debug.DrawLine(HitPoint.position, ray.direction);

    }
}
