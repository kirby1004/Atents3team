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
    // �Լ� ȣ�� ������ 2��
    public UnityAction<Monster> RefreshLootTarget;
    public UnityAction<Monster> DelLootTarget;

    // ���� �����մ� ����â
    public GameObject LootWindow;

    // myLootingMonster[i] �� myMonster�� ���� ���� ����â�� LootingWindowList[i] �� ������Ʈ
    // ���������� ���ù������Կ��θ� ����ִ� ����Ʈ
    public List<(Monster myMonster,bool IsEnter)> myLootingMonster = new();
    // �����ִ� ����â�� ����� ����մ� ����Ʈ
    public List<GameObject> LootWindowList = new();

    // ����â ������� �� ���۹���� ����Ǿ� ���Ұ���
    //public void SpawnLootWindow(ItemDropTable itemDropTable)
    //{
    //    //GameObject obj = Instantiate(LootWindow, transform);
    //    GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
    //    LootWindow = obj;
    //    obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
       
    //}

    // �Է¹��� ������ ������ ����â �����ϴ� �Լ�
    public void ReadyLootWindow(Monster myMonster)
    {
        ItemDropTable itemDropTable = myMonster.myDropTable;
        if (SearchListMonster(myMonster)) return;
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
        //LootWindow = obj;
        obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
        myLootingMonster.Add((myMonster,false));
        LootWindowList.Add(obj);
        //obj.GetComponent<DropList>().IsEnterLoot = true;

        obj.SetActive(false);
    }

    #region myLootWIndows ����Ʈ Ž�� �� ���� �Լ�
    // ���ð����� ���Ͱ� �ִ��� Ž���� �ε����� ��ȯ�ϴ��Լ�
    public int SearchIndexLootList()
    {
        for (int i = 0; i < myLootingMonster.Count; i++)
        {
            if (myLootingMonster[i].IsEnter == true)
            {
                return i;
            }
        }
        return -1;
    }
    // �Է¹��� ���Ͱ� ����â�� �����޴��� ���θ� ��ȯ�ϴ� �Լ�
    public bool SearchListMonster(Monster monster)
    {
        for (int i = 0; i < myLootingMonster.Count; i++)
        {
            if(myLootingMonster[i].myMonster == monster) return true;
        }
        return false;
    }
    // ���ð����� ���Ͱ� �����ϴ��� ��ȯ�ϴ� �Լ�
    public bool SearchLootList()
    {
        for (int i = 0; i < myLootingMonster.Count; i++)
        {
            if (myLootingMonster[i].IsEnter == true)
            {
                return true;
            }
        }
        return false;
    }
    // ����â�� �����ϸ� ������ ���� �� ����Ʈ�� �ε������������ϴ� �Լ�
    public void RemoveWindows(Monster monster)
    {
        //int index = -1;
        for (int i = 0; i < LootWindowList.Count; i++)
        {
            if (myLootingMonster[i].myMonster == monster)
            {
                GameManager.instance.myPlyaer.SetIsEnterUI(false);
                Destroy(LootWindowList[i].gameObject);
                myLootingMonster[i].myMonster.ColDelete?.Invoke();
                myLootingMonster[i].myMonster.OnDisappear();
                LootWindowList.RemoveAt(i);
                myLootingMonster.RemoveAt(i);
                break;
            }
        }
    }
    // ������ ���ð��ɻ��¸� ���Ž����ִ� �Լ�
    public void ListIsEnterUpdate(Monster monster,bool isEnter)
    {
        for(int i = 0;i < myLootingMonster.Count; i++)
        {
            if (myLootingMonster[i].myMonster == monster)
            {
                myLootingMonster[i] = (monster, isEnter);
            }
        }
    }
    // �ش� ���ͷ� ���� ���� ����â�� �����ϸ� �����ִ� �Լ�
    public void OpenLootWindow(Monster mymonster)
    {
        for (int i = 0; i < myLootingMonster.Count; i++)
        {
            if(myLootingMonster[i].myMonster == mymonster)
            {
                LootWindowList[i].gameObject.SetActive(true);
                //LootWindowList[i].GetComponentInChildren<DropList>().ImageRaycastOn();
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
