using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedItem : MonoBehaviour
{
    Transform myParent;
    // Start is called before the first frame update
    void Start()
    {
        myParent = transform.parent;
    }

    private void OnDestroy()
    {
        myParent.GetComponent<UseItemSlot>().myitem = null;
    }
}
