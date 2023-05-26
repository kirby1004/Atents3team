using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySctipts
{
    public List<TextScript> TextScript;
}

[Serializable]
public class TextScript
{
    public int id;
    public string Text;
}

public class ScriptManager : MonoBehaviour
{
    public ScriptWindow myWindow;    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
