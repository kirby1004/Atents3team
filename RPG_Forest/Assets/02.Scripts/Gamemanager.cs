using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// 게임 매니저를 싱글톤 패턴으로 구현
public class Gamemanager : MonoBehaviour , IEconomy
{
    public static Gamemanager inst;
    public static Gamemanager Inst => inst;

    public void Awake()
    {
        if (inst == null)
        {
            inst = FindObjectOfType<Gamemanager>(); // 게임 시작 시 자기 자신을 담음
            DontDestroyOnLoad(this.gameObject);         // 씬이 변경되더라도 자기 자신(싱글톤)을 파괴하지 않고 유지하도록 설정
        }
        else // 이미 유지되고 있는 싱글톤이 있다면
        {
            if (Inst != this) Destroy(this.gameObject); // 씬에 싱글톤 오브젝트가 된 다른 UIManager Object가 있다면 자신을 파괴
        }

        if (FindObjectOfType<UIManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/UIManager") as GameObject);
            myUIManager = obj.GetComponent<UIManager>();
        }
        if(FindObjectOfType<SceneLoader>() == null)
        {
            GameObject obj = new();
            obj.name = typeof(SceneLoader).ToString();
            obj.AddComponent<SceneLoader>();
            DontDestroyOnLoad (obj);
        }
    }

    private void Start()
    {
        economy = this.GetComponent<IEconomy>();
        Money = new int();
        //GetMoney(100);
        DataSaverManager.Inst.LoadPlayerData(false);
    }

    // 씬전환시 플레이어 보스 스포너 탐색동작
    public void FindObject()
    {
        //플레이어 재참조
        myPlayer = FindObjectOfType<PlayerController>();

        //보스가 없는맵일때는 미참조
        if(FindObjectOfType<Dragon>() != null)
        {
            myDragon = FindObjectOfType<Dragon>();
        }

        //스포너가 없는맵일때는 미참조
        if(FindObjectOfType<Spawnner>() != null)
        {
            Spawnner[] spawnners = FindObjectsOfType<Spawnner>();
            mySpawnner = spawnners.ToList();
            for(int i = 0; i < mySpawnner.Count; i++)
            {
                mySpawnner[i].mySpawnnerIndex = mySpawnner[i].FindMyIndex(mySpawnner[i]);
            }
        }
        
    }


    // 바인딩
    public PlayerController myPlayer;
    public Monster myEnemy;
    public Dragon myDragon;
    public List<Spawnner> mySpawnner; // 트랜스폼만 받을지 클래스 전체로 받을지
    public SceneLoader mySceneLoader;

    // 몬스터, 플레이어 쪽에서 처리하는 것 중에서 GameManager로 이관할 내용
    // [IBattle] 
    public UnityAction DeathAlarm;
    [SerializeField]
    public float playTime; // 총 게임 시간



    // [Status] 클래스로 만들어서 프로퍼티 불러오기

    // [Inventory] 클래스로 만들어서 프로퍼티 불러오기

    // [Enemy] 스포너 위치  

    // [UIManager 싱글톤]으로 구현하고 여기에서 view 처리
    [Header("UIManager")]
    public UIManager myUIManager;

    // 세이브데이터(추가예정)





    #region 재화관련코드
    public IEconomy economy;
    private int _Money = 0;
    public int Money
    {
        get 
        {  
            return _Money;
        }
        set 
        {
           _Money = value;
            Gamemanager.Inst.UpdateMoney?.Invoke(_Money);
        }
    }
    //public int Money = 0;
    public void GetMoney(int money)
    {
        if(money < 0)
        {
            if(economy.Money - money >= 0 ) 
            {
                economy.Money += money;
            }
        }
        else
        {
            economy.Money += money;
        }
    }

    public bool CheckMoney(int money)
    {
        if(economy.Money - money >= 0)
        {
            return true;
        }
        else 
        {
            return false; 
        }
    }

    public UnityEvent<int> UpdateMoney;
    #endregion


    #region 각종 계산식
    // 확률 검사
    public bool ProbabilityChoose(float Rate)
    {
        //float Percentge = Rate / 100;
        if (UnityEngine.Random.Range(0, 100) < Rate)
        {
            return true;
        }
        return false;
    }
    // 스킬 데미지 증폭 Val1 = 스킬본데미지
    public float EnchantMultiple(float Value1) 
    {
        return Value1 * (1 + EnchantManager.Inst.EnchantLevel * 0.01f);
    }

    // 데미지 감소율 계산식
    public float DamageDecrease(float AP , float DP)
    {
        float Rate = 1 - (DP / (DP + 100));
        Debug.Log($"{AP*Rate}");
        if( AP * Rate > 1.0f)
        {
            return AP * Rate;
        }
        else
        {
            return 1;
        }
    }
    #endregion


}
