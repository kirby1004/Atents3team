using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LootSlot : MonoBehaviour , IPointerClickHandler ,IPointerEnterHandler , IPointerExitHandler ,IPointerMoveHandler
{
    public ItemStatus myItem;

    public TMP_Text myItemName;
    public Image myItemImage;

    public UnityAction myAction;
    public Sprite emptyImage;
    public int myIndex = -1;
    bool isLoot = false;
    // Start is called before the first frame update
    // ���Թ��� myItem ������ �������� ���� ���ε����� ����
    void Start()
    {
        myItemName = GetComponentInChildren<TMP_Text>();
        myItemImage.sprite = GetComponentInChildren<Image>().sprite;
        myItemName.text = myItem.ItemName;
        myItemImage.sprite = myItem.Image;
        myAction += LootDone;
    }

    // Ŭ������ ������ ȹ�� ���
    public void LootDone()
    {
        // �κ��丮�� ����ִٸ�
        if(InventoryManager.Inst.FindEmptySlot() != -1)
        {
            myItemImage.sprite = emptyImage;
            myItemName.text = null;
            InventoryManager.Inst.AddItem(myItem);
            transform.parent.GetComponent<DropList>().LootLeftCount?.Invoke();
            isLoot = true;
        }
        // �������ִٸ� �κ��丮 Ǯ �̺�Ʈ�� �߻��ϰ� �ҿ���
        else
        {

            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        myAction?.Invoke();
        if(MouseOverWindows != null)
        {
            Destroy(MouseOverWindows.gameObject);   
        }
    }
    public GameObject MouseOverWindows;
    // ���콺�� �ö���� �˾�â�� �߰� �ϱ�
    // ���콺�� �����̳� ���� ��ܿ� �˾�â�� �߰� ��ġ�� �����ǵ��� �ϱ�
    Vector2 mousePos = Vector2.zero;
    Vector2 sumPos = Vector2.zero;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(MouseOverWindows == null)
        {
            if(isLoot == false)
            {
                MouseOverWindows = Instantiate(Resources.Load("UIResource/MouseOverWindow/MouseOverWindow") as GameObject,
                    LootingManager.Inst.LootWindow.transform);
                MouseOverWindows.GetComponent<MouseOverWindow>().myItem = myItem;
                mousePos = (Vector2)transform.position - eventData.position;
                sumPos = new Vector2(MouseOverWindows.GetComponent<RectTransform>().sizeDelta.x / 2,
                    MouseOverWindows.GetComponent<RectTransform>().sizeDelta.y / 2);
                MouseOverWindows.transform.position = eventData.position + sumPos;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(MouseOverWindows != null)
        {
            if (isLoot == false)
            {

                Destroy(MouseOverWindows);
                MouseOverWindows = null;
            }
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (isLoot == false)
        {
            MouseOverWindows.transform.position = eventData.position + sumPos;
        }

        Debug.Log(eventData.position);
    }
}
