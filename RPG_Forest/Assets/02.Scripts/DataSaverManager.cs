using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;


[System.Serializable]
public class PlayerData
{
    public float curHP;

    public Vector3 LastRocate;
    public int LastMapIndex;

    public int Soul;
    public int Level;
}

[System.Serializable]
public class PlayerInventory
{
    public int[] InventoryItemCode;
    public int[] EquipmentItemCode;
}
public class TestData
{
    public PlayerData PlayerData;
    public PlayerInventory PlayerInventory;
}

public class DataSaverManager : Singleton<DataSaverManager>
{
    //public PlayerData playerData;
    public GameObject GameExitUI;
   
    public enum ItemCodes
    {
        T1Weapon=11,
        T1Helmet,
        T1Armor,
        T1Leggins,
        T1Boots,
        T2Weapon=21,
        T2Helmet,
        T2Armor,            
        T2Leggins,
        T2Boots,
        T3Weapon=31,
        T3Helmet,
        T3Armor,
        T3Leggins,
        T3Boots

    }
    public List<ItemStatus> UseableItems;

    Dictionary<ItemCodes, ItemStatus> UseableItemsDict = new();

    public TestData testData;


    private void Awake()
    {
        base.Initialize();
        DontDestroyOnLoad(this);
        ItemListSort();
        InsertItemDict();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadJsonFile();
        testData.PlayerInventory.InventoryItemCode = new int[20];
        testData.PlayerInventory.EquipmentItemCode = new int[6];
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0 ||
            SceneManager.GetActiveScene().buildIndex == 5 || 
            SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {  
                GameExitUI.SetActive(!GameExitUI.activeSelf);
                //SaveAllData(false);
            }
        }
    }

    #region 아이템DB 입출력 및 정렬
    public void ItemListSort()
    {
        UseableItems = UseableItems.OrderBy(x => x.myCodes).ToList();
    }
    public void InsertItemDict()
    {
        for(int i = 0; i < UseableItems.Count; i++)
        {
            UseableItemsDict.Add(UseableItems[i].myCodes, UseableItems[i]);
        }
    }
    public ItemStatus GetItemStatus(ItemCodes itemCodes)
    {
        return UseableItemsDict[itemCodes];
    }
    #endregion

    bool isSaveDone = false;
    #region 데이터 저장
    public void SaveAllData(bool isQuit)
    {
        SavePlayerData();
        SavePlayerInventoryData();
        WriteJSonFile();
        if (isQuit)
        {
            if(isSaveDone)
            {
                Application.Quit();
            }   
        }
    }
    public void WriteJSonFile()
    {
        string data = JsonUtility.ToJson(testData);
        Debug.Log(Application.dataPath);

        System.IO.File.WriteAllText(Application.dataPath + "/12.JSON/Resources/PlayerData/PlayerData.json", data);
        isSaveDone = true;
    }

    public void SavePlayerData()
    {

        LoadJsonFile();

        isSaveDone = false;
        testData.PlayerData.curHP = Gamemanager.inst.myPlayer.curHp;
        testData.PlayerData.Soul = Gamemanager.inst.Money;
        testData.PlayerData.Level = EnchantManager.Inst.EnchantLevel;
        testData.PlayerData.LastMapIndex = SceneManager.GetActiveScene().buildIndex;
        testData.PlayerData.LastRocate = Gamemanager.inst.myPlayer.gameObject.transform.position;

    }

    public void SavePlayerInventoryData()
    {
        LoadJsonFile();

        isSaveDone = false;
        testData.PlayerInventory.EquipmentItemCode = new int[EquipmentManager.Inst.equipslot.Count];
        testData.PlayerInventory.InventoryItemCode = new int[InventoryManager.Inst.startSlotcount];

        for (int i = 0; i < EquipmentManager.Inst.equipslot.Count - 1; i++)
        {
            if (EquipmentManager.Inst.equipslot[i].mySlotItems == null)
            {
                testData.PlayerInventory.EquipmentItemCode[i] = 0;
                continue;
            }
            testData.PlayerInventory.EquipmentItemCode[i] = (int)EquipmentManager.Inst.equipslot[i].mySlotItems.GetComponent<Item>().item.myCodes;
        }

        for (int i = 0; i < InventoryManager.Inst.slots.Count; i++)
        {
            if (InventoryManager.Inst.slots[i].mySlotItems == null)
            {
                testData.PlayerInventory.InventoryItemCode[i] = 0;
                continue;
            }
            testData.PlayerInventory.InventoryItemCode[i] = (int)InventoryManager.Inst.slots[i].mySlotItems.GetComponent<Item>().item.myCodes;
        }

    }

    #endregion

    #region 데이터 로드

    public void LoadJsonFile()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("PlayerData/PlayerData");

        testData = JsonUtility.FromJson<TestData>(textAsset.ToString());
    }

    public void LoadInventory()
    {
        LoadJsonFile();

        for (int i = 0; i < InventoryManager.Inst.slots.Count; i++)
        {
            if (testData.PlayerInventory.InventoryItemCode[i] != 0)
            {
                ItemStatus myItem = GetItemStatus((ItemCodes)testData.PlayerInventory.InventoryItemCode[i]);
                InventoryManager.Inst.SpawnNewItem(myItem, InventoryManager.Inst.slots[i]);
            }
        }
    }
    public void LoadEquipment()
    {
        LoadJsonFile();

        for (int i = 0; i < EquipmentManager.Inst.equipslot.Count-1; i++)
        {
            if (testData.PlayerInventory.EquipmentItemCode[i] != 0)
            {
                ItemStatus myItem = GetItemStatus((ItemCodes)testData.PlayerInventory.EquipmentItemCode[i]);
                GameObject newItemGo = Instantiate(InventoryManager.Inst.inventoryItemPrefab, EquipmentManager.Inst.equipslot[i].transform);    //아이템 오브젝트 원본 받아와서 슬롯의 자식으로 생성
                newItemGo.GetComponent<Item>().InitialiseItem(myItem);
                newItemGo.AddComponent<EquipmentItem>();
            }
        }
    }
    // 런타임 로드와 스타트타임 로드
    public void LoadPlayerData(bool isRunTimeLoading)
    {
        LoadJsonFile();

        if (Gamemanager.inst != null)
        {
            // 런타임 로드시에는 ??? 로드 
            // ex) 씬전환시
            if(isRunTimeLoading)
            {

                Gamemanager.Inst.myPlayer.curHp = testData.PlayerData.curHP;
            }
            // 스타트타임 로드시에는 데이터 전체 로드
            else
            {
                //Gamemanager.Inst.myPlayer.curHp = testData.PlayerData.curHP;
                Gamemanager.Inst.Money = testData.PlayerData.Soul;
                EnchantManager.Inst.EnchantLevel = testData.PlayerData.Level;
                Gamemanager.inst.myPlayer.transform.position = testData.PlayerData.LastRocate;
            }
        }
    }
    #endregion

    #region 슬롯 초기화
    public void ResetAllSlots()
    {
        StartCoroutine(ResetAllSlot());
    }
    IEnumerator ResetAllSlot()
    {
        yield return new WaitForEndOfFrame();
        ResetInventorySlot();
        ResetEquipmentSlot();
        ResetPlayerStatus();
    }
    public void ResetInventorySlot()
    {
        for (int i = 0; i < InventoryManager.Inst.slots.Count; i++)
        {
            if (InventoryManager.Inst.slots[i].mySlotItems != null)
            {
                Destroy(InventoryManager.Inst.slots[i].mySlotItems.gameObject);
            }
            InventoryManager.Inst.slots[i].mySlotItems = null;
        }
    }
    public void ResetEquipmentSlot()
    {
        for (int i = 0; i < EquipmentManager.Inst.equipslot.Count-1; i++)
        {
            if (EquipmentManager.Inst.equipslot[i].mySlotItems != null)
            {
                Destroy(EquipmentManager.Inst.equipslot[i].mySlotItems.gameObject);
            }
            EquipmentManager.Inst.equipslot[i].mySlotItems = null;
        }
    }
    public void ResetPlayerStatus()
    {
        Gamemanager.Inst.myPlayer.curHp = 100;
        Gamemanager.Inst.Money = 0;
        EnchantManager.Inst.EnchantLevel = 0;

    }
    #endregion

    #region 데이터 초기화
    public void ResetAllData()
    {
        ResetEquipment();
        ResetInventory();
        ResetPlayerData();
        WriteJSonFile();    
    }

    public void ResetInventory()
    {
        for(int i = 0;i < testData.PlayerInventory.InventoryItemCode.Length; i++)
        {
            testData.PlayerInventory.InventoryItemCode[i] = 0;
        }
    }
    public void ResetEquipment()
    {
        for (int i = 0; i < testData.PlayerInventory.EquipmentItemCode.Length-1; i++)
        {
            testData.PlayerInventory.EquipmentItemCode[i] = 0;
        }
    }
    public void ResetPlayerData()
    {
        testData.PlayerData.curHP = 100;
        testData.PlayerData.Soul = 0;
        testData.PlayerData.Level = 0;
        testData.PlayerData.LastMapIndex = -1;
        testData.PlayerData.LastRocate = new Vector3(0,0,0);
    }

    #endregion
    

    public void OpenSaveUI()
    {

    }

}
