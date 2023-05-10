using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;                 // 자기 자신을 담을 static 변수

    public static SkillManager Instance => instance;     // 그 변수를 리턴할 static 프로퍼티 Instance

    public void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SkillManager>(); // 게임 시작 시 자기 자신을 담음
            DontDestroyOnLoad(this.gameObject);         // 씬이 변경되더라도 자기 자신(싱글톤)을 파괴하지 않고 유지하도록 설정
        }
        else // 이미 유지되고 있는 싱글톤이 있다면
        {
            if (Instance != this) Destroy(this.gameObject); // 씬에 싱글톤 오브젝트가 된 다른 GameManager Object가 있다면 자신을 파괴
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