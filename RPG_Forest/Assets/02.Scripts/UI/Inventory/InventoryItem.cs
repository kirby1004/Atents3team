using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour , IBeginDragHandler, IEndDragHandler ,IDragHandler
{
    // 아이템의 정보
    public ItemStatus item;
    //아이템 이미지
    public Image image;
    
    public Transform parentAfterDrag;

    // 아이템 생성시 정보갱신 (추가할지 말지 고민중)
    // Start is called before the first frame update
    //private void Start()
    //{
    //    InitialiseItem(item);
    //}

    // 아이템 생성시 정보주입
    public void InitialiseItem(ItemStatus newItem)
    {
        item = newItem;
        image.sprite = newItem.Image;
        Color color = image.color;
        color.a = 1;
        image.color = color;
    }

    // 마우스 드래그 
    Vector2 mousePos = Vector2.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePos = (Vector2)transform.position - eventData.position;
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent);
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


    // 입력받은 트랜스폼과 부모 교환 false 일때는 참조대상만 , true 일때는 부모변경후 포지션이동까지
    public void ChangeParent(Transform p, bool update = false)
    {
        parentAfterDrag = p;
        if (update)
        {
            transform.SetParent(p);
            transform.localPosition = Vector3.zero;
        }
    }



}
