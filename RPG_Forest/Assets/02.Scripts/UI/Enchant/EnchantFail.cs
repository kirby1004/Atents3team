using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnchantFail : MonoBehaviour
{
    // true => ��ȭ�õ� ���� , false => ������� ����
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
            myText.text = "��ȭ��\n�����߽��ϴ�.";
        }
        else
        {
            myText.text = "�ҿ���\n���ڶ��ϴ�.";
        }
    }

}
