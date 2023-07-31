using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        T1Weapon = 11,
        T1Helmet,
        T1Armor,
        T1Leggins,
        T1Boots,
        T2Weapon = 21,
        T2Helmet,
        T2Armor,
        T2Leggins,
        T2Boots,
        T3Weapon = 31,
        T3Helmet,
        T3Armor,
        T3Leggins,
        T3Boots

    }
    public List<ItemStatus> UseableItems;

    Dictionary<ItemCodes, ItemStatus> UseableItemsDict = new();

    public TestData testData;

    public GameObject GameExitObj;

    private void Awake()
    {
        base.Initialize();
        DontDestroyOnLoad(this);
        ItemListSort();
        InsertItemDict();
        LoadPossibleCheck();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadJsonFile();
        if (!LoadSuccess)
        {
            testData.PlayerInventory.InventoryItemCode = new int[20];
            testData.PlayerInventory.EquipmentItemCode = new int[6];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 ||
            SceneManager.GetActiveScene().buildIndex == 5 ||
            SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (FindObjectOfType<GameExit>() == null)
                {
                    //Instantiate(Resources.Load("UIResource/Test/GameExitUI2"))
                    Instantiate(GameExitUI);
                }
            }
        }
    }

    #region ������DB ����� �� ����
    public void ItemListSort()
    {
        UseableItems = UseableItems.OrderBy(x => x.myCodes).ToList();
    }
    public void InsertItemDict()
    {
        for (int i = 0; i < UseableItems.Count; i++)
        {
            UseableItemsDict.Add(UseableItems[i].myCodes, UseableItems[i]);
        }
    }
    public ItemStatus GetItemStatus(ItemCodes itemCodes)
    {
        return UseableItemsDict[itemCodes];
    }
    #endregion

    public bool isSaveDone = false;
    #region ������ ����

    /// <summary>
    /// �÷��̾� ������ , �κ��丮 , ���â ��Ȳ�� �����ϱ�
    /// bool�� True �� ����������� ���� false �̸� ���常
    /// </summary>
    /// <param name="isQuit"></param>
    public void SaveAllData(bool isQuit)
    {
        LoadJsonFile();
        SavePlayerData();
        SavePlayerInventoryData();
        WriteJSonFile();

    }
    public void WriteJSonFile()
    {
        string data = JsonUtility.ToJson(testData);
        Debug.Log(Application.dataPath);

#if !UNITY_EDITOR
        System.IO.File.WriteAllText(Application.persistentDataPath + 
        "/JSon/PlayerDatas/PlayerData.json", data);
#else
        System.IO.File.WriteAllText(Application.dataPath + 
            "/12.JSON/Resources/PlayerDatas/PlayerData.json", data);
#endif      
        isSaveDone = true;
    }


    public void SavePlayerData()
    {

        isSaveDone = false;
        testData.PlayerData.curHP = Gamemanager.inst.myPlayer.curHp;
        testData.PlayerData.Soul = Gamemanager.inst.Money;
        testData.PlayerData.Level = EnchantManager.Inst.EnchantLevel;
        testData.PlayerData.LastMapIndex = SceneManager.GetActiveScene().buildIndex;
        testData.PlayerData.LastRocate = Gamemanager.inst.myPlayer.gameObject.transform.position;

    }

    public void SavePlayerInventoryData()
    {

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

    #region ������ �ε�
    bool LoadSuccess = false;
    // ���� �ε尡 �������� Ȯ���� ���ٸ� �����ϱ�
    public void LoadPossibleCheck()
    {
#if !UNITY_EDITOR
        bool success = false;
        while (!success)
        {
            if (Directory.Exists(Application.persistentDataPath + "/JSon"))
            {
                if(Directory.Exists(Application.persistentDataPath + "/JSon/PlayerDatas"))
                {
                    if(File.Exists(Application.persistentDataPath + "/JSon/PlayerDatas/PlayerData.json"))
                    {
                        success = true;
                        LoadSuccess = true;
                    }
                    else
                    {
                        //File.Create(Application.persistentDataPath + "/JSon/PlayerDatas/PlayerData.json");
                        FirstTimeWrite();
                        success = true;
                    }
                }
                else
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/JSon/PlayerDatas");
                }
            }
            else
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/JSon/");
            }
        }
#endif

    }
    public void LoadJsonFile()
    {
#if !UNITY_EDITOR
        testData = JsonUtility.FromJson<TestData>(System.IO.File.ReadAllText(Application.persistentDataPath +
            "/JSon/PlayerDatas/PlayerData.json"));
#else
        //TextAsset textAsset = 
        TextAsset textAsset = Resources.Load<TextAsset>("PlayerDatas/PlayerData");
        if(textAsset != null)
        {
            LoadSuccess = true;
        }
        testData = JsonUtility.FromJson<TestData>(textAsset.ToString());
#endif

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

        for (int i = 0; i < EquipmentManager.Inst.equipslot.Count - 1; i++)
        {
            if (testData.PlayerInventory.EquipmentItemCode[i] != 0)
            {
                ItemStatus myItem = GetItemStatus((ItemCodes)testData.PlayerInventory.EquipmentItemCode[i]);
                GameObject newItemGo = Instantiate(InventoryManager.Inst.inventoryItemPrefab, EquipmentManager.Inst.equipslot[i].transform);    //������ ������Ʈ ���� �޾ƿͼ� ������ �ڽ����� ����
                newItemGo.GetComponent<Item>().InitialiseItem(myItem);
                newItemGo.AddComponent<EquipmentItem>();
            }
        }
    }
    // ��Ÿ�� �ε�� ��ŸƮŸ�� �ε�
    public void LoadPlayerData(bool isRunTimeLoading)
    {
        LoadJsonFile();

        if (Gamemanager.inst != null)
        {
            // ��Ÿ�� �ε�ÿ��� ??? �ε� 
            // ex) ����ȯ��
            if (isRunTimeLoading)
            {

                Gamemanager.Inst.myPlayer.curHp = testData.PlayerData.curHP;
            }
            // ��ŸƮŸ�� �ε�ÿ��� ������ ��ü �ε�
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

    #region ���� �ʱ�ȭ
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
        for (int i = 0; i < EquipmentManager.Inst.equipslot.Count - 1; i++)
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

    #region ������ �ʱ�ȭ

    public void ResetAllData()
    {
        ResetEquipment();
        ResetInventory();
        ResetPlayerData();
        WriteJSonFile();
    }

    public void ResetInventory()
    {
        for (int i = 0; i < testData.PlayerInventory.InventoryItemCode.Length; i++)
        {
            testData.PlayerInventory.InventoryItemCode[i] = 0;
        }
    }
    public void ResetEquipment()
    {
        for (int i = 0; i < testData.PlayerInventory.EquipmentItemCode.Length - 1; i++)
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
        testData.PlayerData.LastRocate = new Vector3(0, 0, 0);
    }

    #endregion


    public void OpenSaveUI()
    {

    }

    public void FirstTimeWrite()
    {
        string data = "{\"PlayerData\":{\"curHP\":100.0,\"LastRocate\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"LastMapIndex\":-1,\"Soul\":0,\"Level\":0},\"PlayerInventory\":{\"InventoryItemCode\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"EquipmentItemCode\":[0,0,0,0,0,0]}}";
        
        System.IO.File.WriteAllText(Application.persistentDataPath+"/BasicData.json", data);


        //string textAsset = System.IO.File.ReadAllText(Application.persistentDataPath+"/BasicData.json");


        System.IO.File.WriteAllText(Application.persistentDataPath +
        "/JSon/PlayerDatas/PlayerData.json", data);
    }
}
