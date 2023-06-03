
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DropList : MonoBehaviour
{
    public ItemDropTable myDropTable;

    public List<LootSlot> myLootSlots;

    public int myLootCount;
    public UnityAction LootLeftCount;
    public bool IsEnterLoot = false;

    // Start is called before the first frame update
    void Start()
    {
        ConfirmItem(myDropTable);
        myLootCount = myLootSlots.Count;
        LootLeftCount += () => myLootCount--;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (myLootCount == 0)
        {
            Gamemanager.Inst.myPlayer.CloseUi?.Invoke();
        }
    }

    // ������̺� �ִ� �������� �����üũ�� ����Ǵ¾������� �������ִ� �Լ�
    void ConfirmItem(ItemDropTable mydropTable)
    {
        for (int i = 0; i < mydropTable.myDropTable.Count(); i++)
        {
            if (Gamemanager.Inst.ProbabilityChoose(mydropTable.myDropRate[i]))
            {
                SpawnLootSlot(mydropTable.myDropTable[i]);
            }
        }
    }

    // ����â ������ ���ð����� �����۽����� ���� �� ������ ������ �߰����ִ� �Լ�
    void SpawnLootSlot(ItemStatus itemStatus)
    {
        //GameObject obj = Instantiate(LootSlot, transform);
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootSlot") as GameObject, transform);
        obj.GetComponent<LootSlot>().myItem = itemStatus;
        myLootSlots.Add(obj.GetComponent<LootSlot>());
        obj.GetComponent<LootSlot>().myIndex = myLootSlots.Count - 1;
    }

    // ���������� ����ó�����ִ� �Լ�
    public void LootAll()
    {
        for (int i = 0; i < myLootSlots.Count; i++)
        {
            myLootSlots[i].GetComponent<LootSlot>().LootDone();
        }
        Gamemanager.Inst.myPlayer.CloseUi?.Invoke();
        LootingManager.Inst.LootWindow = null;
        Destroy(transform.parent.gameObject);
    }

}
