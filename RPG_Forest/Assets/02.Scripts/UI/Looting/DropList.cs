
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
        //ImageRaycastOff();
    }

    // Update is called once per frame
    void Update()
    {
        if(LootingManager.Inst.LootWindowList != null)
        {
            if (LootingManager.Inst.LootWindowList[0] == this)
            {
                ImageRaycastOn();
            }
        }
        if (myLootCount == 0)
        {
            GameManager.instance.myPlyaer.GetComponent<IinterPlay>().CloseUi?.Invoke();
            LootingManager.Inst.LootWindow = null;
            Destroy(transform.parent.gameObject);
        }
    }

    // ������̺� �ִ� �������� �����üũ�� ����Ǵ¾������� �������ִ� �Լ�
    void ConfirmItem(ItemDropTable mydropTable)
    {
        for (int i = 0; i < mydropTable.myDropTable.Count(); i++)
        {
            if (LootingManager.Inst.ProbabilityChoose(mydropTable.myDropRate[i]))
            {
                SpawnLootSlot(mydropTable.myDropTable[i]);
            }
        }
    }

    public GameObject LootSlot;

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
        GameManager.instance.myPlyaer.GetComponent<IinterPlay>().CloseUi?.Invoke();
        LootingManager.Inst.LootWindow = null;
        Destroy(transform.parent.gameObject);
    }

    // ��� ImageRaycast�� ���ִ� �Լ�
    public void ImageRaycastOff()
    {
        Image[] myImage = transform.parent.GetComponentsInChildren<Image>();
        foreach (Image img in myImage)
        {
            img.raycastTarget = false;
        }
    }
    // ��� ImageRaycast�� ���ִ� �Լ�
    public void ImageRaycastOn()
    {
        Image[] myImage = transform.parent.GetComponentsInChildren<Image>();
        foreach(Image img in myImage)
        {
            img.raycastTarget = true;
        }
    }

}
