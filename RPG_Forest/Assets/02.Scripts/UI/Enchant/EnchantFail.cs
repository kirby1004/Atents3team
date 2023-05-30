using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnchantFail : MonoBehaviour
{
    // true => 강화시도 실패 , false => 요금지불 실패
    bool _isType= false;
    public bool isType
    {
        get => _isType;
        set
        {
            _isType = value;
            isFail.Invoke(value);
        }
    }

    public UnityEvent<bool> isFail;

    public TMP_Text myText;
    
    void OnEnable()
    {
        isFail.AddListener(SetText);
    }



    public void SetText(bool isType)
    {
        if (isType)
        {
            myText.text = "강화에\n실패했습니다.";
        }
        else
        {
            myText.text = "소울이\n모자랍니다.";
        }
    }

}
