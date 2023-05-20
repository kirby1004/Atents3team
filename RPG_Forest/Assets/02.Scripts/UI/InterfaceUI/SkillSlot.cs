using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillData mySkillData;

    public GameObject myIcon = null;


    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new("Icon");
        obj.transform.SetParent(transform);
        myIcon = obj;
        obj.AddComponent<SlotCoolDown>();
        obj.AddComponent<Image>();
        obj.GetComponent<SlotCoolDown>().myParent = transform;
        GetComponent<Image>().sprite = mySkillData.Image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
