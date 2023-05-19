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
    [SerializeField]
    Dictionary<PlayerSkillName,bool> playerSkillCooldown = new Dictionary<PlayerSkillName, bool>(); //��ų�̸��̶� bool �� �����ؼ� true�� ���� X, false�� ������� �ʵ��� 

    private void Start() 
    {
       for(int i = 0; i < System.Enum.GetValues(typeof(PlayerSkillName)).Length; i++)
        {
            playerSkillCooldown.Add((PlayerSkillName)i, false);
        }
    }

    public void RegisterSkill(PlayerSkillName name,Transform Point)
    {
        if (!playerSkillCooldown[name])
        {
            playerSkillCooldown[name] = true;
            GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, Quaternion.identity);
            skill.GetComponent<ISkill>()?.Use();
            StartCoroutine(CoolDown(name,skill.GetComponent<ISkill>().skillData.CoolTime));
        }
    }

    public void RegisterSkill(MonsterSkillName name, Transform Point)
    {
        GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, Quaternion.identity);
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
    }
}

public enum PlayerSkillName
{
    EnergyBall
}

public enum MonsterSkillName
{
    DevilEye
}