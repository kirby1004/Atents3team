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

    public List<Skill> skillList = new List<Skill>();

    private void Start()
    {
       
    }

    public void RegisterSkill(Skillname name,Transform Point)
    {
        GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position,Quaternion.identity);

    }
}

public enum Skillname
{
    EnergyBall
}