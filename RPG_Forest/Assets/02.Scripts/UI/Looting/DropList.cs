using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class DropList : MonoBehaviour
{
    public ItemDropTable myDropTable;

    public List<LootSlot> myLootSlots;

    public int myLootCount;
    public UnityAction LootLeftCount;
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
            Destroy(transform.parent.gameObject);
    }

    void ConfirmItem(ItemDropTable mydropTable)
    {
        for(int i = 0; i < mydropTable.myDropTable.Count(); i++)
        {
            if (LootingManager.Inst.ProbabilityChoose(mydropTable.myDropRate[i]))
            {
                SpawnLootSlot(mydropTable.myDropTable[i]);
            }
        }
    }

    public GameObject LootSlot;
     
    void SpawnLootSlot(ItemStatus itemStatus)
    {
        //GameObject obj = Instantiate(LootSlot, transform);
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootSlot") as  GameObject, transform);
        obj.GetComponent<LootSlot>().myItem = itemStatus;
        myLootSlots.Add(obj.GetComponent<LootSlot>());
        obj.GetComponent<LootSlot>().myIndex = myLootSlots.Count - 1;
    }

    public void LootAll()
    {
        for(int i = 0  ; i < myLootSlots.Count; i++)
        {
            myLootSlots[i].GetComponent<LootSlot>().LootDone();
        }
        Destroy(transform.parent.gameObject);
    }
}
