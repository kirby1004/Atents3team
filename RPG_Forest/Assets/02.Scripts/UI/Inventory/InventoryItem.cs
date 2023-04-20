using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour , IBeginDragHandler, IEndDragHandler ,IDragHandler
{
    public ItemStatus item;

    public Image image;
    public Transform parentAfterDrag;
    
    // Start is called before the first frame update
    //private void Start()
    //{
    //    InitialiseItem(item);
    //}
    public void InitialiseItem(ItemStatus newItem)
    {
        item = newItem;
        image.sprite = newItem.Image;
        Color color = image.color;
        color.a = 1;
        image.color = color;
    }
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
