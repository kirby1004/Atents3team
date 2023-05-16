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
       //����� ��ų enum�� skillCooldown ��ųʸ��� �� �־������.
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
            StartCoroutine(CoolDown(name));
        }
    }

    IEnumerator CoolDown(Skillname name) //��ٿ� �ڷ�ƾ ��ٿ��� �� �Ǹ� false�� �ٲٱ�.
    {
        yield return null;
    }
}

public enum Skillname
{
    EnergyBall
}