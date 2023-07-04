using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public Button myButton;

    public Transform myTarget;
    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => DataSaverManager.Inst.SaveAllData(true));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
