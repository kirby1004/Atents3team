using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// ���� �Ŵ����� �̱��� �������� ����
public class Gamemanager : MonoBehaviour , IEconomy
{
    public static Gamemanager inst;
    public static Gamemanager Inst => inst;

    public void Awake()
    {
        if (inst == null)
        {
            inst = FindObjectOfType<Gamemanager>(); // ���� ���� �� �ڱ� �ڽ��� ����
            DontDestroyOnLoad(this.gameObject);         // ���� ����Ǵ��� �ڱ� �ڽ�(�̱���)�� �ı����� �ʰ� �����ϵ��� ����
        }
        else // �̹� �����ǰ� �ִ� �̱����� �ִٸ�
        {
            if (Inst != this) Destroy(this.gameObject); // ���� �̱��� ������Ʈ�� �� �ٸ� UIManager Object�� �ִٸ� �ڽ��� �ı�
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
        GetMoney(100);
    }

    // ����ȯ�� �÷��̾� ���� ������ Ž������
    public void FindObject()
    {
        //�÷��̾� ������
        myPlayer = FindObjectOfType<PlayerController>();

        //������ ���¸��϶��� ������
        if(FindObjectOfType<Dragon>() != null)
        {
            myDragon = FindObjectOfType<Dragon>();
        }

        //�����ʰ� ���¸��϶��� ������
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


    // ���ε�
    public PlayerController myPlayer;
    public Monster myEnemy;
    public Dragon myDragon;
    public List<Spawnner> mySpawnner; // Ʈ�������� ������ Ŭ���� ��ü�� ������
    public SceneLoader mySceneLoader;

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

    // ���̺굥����(�߰�����)





    #region ��ȭ�����ڵ�
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


    #region ���� ����
    // Ȯ�� �˻�
    public bool ProbabilityChoose(float Rate)
    {
        //float Percentge = Rate / 100;
        if (UnityEngine.Random.Range(0, 100) < Rate)
        {
            return true;
        }
        return false;
    }
    // ��ų ������ ���� Val1 = ��ų��������
    public float EnchantMultiple(float Value1) 
    {
        return Value1 * (1 + EnchantManager.Inst.EnchantLevel * 0.01f);
    }

    // ������ ������ ����
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
