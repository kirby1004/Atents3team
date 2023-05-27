using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MySctipts
{
    public List<TextScript> TextScript;
}

[System.Serializable]
public class TextScript
{
    public int id;
    public string Text;
}

public class ScriptManager : Singleton<ScriptManager>
{
    public ScriptWindow myWindow;

    //public MySctipts mySctipts = new();

    public int index = 0;

    public MySctipts myScripts;
    public bool isScriptEnd = false;

    private void Awake()
    {
        base.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JSon/Test1");

        myScripts = JsonUtility.FromJson<MySctipts>(textAsset.ToString());
        //foreach(TextScript script in mySctipts.TextScript)
        //{
        //    Debug.Log($"{script.id},{script.Text}");
        //}
        for(int i = 0; i< myScripts.TextScript.Count; i++)
        {
            Debug.Log($"{myScripts.TextScript[i].id} , {myScripts.TextScript[i].Text}");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isScriptEnd)
            {
                myWindow.ScriptAdd((ScriptWindow.IdType)myScripts.TextScript[index].id, myScripts.TextScript[index].Text);
                index++;
            }
        }
    }

    public void RefreshScript(string text)
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"JSon/{text}");

        myScripts = JsonUtility.FromJson<MySctipts>(textAsset.ToString());
        isScriptEnd = false;
        index = -1;
    }

}
