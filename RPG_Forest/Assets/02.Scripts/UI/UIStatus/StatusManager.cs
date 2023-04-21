using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public TMP_Text[] myStat;

    public void RefreshStatus()
    {
        
    }


}
public enum StatType
{
    HP,AP,DP,Speed,Range
}