using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.Events;

// ���� �Ŵ����� �̱��� �������� ����
public class GameManager : MonoBehaviour , IEconomy
{
    public static GameManager inst;
    public static GameManager Inst => inst;

    public void Awake()
    {
        if (inst == null)
        {
            inst = FindObjectOfType<GameManager>(); // ���� ���� �� �ڱ� �ڽ��� ����
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

        mySpawnner = null;
    }
    private void Start()
    {
        economy = this.GetComponent<IEconomy>();
        Money = new int();
    }

    // ��
    public GameObject cutScene01;
    
    //enum CutScene { encounter = 4, cut1, cut2, cut3 }

    // ���ε�

    public PlayerController myPlayer;
    public Monster myEnemy;
    public Dragon myDragon;
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
            GameManager.Inst.UpdateMoney?.Invoke(_Money);
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
        if(economy.Money - money > 0)
        {
            return true;
        }
        else 
        {
            return false; 
        }
    }

    public UnityEvent<int> UpdateMoney;

    // Ȯ�� �˻�
    public bool ProbabilityChoose(float Rate)
    {
        //float Percentge = Rate / 100;
        if (Random.Range(0, 100) < Rate)
        {
            return true;
        }
        return false;
    }

    public float EnchantMultiple(float Value1) 
    {
        return Value1 * (1 + EnchantManager.Inst.EnchantLevel * 0.01f);
    } 

}
