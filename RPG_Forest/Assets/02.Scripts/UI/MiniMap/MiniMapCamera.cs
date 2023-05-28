using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapCamera : MonoBehaviour
{
    public Camera myCamera;

    public Transform myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = Gamemanager.Inst.myPlayer.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if( myPlayer == null)
        {
            myPlayer = Gamemanager.Inst.myPlayer.transform;
            transform.SetParent(myPlayer);
            transform.localPosition = Vector3.up * 50.0f;
        }
        
    }


}
