using System.Collections;
using System.Collections.Generic;
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
    
    public TestData testData;

    private void Awake()
    {
        base.Initialize();
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {

        TextAsset textAsset = Resources.Load<TextAsset>("PlayerData/PlayerData");

        testData = JsonUtility.FromJson<TestData>(textAsset.ToString());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveAllData();
        }
    }

    public void SaveAllData()
    {
        SavePlayerData();
        SavePlayerInventoryData();
    }
    public void SavePlayerData()
    {
        LoadJsonFile();

        testData.PlayerData.curHP = Gamemanager.inst.myPlayer.curHp;
        testData.PlayerData.Soul = Gamemanager.inst.Money;
        testData.PlayerData.Level = EnchantManager.Inst.EnchantLevel;
        testData.PlayerData.LastMapIndex = SceneManager.GetActiveScene().buildIndex;
        testData.PlayerData.LastRocate = Gamemanager.inst.myPlayer.gameObject.transform.position;
        string data = JsonUtility.ToJson(testData);
        Debug.Log(Application.dataPath);

        System.IO.File.WriteAllText(Application.dataPath + "/12.JSON/Resources/PlayerData/PlayerData.json", data);

    }
    public void SavePlayerInventoryData()
    {
        LoadJsonFile();
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

        string data = JsonUtility.ToJson(testData);
        Debug.Log(Application.dataPath);

        System.IO.File.WriteAllText(Application.dataPath + "/12.JSON/Resources/PlayerData/PlayerData.json", data);

    }
    public void LoadJsonFile()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("PlayerData/PlayerData");

        testData = JsonUtility.FromJson<TestData>(textAsset.ToString());
    }

    public void TestFunc()
    {
        testData.PlayerData.curHP = Gamemanager.inst.myPlayer.curHp;
        //for(int i = 0;i < InventoryManager.Inst.startSlotcount;i++)
        //{
        //    testData.PlayerInventory.InventoryItems[i] = 
        //        InventoryManager.Inst.slots[i].mySlotItems.GetComponent<Item>();
        //}
        testData.PlayerData.Soul = Gamemanager.inst.Money;
        testData.PlayerData.Level = EnchantManager.Inst.EnchantLevel;
        string data = JsonUtility.ToJson(testData);
        Debug.Log(Application.dataPath);
        
        System.IO.File.WriteAllText(Application.dataPath+ "/12.JSON/Resources/PlayerData/PlayerData.json", data);
    }

}
