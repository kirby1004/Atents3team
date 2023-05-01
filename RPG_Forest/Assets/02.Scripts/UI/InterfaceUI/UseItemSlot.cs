using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseItemSlot : MonoBehaviour , IDropHandler , IPointerClickHandler
{
    public InventoryItem myitem;
    public float myCoolDown;

    public void OnDrop(PointerEventData eventData)
    {
        // Drag �� �������� ���������ϰ�� �������� �ű�� => �������� �϶��� �Űܿ���
        // ���Կ� �������� ������ �״�� �ű�� ������ �ٲٱ�
        InventoryItem dropItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (dropItem != null)
        {        
            if(dropItem.item.actionType == ActionType.UsedItem)
            { 
                if(transform.GetComponentInChildren<InventoryItem>() == null)
                {
                    dropItem.ChangeParent(transform,true);
                    dropItem.AddComponent<UsedItem>();
                    myitem = dropItem;
                }
                else
                {
                    transform.GetComponentInChildren<InventoryItem>().ChangeParent(dropItem.parentAfterDrag,true);
                    dropItem.ChangeParent(transform, true);
                    myitem = dropItem;
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
