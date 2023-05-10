using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler , IPointerClickHandler
{

    // �������� ����
    public ItemStatus item;
    //������ �̹���
    public Image image;
    public Image myIcon = null;
    public Transform parentAfterDrag;

    public ItemSlotType slotType = ItemSlotType.Inventory;
    Color color = Color.white;
    // ������ ������ �������� (�߰����� ���� �����)
    // Start is called before the first frame update
    //private void Start()
    //{
    //    InitialiseItem(item);
    //}

    // ������ ������ ��������
    public void InitialiseItem(ItemStatus newItem)
    {
        item = newItem;
        image.sprite = newItem.Image;
        Color color = image.color;
        color.a = 1;
        image.color = color;
        slotType = ItemSlotType.Inventory;
    }

    // ���콺 �巡�� 
    Vector2 mousePos = Vector2.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePos = (Vector2)transform.position - eventData.position;
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.parent.parent.parent.parent);
        transform.SetAsLastSibling();
        //transform.SetParent(transform.parent);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + mousePos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
    }


    // �Է¹��� Ʈ�������� �θ� ��ȯ false �϶��� ������� , true �϶��� �θ𺯰��� �������̵�����
    public void ChangeParent(Transform p, bool update = false)
    {
        parentAfterDrag = p;
        if (update)
        {
            transform.SetParent(p);
            transform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(transform.GetComponent<QuickItem>() != null)
        {
            if (Mathf.Approximately(myIcon.fillAmount, 1.0f))
            {
                myIcon.fillAmount = 0.0f;
                color.a = 0.0f;
                image.color = color;
                StopAllCoroutines();
                StartCoroutine(Cooling());
            }
        }
    }

    IEnumerator Cooling()
    {
        float speed = 1.0f / item.ItemCoolDown;
        while (myIcon.fillAmount < 1)
        {
            myIcon.fillAmount += speed * Time.deltaTime;
            yield return null;
        }
        color.a = 1.0f;
        image.color = color;
    }

}
