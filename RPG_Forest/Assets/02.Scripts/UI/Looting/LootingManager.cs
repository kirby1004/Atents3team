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
        OpenTarget += PossibleOpenLoot;
        RemoveTarget += RemoveMonster;
        LootWindow = null;
    }

    public UnityAction OpenTarget;
    public UnityAction<Monster> RemoveTarget;

    // 현재 열려잇는 루팅창
    public GameObject LootWindow;


#region 루팅버그개선완료버전

    // 몬스터의 루팅창 연동
    public List<(Monster myMonster, GameObject myWindow)> LootWindows = new();
    // 루팅창을 열수 있는 몬스터
    public List<Monster> PossibleList = new();

    // 루팅창 생성
    public void CreateLootWindow(Monster monster)
    {
        ItemDropTable itemDropTable = monster.myDropTable;
        if (PossibleList.Contains(monster)) return;
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject,transform);
        obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
        LootWindows.Add((monster, obj));
        PossibleList.Add(monster);
        obj.SetActive(false);
    }

    // 루팅 가능한 맨앞의것 열기
    public void PossibleOpenLoot()
    {
        if(PossibleList.Count == 0) return;
        OpenLootWindow(PossibleList[0]);
    }

    // 해당몬스터의 루팅창 열기
    public void OpenLootWindow(Monster monster)
    {
        if(LootWindow == null)
        {
            for(int i = 0;i < LootWindows.Count; i++)
            {
                if (LootWindows[i].myMonster == monster)
                {
                    LootWindows[i].myWindow.SetActive(true);
                    LootWindow = LootWindows[i].myWindow;
                    Gamemanager.inst.myPlayer.SetisUI(true);
                    break;
                }
            }
        }
    }

    // 몬스터가 루팅가능상태인지 판단하기
    public bool CanOpenWindow(Monster monster)
    {
        if(PossibleList.Contains(monster)) { return true; }
        return false;
    }
    
    // 몬스터가 루팅범위 밖에있을때 제거하기
    public void RemoveMonster(Monster monster)
    {
        if (PossibleList.Contains(monster))
        {
            PossibleList.Remove(monster);
        }
    }

    // 현재 루팅창이 몇번째 인덱스인지 찾기
    public int WindowIndexFind(GameObject myWindow)
    {
        for (int i = 0; i < LootWindows.Count; i++)
        {
            if (LootWindows[i].myWindow == myWindow)
            {
                return i;
            }
        }
        return -1;
    }

    // 루팅창 닫기
    public void CloseWindows()
    {
        int index = WindowIndexFind(LootWindow); 
        if (index != -1) 
        {
            Destroy(LootWindows[index].myWindow);
            LootWindows[index].myMonster.OnDisappear();
            LootWindows.RemoveAt(index);
            PossibleList.RemoveAt(0);
            LootWindow = null;
            Gamemanager.inst.myPlayer.SetisUI(false);
            if(PossibleList.Count == 0) 
            {
                Gamemanager.inst.myPlayer.OpenUi.RemoveAllListeners();
                Gamemanager.inst.myPlayer.CloseUi.RemoveAllListeners();
            }
        }
    }

#endregion


}
