using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatText : MonoBehaviour
{
    public TMP_Text text;

    //public StatType type;

    private void Update()
    {
        Gamemanager.instance.myUIManager.statusManager.RefreshingStat();
    }
}

