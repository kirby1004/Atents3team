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

    // ���� �����մ� ����â
    public GameObject LootWindow;


#region ���ù��װ����Ϸ����

    // ������ ����â ����
    public List<(Monster myMonster, GameObject myWindow)> LootWindows = new();
    // ����â�� ���� �ִ� ����
    public List<Monster> PossibleList = new();

    // ����â ����
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

    // ���� ������ �Ǿ��ǰ� ����
    public void PossibleOpenLoot()
    {
        if(PossibleList.Count == 0) return;
        OpenLootWindow(PossibleList[0]);
    }

    // �ش������ ����â ����
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

    // ���Ͱ� ���ð��ɻ������� �Ǵ��ϱ�
    public bool CanOpenWindow(Monster monster)
    {
        if(PossibleList.Contains(monster)) { return true; }
        return false;
    }
    
    // ���Ͱ� ���ù��� �ۿ������� �����ϱ�
    public void RemoveMonster(Monster monster)
    {
        if (PossibleList.Contains(monster))
        {
            PossibleList.Remove(monster);
        }
    }

    // ���� ����â�� ���° �ε������� ã��
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

    // ����â �ݱ�
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
