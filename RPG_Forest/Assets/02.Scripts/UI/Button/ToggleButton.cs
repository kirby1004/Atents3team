using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour 
{
    public Button mybutton;
    // Start is called before the first frame update
    void Start()
    {
        mybutton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(mybutton.enabled)
        //{
        //    if (gameObject.activeSelf)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        gameObject.SetActive(true);
        //    }
        //}
    }

    public void OnbuttonClick(Button button)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
