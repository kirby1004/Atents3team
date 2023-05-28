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
    public string Player;
}

public class ScriptManager : Singleton<ScriptManager>
{
    // 텍스트 출력창
    public ScriptWindow myWindow;

    // 현재 열린 문서 내에 텍스트 인덱스
    public int index = 0;
    // 열린 문서의 내용을 받아올 클래스
    public MySctipts myScripts;

    // 텍스트 진행이 끝낫는지 판단
    public bool isScriptEnd = true;

    private void Awake()
    {
        base.Initialize();

    }


    // Start is called before the first frame update
    void Start()
    {
        RefreshScript("json");
        myWindow.gameObject.SetActive(false);
        //for(int i = 0; i< myScripts.TextScript.Count; i++)
        //{
        //    Debug.Log($"{myScripts.TextScript[i].id} , {myScripts.TextScript[i].Text}");
        //}
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (isScriptEnd)
        //    {
        //        CurIndexAdd(index);
        //        index++;
        //    }
        //}
    }

    public void CurIndexAdd(int idx)
    {
        myWindow.ScriptAdd((ScriptWindow.IdType)myScripts.TextScript[idx].id,
            myScripts.TextScript[idx].Text, myScripts.TextScript[idx].Player);
    }

    public void RefreshScript(string text)
    {
        isScriptEnd = false;
        TextAsset textAsset = Resources.Load<TextAsset>($"JSon/{text}");

        myScripts = JsonUtility.FromJson<MySctipts>(textAsset.ToString());
        
        index = 0;
        isScriptEnd = true;
    }

}
