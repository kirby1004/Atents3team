using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler , IPointerClickHandler
{
    // �� ������ ���� , ���� , �̹���
    public ItemStatus myItem;
    public int myCost;
    public Image myImage;
    // ���Ź�ư
    public Button buyButton;

    // ���� Ȯ���� �κ��丮�� ������ �߰�
    public void BuyItem(ItemStatus item,bool isDone)
    {
        if (isDone)
        {
            //Destroy(GetComponent<ShopItem>());
            InventoryManager.Inst.AddItem(item);
        }
    }

    //���콺���� ������
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OFF");
    }

    // ���콺���� �ɶ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���� ����â ���� => ���콺 ������ ���� �������� ���� , ���� �����ͼ� �Ѱ��ֱ�
        ShopManager.Inst.itemInfo.RefreshItemInfo
            (eventData.pointerEnter.GetComponent<ShopItem>().myItem);
        ShopManager.Inst.itemInfo.myCost = myCost;

    }

    // Ŭ���� �������� ���� ������� �����ϱ�
    // �߰����� ��� 1. ���� Ȯ��â �߰� �ϱ�
    //               2. ���Ŵ�� ������ �ٸ������� ���콺�����ȵǰ� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        buyButton.gameObject.SetActive(true);
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() =>gameObject.GetComponent<ShopItem>().BuyItem(myItem,true));
    }
}
