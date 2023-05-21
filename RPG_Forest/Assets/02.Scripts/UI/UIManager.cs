using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static UIManager Instance => instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<UIManager>(); // ���� ���� �� �ڱ� �ڽ��� ����
            DontDestroyOnLoad(this.gameObject);         // ���� ����Ǵ��� �ڱ� �ڽ�(�̱���)�� �ı����� �ʰ� �����ϵ��� ����
        }
        else // �̹� �����ǰ� �ִ� �̱����� �ִٸ�
        {
            if (Instance != this) Destroy(this.gameObject); // ���� �̱��� ������Ʈ�� �� �ٸ� UIManager Object�� �ִٸ� �ڽ��� �ı�
        }
        // ������ �Ŵ����� �������������� ������ ����������� �������ֱ�
        // �̹� ������������ ��������� �߰��ʿ�?
        if (FindObjectOfType<InventoryManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/InventorySystem") as GameObject,transform);
            inventoryManager = obj.GetComponent<InventoryManager>();
        }
        if (FindObjectOfType<EquipmentManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/EquipmentSystem") as GameObject, transform);
            equipmentManager = obj.GetComponent<EquipmentManager>();
        }
        if (FindObjectOfType<StatusManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/StatusSystem") as GameObject, transform);
            statusManager = obj.GetComponent<StatusManager>();
        }
        if (FindObjectOfType<LootingManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/LootingSystem") as GameObject, transform);
            lootingManager = obj.GetComponent<LootingManager>();
        }
        if (FindObjectOfType<ShopManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/ShopSystem") as GameObject, transform);
            shopManager = obj.GetComponent<ShopManager>();
        }

    }
    private void Start()
    {
        Refresh();
        skillList = GetComponentInChildren<MySkillList>();
    }
    [Header("Managers")]
    public InventoryManager inventoryManager;
    public EquipmentManager equipmentManager;
    public StatusManager statusManager;
    public ShopManager shopManager;
    public LootingManager lootingManager;
    //�����ϴ� ����
    //public GameObject inventoryManagerGameObject;
    //public GameObject statusManagerGameObject;
    //public GameObject equipmentManagerGameObject;
    
    public MySkillList skillList;
    public TMP_Text myName;

    public void Refresh()
    {
        StartCoroutine(ManagerSetup());
    }
    IEnumerator ManagerSetup()
    {
        yield return new WaitForFixedUpdate();
        EquipmentManager.Inst.gameObject.SetActive(false);
        InventoryManager.Inst.gameObject.SetActive(false);
        StatusManager.Inst.gameObject.SetActive(false);

    }

    public void TestOpenShop(ShopItemList myList)
    {
        ShopManager.Inst.OpenShop(myList);
    }
}
public enum ItemSlotType
{
    Inventory, Equipment, Quick, Shop, Skill, Soul
}