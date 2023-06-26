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

public class DataSaverManager : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {

        TextAsset textAsset = Resources.Load<TextAsset>("PlayerData/PlayerData");

        testData = JsonUtility.FromJson<TestData>(textAsset.ToString());
        //TestFunc();
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
        TextAsset textAsset = Resources.Load<TextAsset>("PlayerData/PlayerData");

        testData = JsonUtility.FromJson<TestData>(textAsset.ToString());

        testData.PlayerData.curHP = Gamemanager.inst.myPlayer.curHp;
        testData.PlayerData.Soul = Gamemanager.inst.Money;
        testData.PlayerData.Level = EnchantManager.Inst.EnchantLevel;
        testData.PlayerData.LastMapIndex = SceneManager.GetActiveScene().buildIndex;
        testData.PlayerData.LastRocate = Gamemanager.inst.myPlayer.gameObject.transform.position;
    }
    public void SavePlayerInventoryData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("PlayerData/PlayerData");

        testData = JsonUtility.FromJson<TestData>(textAsset.ToString());

        for (int i = 0; i < EquipmentManager.Inst.equipslot.Count - 1; i++)
        {
            //testData.PlayerInventory.EquipmentItemCode[i] = EquipmentManager.Inst.equipslot[i].mySlotItems.GetComponent<ItemStatus>();
        }
        for (int i = 0; i < InventoryManager.Inst.slots.Count; i++)
        {
            //testData.PlayerInventory.InventoryItemCode[i] = InventoryManager.Inst.slots[i].itemCodes;
        }

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
