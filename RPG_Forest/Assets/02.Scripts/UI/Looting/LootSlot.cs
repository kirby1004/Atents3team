using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LootSlot : MonoBehaviour , IPointerClickHandler
{
    public ItemStatus myItem;

    public TMP_Text myItemName;
    public Image myItemImage;

    public UnityAction myAction;
    public Sprite emptyImage;
    public int myIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        myItemName = GetComponentInChildren<TMP_Text>();
        myItemImage.sprite = GetComponentInChildren<Image>().sprite;
        myItemName.text = myItem.ItemName;
        myItemImage.sprite = myItem.Image;
        myAction += LootDone;
    }

    // 클릭으로 아이템 획득 기능
    public void LootDone()
    {
        // 인벤토리가 비어있다면
        if(InventoryManager.Inst.FindEmptySlot() != -1)
        {
            myItemImage.sprite = emptyImage;
            myItemName.text = null;
            InventoryManager.Inst.AddItem(myItem);
            transform.parent.GetComponent<DropList>().LootLeftCount?.Invoke();
        }
        // 가득차있다면 인벤토리 풀 이벤트가 발생하게 할예정
        else
        {

            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        myAction?.Invoke();
    }
}
