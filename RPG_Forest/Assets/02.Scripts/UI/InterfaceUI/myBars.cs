using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class myBars : MonoBehaviour
{
    public Slider Bars;
    public UnityEvent MaxAlarm;
    // Start is called before the first frame update
    void Awake()
    {
        Bars = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Bars.value == Bars.maxValue)
        {
            MaxValNotice();
        }
    }
    public void UpdateState(float Value)
    {
        Bars.value = Value;
    }
    public void RefreshMax(float newMax)
    {
        Bars.maxValue = newMax;
    }
    public void MaxValNotice()
    {
        MaxAlarm?.Invoke();
    }
}
