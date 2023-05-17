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
    Dictionary<Skillname,bool> skillCooldown = new Dictionary<Skillname,bool>(); //��ų�̸��̶� bool �� �����ؼ� true�� ���� X, false�� ������� �ʵ��� 

    private void Start() 
    {
       for(int i = 0; i < System.Enum.GetValues(typeof(Skillname)).Length; i++)
        {
            skillCooldown.Add((Skillname)i, false);
        }
    }

    public void RegisterSkill(Skillname name,Transform Point)
    {
        if (!skillCooldown[name])
        {
            skillCooldown[name] = true;
            GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, Quaternion.identity);
            skill.GetComponent<ISkill>()?.Use();
            StartCoroutine(CoolDown(name,skill.GetComponent<ISkill>().skillData.CoolTime));
        }
    }

    IEnumerator CoolDown(Skillname name,float coolTime) //��ٿ� �ڷ�ƾ ��ٿ��� �� �Ǹ� false�� �ٲٱ�.
    {
        float playTime = 0.0f;
        while (coolTime>playTime)
        {
            playTime += Time.deltaTime;
            yield return null;
        }
        skillCooldown[name] = false;
    }
}

public enum Skillname
{
    EnergyBall
}