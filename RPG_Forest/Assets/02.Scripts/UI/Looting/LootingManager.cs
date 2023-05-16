using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LootingManager : Singleton<LootingManager>
{

    private void Awake()
    {
        base.Initialize();
    }


    public UnityAction ItemDrop;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject LootWindow;
    public Dictionary<Monster, bool> LootQueue = new Dictionary<Monster, bool>();

    public List<(Monster,GameObject,bool)> LootList = new List<(Monster, GameObject, bool)>();

    //public void SpawnLootWindow(ItemDropTable itemDropTable, Transform transform)
    //{
    //    GameObject obj = Instantiate(LootWindow, transform);
    //    obj.GetComponent<DropList>().myDropTable = itemDropTable;
    //    ItemDrop += () => obj.SetActive(true);
    //    //GameObject obj = Instantiate(Resources.Load("") as GameObject, transform);
    //}

    public Monster LootQueueSearch()
    {
        for (int i = 0; i < LootQueue.Count; i++)
        {
            if (LootQueue[LootList[i].Item1] == true)
            {
                //LootList[i].
                return LootList[i].Item1;
            }
        }
        return null;
    }

    public void SpawnLootWindow(ItemDropTable itemDropTable)
    {
        //GameObject obj = Instantiate(LootWindow, transform);
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
        LootWindow = obj;
        obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
        ItemDrop += () => obj.SetActive(true);
    }

    public void ReadyLootWindow(Monster myMonster)
    {
        ItemDropTable itemDropTable = myMonster.myDropTable;
        //GameObject obj = Instantiate(LootWindow, transform);
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
       // LootWindow = obj;
        obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
        LootList.Add((myMonster, obj, true));
        LootQueue.Add(myMonster, true);
        obj.SetActive(false);
    }

    public void OpenLootWindow(Monster mymonster)
    {
        if(LootWindow == null)
        {
            
        }
        else
        {
            return;
        }
    }


    // DropRate Calculate
    public bool ProbabilityChoose(float Rate)
    {
        //float Percentge = Rate / 100;
        if (Random.Range(0, 100) < Rate)
        {
            return true;
        }
        return false;
    }


}
