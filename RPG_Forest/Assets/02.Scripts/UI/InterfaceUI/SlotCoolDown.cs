using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotCoolDown : MonoBehaviour
{
    public Transform myParent;
    public Image myImage;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(transform);
        transform.localPosition = Vector3.zero;
        GetComponent<RectTransform>().sizeDelta = myParent.GetComponent<RectTransform>().sizeDelta;
        myImage = GetComponent<Image>();
        myImage.sprite = myParent.GetComponent<SkillSlot>().mySkillData.Image;
        myImage.type = Image.Type.Filled;
        myImage.fillOrigin = 2;

        //myParent.GetComponent<SkillSlot>().myIcon = myImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCooldown(float myCooldown)
    {
        if(Mathf.Approximately(myImage.fillAmount,1.0f))
        {
            myImage.fillAmount = 0.0f;
            StopAllCoroutines();
            StartCooldown(myCooldown);
        }
    }

    IEnumerator Cooldowns(float myCooldown)
    {

        while (myImage.fillAmount < 1.0f)
        {
            myImage.fillAmount += Time.deltaTime / myCooldown;
            yield return null;
        }
    }
}
