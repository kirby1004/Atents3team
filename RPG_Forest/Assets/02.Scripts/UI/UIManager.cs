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
            GameObject obj = Instantiate(inventoryManagerGameObject, transform);
            TestinventoryManager = obj.GetComponent<InventoryManager>();
        }
        if (FindObjectOfType<EquipmentManager>() == null)
        {
            GameObject obj = Instantiate(equipmentManagerGameObject, transform);
            TestequipmentManager = obj.GetComponent<EquipmentManager>();
        }
        if (FindObjectOfType<StatusManager>() == null)
        {
            GameObject obj = Instantiate(statusManagerGameObject, transform);
            TeststatusManager = obj.GetComponent<StatusManager>();
        }
    }
    private void Start()
    {
        Refresh();
    }
    public InventoryManager TestinventoryManager;
    public EquipmentManager TestequipmentManager;
    public StatusManager TeststatusManager;
    public ShopManager shopManager;
    //�����ϴ� ����
    public GameObject inventoryManagerGameObject;
    public GameObject statusManagerGameObject;
    public GameObject equipmentManagerGameObject;




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