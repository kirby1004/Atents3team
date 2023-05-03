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
            instance = FindObjectOfType<UIManager>(); // 게임 시작 시 자기 자신을 담음
            DontDestroyOnLoad(this.gameObject);         // 씬이 변경되더라도 자기 자신(싱글톤)을 파괴하지 않고 유지하도록 설정
        }
        else // 이미 유지되고 있는 싱글톤이 있다면
        {
            if (Instance != this) Destroy(this.gameObject); // 씬에 싱글톤 오브젝트가 된 다른 UIManager Object가 있다면 자신을 파괴
        }
        // 각각의 매니저가 존재하지않을때 생성후 참조대상으로 설정해주기
        // 이미 생성되잇을때 재참조기능 추가필요?
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
    //참조하는 원본
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