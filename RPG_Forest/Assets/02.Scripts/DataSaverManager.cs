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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {  
            GameExitUI.SetActive(!GameExitUI.activeSelf);
            //SaveAllData(false);
        }
    }

    #region ������DB ����� �� ����
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
    #region ������ ����
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
                testData.PlayerInventory.EquipmentItemCode[i] = -1;
                continue;
            }
            testData.PlayerInventory.EquipmentItemCode[i] = (int)EquipmentManager.Inst.equipslot[i].mySlotItems.GetComponent<Item>().item.myCodes;
        }

        for (int i = 0; i < InventoryManager.Inst.slots.Count; i++)
        {
            if (InventoryManager.Inst.slots[i].mySlotItems == null)
            {
                testData.PlayerInventory.InventoryItemCode[i] = -1;
                continue;
            }
            testData.PlayerInventory.InventoryItemCode[i] = (int)InventoryManager.Inst.slots[i].mySlotItems.GetComponent<Item>().item.myCodes;
        }

    }

    #endregion

    #region ������ �ε�

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
            if (testData.PlayerInventory.InventoryItemCode[i] != -1)
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
            if (testData.PlayerInventory.EquipmentItemCode[i] != -1)
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
            if(isRunTimeLoading)
            {

                Gamemanager.Inst.myPlayer.curHp = testData.PlayerData.curHP;

            }
            // ��ŸƮŸ�� �ε�ÿ��� ������ ��ü �ε�
            else
            {
                //Gamemanager.Inst.myPlayer.curHp = testData.PlayerData.curHP;
                Gamemanager.Inst.Money = testData.PlayerData.Soul;
                EnchantManager.Inst.EnchantLevel = testData.PlayerData.Level;
            }
        }
    }
    #endregion

    #region ������ �ʱ�ȭ
    public void ResetInventory()
    {
        for (int i = 0; i < InventoryManager.Inst.slots.Count; i++)
        {
            Destroy(InventoryManager.Inst.slots[i].mySlotItems.gameObject);
            InventoryManager.Inst.slots[i].mySlotItems = null;
        }
    }
    public void ResetEquipment()
    {
        for(int i = 0;i < EquipmentManager.Inst.equipslot.Count; i++)
        {
            Destroy(EquipmentManager.Inst.equipslot[i].mySlotItems.gameObject);
            EquipmentManager.Inst.equipslot[i].mySlotItems = null;
        }
    }
    public void ResetPlayerData()
    {
        Gamemanager.Inst.myPlayer.curHp = Gamemanager.inst.myPlayer.MaxHp;
        Gamemanager.Inst.Money = 0;
        EnchantManager.Inst.EnchantLevel = 0;
    }

    #endregion
    

    public void OpenSaveUI()
    {

    }

}
