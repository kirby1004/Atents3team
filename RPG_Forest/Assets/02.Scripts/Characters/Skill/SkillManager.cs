using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillManager : MonoBehaviour
{
    
    public static SkillManager instance;                 // �ڱ� �ڽ��� ���� static ����
    public static SkillManager Instance => instance;     // �� ������ ������ static ������Ƽ Instance

    public void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SkillManager>(); // ���� ���� �� �ڱ� �ڽ��� ����
            DontDestroyOnLoad(this.gameObject);         // ���� ����Ǵ��� �ڱ� �ڽ�(�̱���)�� �ı����� �ʰ� �����ϵ��� ����
        }
        else // �̹� �����ǰ� �ִ� �̱����� �ִٸ�
        {
            if (Instance != this) Destroy(this.gameObject); // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager Object�� �ִٸ� �ڽ��� �ı�
        }
    }

    public Dictionary<PlayerSkillName,bool> playerSkillCooldown = new Dictionary<PlayerSkillName, bool>(); //��ų�̸��̶� bool �� �����ؼ� true�� ���� X, false�� ������� �ʵ��� 
    [SerializeField] List<PlayerSkillName> key=new List<PlayerSkillName> ();
    private void Start() 
    {
       for(int i = 0; i < System.Enum.GetValues(typeof(PlayerSkillName)).Length; i++)
        {
            playerSkillCooldown.Add((PlayerSkillName)i, false);
            key.Add((PlayerSkillName)i);
        }
    }

    public void RegisterSkill(PlayerSkillName name,Transform Point)
    {
        if (!playerSkillCooldown[name])
        {
            playerSkillCooldown[name] = true;
            GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, Quaternion.identity);
            skill.GetComponent<ISkill>()?.Use();
            UIManager.instance.skillList.FindSlots(skill.GetComponent<ISkill>().skillData).
                StartCooldown(skill.GetComponent<ISkill>().skillData.CoolTime);
            StartCoroutine(CoolDown(name,skill.GetComponent<ISkill>().skillData.CoolTime));
        }
    }

    public void RegisterSkill(PlayerSkillName name, Vector3 Point,Quaternion quaternion)
    {
        if (!playerSkillCooldown[name])
        {
            playerSkillCooldown[name] = true;
            GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point, quaternion);
            skill.GetComponent<ISkill>()?.Use();
            UIManager.instance.skillList.FindSlots(skill.GetComponent<ISkill>().skillData).
                StartCooldown(skill.GetComponent<ISkill>().skillData.CoolTime);
            StartCoroutine(CoolDown(name, skill.GetComponent<ISkill>().skillData.CoolTime));
        }
    }

    public void RegisterSkill(MonsterSkillName name, Transform Point)
    {
        GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, Quaternion.identity);
        skill.GetComponent<ISkill>()?.Use();
    }

    public void RegisterSkill(MonsterSkillName name, Transform Point, Quaternion quaternion)
    {
        GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, quaternion);
        skill.GetComponent<ISkill>()?.Use();
    }

    public void RegisterSkill(MonsterSkillName name, Vector3 Point, Quaternion quaternion = default)
    {
        GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point, quaternion);
        skill.GetComponent<ISkill>()?.Use();
    }

    IEnumerator CoolDown(PlayerSkillName name,float coolTime) //��ٿ� �ڷ�ƾ ��ٿ��� �� �Ǹ� false�� �ٲٱ�.
    {
        float playTime = 0.0f;
        while (coolTime>playTime)
        {
            playTime += Time.deltaTime;
            yield return null;
        }
        playerSkillCooldown[name] = false;
        playTime = 0.0f;
    }
}

public enum PlayerSkillName
{
    EnergyBall,EnergyTornado
}

public enum MonsterSkillName
{
    DevilEye, MagicCircleImage, SpitFire
}