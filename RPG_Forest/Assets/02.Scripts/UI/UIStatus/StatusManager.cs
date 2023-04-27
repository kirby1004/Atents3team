using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public TMP_Text[] myStat;


    public void RefreshingStat()
    {
        for (int i = 0; i < myStat.Length; i++)
        {
            switch ((StatType)i)
            {
                case StatType.HP:
                    myStat[i].text = Gamemanager.instance.myUIManager.equipmentManager.equipmentHP.ToString();
                    break;
                case StatType.AP:
                    myStat[i].text = Gamemanager.instance.myUIManager.equipmentManager.equipmentAP.ToString();
                    break;
                case StatType.DP:
                    myStat[i].text = Gamemanager.instance.myUIManager.equipmentManager.equipmentDP.ToString();
                    break;
                case StatType.Speed:
                    myStat[i].text = Gamemanager.instance.myUIManager.equipmentManager.equipmentSpeed.ToString();
                    break;
                case StatType.AS:
                    myStat[i].text = Gamemanager.instance.myUIManager.equipmentManager.equipmentAS.ToString();
                    break;
            }
        }
    }

}
public enum StatType
{
    HP,AP,DP,Speed,AS
}
