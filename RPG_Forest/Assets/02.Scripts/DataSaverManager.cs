using Mono.Cecil;
using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float curHP;
    public Vector3[] PortalRocate;
}
[System.Serializable]
public class PlayerInfo
{
    public int Soul;
    public int Level;
}
public class TestData
{
    public PlayerData PlayerData;
    public PlayerInfo PlayerInfo;
}

public class DataSaverManager : MonoBehaviour
{
    //public PlayerData playerData;
    
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
            TestFunc();
        }
    }


    public void TestFunc()
    {
        testData.PlayerData.curHP = GameManager.inst.myPlayer.curHp;
        //for(int i = 0;i < InventoryManager.Inst.startSlotcount;i++)
        //{
        //    testData.PlayerInventory.InventoryItems[i] = 
        //        InventoryManager.Inst.slots[i].mySlotItems.GetComponent<Item>();
        //}
        testData.PlayerInfo.Soul = GameManager.inst.Money;
        testData.PlayerInfo.Level = EnchantManager.Inst.EnchantLevel;
        string data = JsonUtility.ToJson(testData);
        Debug.Log(Application.dataPath);
        
        System.IO.File.WriteAllText(Application.dataPath+ "/12.JSON/Resources/PlayerData/PlayerData.json", data);
    }

}
