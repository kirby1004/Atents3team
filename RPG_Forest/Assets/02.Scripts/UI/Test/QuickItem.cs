using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuickItem : MonoBehaviour , IItems
{
    public Component myState
    {
        get => this as Component;
    }

    // ������ ��Ÿ�� ����Ʈ�� ��ü 
    private void Start() 
    {
        transform.parent.GetComponent<Slot>().mySlotItems = transform;
    }
    private void OnDestroy()
    {
        transform.parent.GetComponent<Slot>().mySlotItems = null;
    }

}
