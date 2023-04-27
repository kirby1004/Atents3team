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
        if(FindObjectOfType<InventoryManager>() == null)
        {
            GameObject obj = Instantiate(inventoryManagerGameObject, transform);
            inventoryManager = obj.GetComponent<InventoryManager>();
            obj.SetActive(false);
        }
        if (FindObjectOfType<EquipmentManager>() == null)
        {
            GameObject obj = Instantiate(equipmentManagerGameObject, transform);
            equipmentManager = obj.GetComponent<EquipmentManager>();
            obj.SetActive(false);
        }
        if (FindObjectOfType<StatusManager>() == null)
        {
            GameObject obj = Instantiate(statusManagerGameObject, transform);
            statusManager = obj.GetComponent<StatusManager>();
            obj.SetActive(false);
        }
    }


    //�����ϴ� �Ŵ���
    public InventoryManager inventoryManager;
    public EquipmentManager equipmentManager;
    public StatusManager statusManager;
    public ShopManager shopManager;
    //�����ϴ� ������
    public GameObject inventoryManagerGameObject;
    public GameObject statusManagerGameObject;
    public GameObject equipmentManagerGameObject;




    public void Refresh(Component component , Transform parents)
    {

    }

}
