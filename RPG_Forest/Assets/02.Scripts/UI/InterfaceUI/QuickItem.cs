using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickItem : MonoBehaviour , IItems
{
    public Component myState
    {
        get => this as Component;
    }

    // »ý¼º½Ã ÄðÅ¸ÀÓ ÀÌÆåÆ®¿ë °´Ã¼ 
    private void Start() 
    {
        transform.parent.GetComponent<Slot>().mySlotItems = transform;
        GameObject obj = new("Icon");
        obj.transform.SetParent(transform);
        obj.AddComponent<SlotCoolDown>();
        obj.AddComponent<Image>();
        obj.GetComponent<SlotCoolDown>().myParent = transform;

    }
    private void OnDestroy()
    {
        transform.parent.GetComponent<Slot>().mySlotItems = null;
    }

}
