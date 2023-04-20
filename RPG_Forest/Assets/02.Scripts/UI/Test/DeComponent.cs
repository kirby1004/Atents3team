using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeComponent : MonoBehaviour
{
    public Button myButton;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DelComponent()
    {
        Destroy(myButton);
    }

    public void SetComponent()
    {
        gameObject.AddComponent<Text>();
    }
}
