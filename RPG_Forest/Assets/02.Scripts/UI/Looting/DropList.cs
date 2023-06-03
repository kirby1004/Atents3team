
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

    // 드랍테이블에 있는 아이템을 드랍률체크후 드랍되는아이템을 생성해주는 함수
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

    // 루팅창 내부의 루팅가능한 아이템슬롯을 생성 후 아이템 정보를 추가해주는 함수
    void SpawnLootSlot(ItemStatus itemStatus)
    {
        //GameObject obj = Instantiate(LootSlot, transform);
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootSlot") as GameObject, transform);
        obj.GetComponent<LootSlot>().myItem = itemStatus;
        myLootSlots.Add(obj.GetComponent<LootSlot>());
        obj.GetComponent<LootSlot>().myIndex = myLootSlots.Count - 1;
    }

    // 모든아이템을 루팅처리해주는 함수
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
