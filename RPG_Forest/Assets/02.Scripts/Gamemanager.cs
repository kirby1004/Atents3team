using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ���� �Ŵ����� �̱��� �������� ����
public class GameManager : MonoBehaviour
{
    public static GameManager instance;                 // �ڱ� �ڽ��� ���� static ����

    public static GameManager Instance => instance;     // �� ������ ������ static ������Ƽ Instance

    public void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<GameManager>(); // ���� ���� �� �ڱ� �ڽ��� ����
            DontDestroyOnLoad(this.gameObject);         // ���� ����Ǵ��� �ڱ� �ڽ�(�̱���)�� �ı����� �ʰ� �����ϵ��� ����
        }
        else // �̹� �����ǰ� �ִ� �̱����� �ִٸ�
        {
            if (Instance != this) Destroy(this.gameObject); // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager Object�� �ִٸ� �ڽ��� �ı�
        }
        
        if(FindObjectOfType<UIManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/UIManager") as GameObject);
            myUIManager = obj.GetComponent<UIManager>();
        }

        mySpawnner = null;
    }
    private void Start()
    {
        UpdateMoney.AddListener(InventoryManager.Inst.UpdateMyMoney);
    }

    // ��
    public GameObject cutScene01;
    
    //enum CutScene { encounter = 4, cut1, cut2, cut3 }

    // ���ε�

    public PlayerController myPlayer;
    public Monster myEnemy;
    public Spawnner mySpawnner; // Ʈ�������� ������ Ŭ���� ��ü�� ������


    // ����, �÷��̾� �ʿ��� ó���ϴ� �� �߿��� GameManager�� �̰��� ����
    // [IBattle] 
    public UnityAction DeathAlarm;
    [SerializeField]
    public float playTime; // �� ���� �ð�



    // [Status] Ŭ������ ���� ������Ƽ �ҷ�����

    // [Inventory] Ŭ������ ���� ������Ƽ �ҷ�����

    // [Enemy] ������ ��ġ  

    // [UIManager �̱���]���� �����ϰ� ���⿡�� view ó��
    [Header("UIManager")]
    public UIManager myUIManager;
    // ���̺굥����



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

    

    public void GetMoney(int money)
    {
        if( GetComponent<IEconomy>().Money - money >= 0 ) 
        {
            GetComponent<IEconomy>().Money += money;
        }
    }

    public bool CheckMoney(int money)
    {
        if(GetComponent<IEconomy>().Money - money > 0)
        {
            return true;
        }
        else 
        {
            return false; 
        }
    }

    public UnityEvent<int> UpdateMoney;
}
