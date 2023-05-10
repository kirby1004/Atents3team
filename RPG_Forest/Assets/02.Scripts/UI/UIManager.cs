using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(FindObjectOfType<LootingManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/LootingSystem") as GameObject, transform);
            lootingManager = obj.GetComponent<LootingManager>();
        }
    }
    private void Start()
    {
        Refresh();
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

}
public enum ItemSlotType
{
    Inventory, Equipment, Quick, Shop, Skill, Soul
}