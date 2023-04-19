using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 게임 매니저를 싱글톤 패턴으로 구현
public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;                 // 자기 자신을 담을 static 변수

    public static Gamemanager Instance => instance;     // 그 변수를 리턴할 static 프로퍼티 Instance

    public void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Gamemanager>(); // 게임 시작 시 자기 자신을 담음
            DontDestroyOnLoad(this.gameObject);         // 씬이 변경되더라도 자기 자신(싱글톤)을 파괴하지 않고 유지하도록 설정
        }
        else // 이미 유지되고 있는 싱글톤이 있다면
        {
            if (Instance != this) Destroy(this.gameObject); // 씬에 싱글톤 오브젝트가 된 다른 GameManager Object가 있다면 자신을 파괴
        }
    }

    // 바인딩
    public RPGPlayer myPlyaer;
    public Monster myMonster;
    public Spawnner mySpawnner; // 트랜스폼만 받을지 클래스 전체로 받을지


    // 몬스터, 플레이어 쪽에서 처리하는 것 중에서 GameManager로 이관할 내용
    // [IBattle] 
    public UnityAction DeathAlarm;
    [SerializeField]
    public float playTime; // 


    
    // [Status] 클래스로 만들어서 프로퍼티 불러오기
    
    // [Inventory] 클래스로 만들어서 프로퍼티 불러오기
    
    // [Enemy] 스포너 위치  
    
    // [UIManager 싱글톤]으로 구현하고 여기에서 view 처리

    // 세이브데이터



}
