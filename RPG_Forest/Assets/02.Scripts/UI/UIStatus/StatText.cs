using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatText : MonoBehaviour
{
    public TMP_Text text;

    public StatType type;

    private void Update()
    {
        //Debug.Log(Gamemanager.instance.myUIManager.equipmentManager.equipmentAP);
        switch (type)
        {
            case StatType.HP:
                text.text = Gamemanager.instance.myUIManager.equipmentManager.equipmentHP.ToString();
                break;
            case StatType.AP:
                text.text = Gamemanager.instance.myUIManager.equipmentManager.equipmentAP.ToString();
                break;
            case StatType.DP:
                text.text = Gamemanager.instance.myUIManager.equipmentManager.equipmentDP.ToString();
                break;
            case StatType.Speed:
                text.text = Gamemanager.instance.myUIManager.equipmentManager.equipmentSpeed.ToString();
                break;
            case StatType.Range:
                break;
            default:
                break;
        }
    }
}

