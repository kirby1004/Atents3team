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
        //myButton = transform.GetComponent<Button>();
    }

    private void Start()
    {

        myButton.onClick.AddListener(() =>
        {
            DataSaverManager.Inst.SaveAllData(true);
            myTarget.gameObject.SetActive(false);
        });
        
    }
    // Update is called once per frame
    void Update()
    {
        //if(GetComponent<Button>().onClick == null)
        //{
        //    GetComponent<Button>().onClick.AddListener(() => DataSaverManager.Inst.SaveAllData(true));
        //}


    }

    public void ExitGames()
    {

    }

}
