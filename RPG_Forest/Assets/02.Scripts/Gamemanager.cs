using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ���� �Ŵ����� �̱��� �������� ����
public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;                 // �ڱ� �ڽ��� ���� static ����

    public static Gamemanager Instance => instance;     // �� ������ ������ static ������Ƽ Instance

    public void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Gamemanager>(); // ���� ���� �� �ڱ� �ڽ��� ����
            DontDestroyOnLoad(this.gameObject);         // ���� ����Ǵ��� �ڱ� �ڽ�(�̱���)�� �ı����� �ʰ� �����ϵ��� ����
        }
        else // �̹� �����ǰ� �ִ� �̱����� �ִٸ�
        {
            if (Instance != this) Destroy(this.gameObject); // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager Object�� �ִٸ� �ڽ��� �ı�
        }
    }

    // ��


    // ���ε�
    
    public PlayerController myPlyaer;
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
    public UIManager myUIManager;
    
    // ���̺굥����





}
