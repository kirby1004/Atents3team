using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour 
{
    public Button mybutton;

    public List<GameObject> mySystems;
    // Start is called before the first frame update
    void Start()
    {
        mybutton = GetComponent<Button>();
        mySystems.Add(Gamemanager.instance.myUIManager.inventoryManager.gameObject);
        mySystems.Add(Gamemanager.instance.myUIManager.equipmentManager.gameObject);
        mySystems.Add(Gamemanager.instance.myUIManager.statusManager.gameObject);
        //mySystems.Add(Gamemanager.instance.myUIManager.shopManager.gameObject);
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
    public void ToggleBUtton(Button button)
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void OnbuttonClick(int index)
    {
        if (mySystems[index].gameObject.activeSelf)
        {
            mySystems[index].gameObject.SetActive(false);
        }
        else
        {
            mySystems[index].gameObject.SetActive(true);
        }
    }

}
