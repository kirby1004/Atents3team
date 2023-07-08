using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public Button myButton;

    public Transform myTarget;
    // Start is called before the first frame update
    void Awake()
    {
        myButton = transform.GetComponent<Button>();

        myButton.onClick.AddListener(() => DataSaverManager.Inst.SaveAllData(true));
        myButton.onClick.AddListener(() => myTarget.GetComponent<DataSaverManager>().SaveAllData(true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
