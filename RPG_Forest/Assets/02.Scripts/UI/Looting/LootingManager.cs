using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class LootingManager : Singleton<LootingManager>
{

    private void Awake()
    {
        base.Initialize();
    }


    // Start is called before the first frame update
    void Start()
    {
        RefreshLootTarget += OpenLootWindow;
        DelLootTarget += RemoveWindows;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public UnityAction<Monster> RefreshLootTarget;
    public UnityAction<Monster> DelLootTarget;

    public GameObject LootWindow;

    public List<(Monster myMonster,bool IsEnter)> myLootWindows = new();

    public List<GameObject> LootWindowList = new();


    public void SpawnLootWindow(ItemDropTable itemDropTable)
    {
        //GameObject obj = Instantiate(LootWindow, transform);
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
        LootWindow = obj;
        obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
       
    }

    public void ReadyLootWindow(Monster myMonster)
    {
        ItemDropTable itemDropTable = myMonster.myDropTable;
        if (SearchListMonster(myMonster)) return;
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
        //LootWindow = obj;
        obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
        myLootWindows.Add((myMonster,false));
        LootWindowList.Add(obj);
        //obj.GetComponent<DropList>().IsEnterLoot = true;

        obj.SetActive(false);
    }

    #region myLootWIndows 리스트 탐색 및 수정 함수
    public int SearchIndexLootList()
    {
        for (int i = 0; i < myLootWindows.Count; i++)
        {
            if (myLootWindows[i].IsEnter == true)
            {
                return i;
            }
        }
        return -1;
    }

    public bool SearchListMonster(Monster monster)
    {
        for (int i = 0; i < myLootWindows.Count; i++)
        {
            if(myLootWindows[i].myMonster == monster) return true;
        }
        return false;
    }

    public bool SearchLootList()
    {
        for (int i = 0; i < myLootWindows.Count; i++)
        {
            if (myLootWindows[i].IsEnter == true)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveWindows(Monster monster)
    {
        int index = -1;
        for (int i = 0; i < LootWindowList.Count; i++)
        {
            if (myLootWindows[i].myMonster == monster)
            {
                Gamemanager.instance.myPlyaer.SetIsEnterUI(false);
                Destroy(LootWindowList[i].gameObject);
                myLootWindows[i].myMonster.ColDelete?.Invoke();
                myLootWindows[i].myMonster.OnDisappear();
                LootWindowList.RemoveAt(i);
                myLootWindows.RemoveAt(i);
                break;
            }
        }
    }

    public void ListIsEnterUpdate(Monster monster,bool isEnter)
    {
        for(int i = 0;i < myLootWindows.Count; i++)
        {
            if (myLootWindows[i].myMonster == monster)
            {
                myLootWindows[i] = (monster, isEnter);
            }
        }
    }
     
    public void OpenLootWindow(Monster mymonster)
    {
        for (int i = 0; i < myLootWindows.Count; i++)
        {
            if(myLootWindows[i].myMonster == mymonster)
            {
                LootWindowList[i].gameObject.SetActive(true);
                LootWindowList[i].GetComponentInChildren<DropList>().ImageRaycastOn();
                LootWindow = LootWindowList[i].gameObject; 
                break;
            }
        }
    }
    #endregion

    // DropRate Calculate
    public bool ProbabilityChoose(float Rate)
    {
        //float Percentge = Rate / 100;
        if (Random.Range(0, 101) < Rate)
        {
            return true;
        }
        return false;
    }


}
