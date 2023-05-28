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
    // �ؽ�Ʈ ���â
    public ScriptWindow myWindow;

    // ���� ���� ���� ���� �ؽ�Ʈ �ε���
    public int index = 0;
    // ���� ������ ������ �޾ƿ� Ŭ����
    public MySctipts myScripts;

    // �ؽ�Ʈ ������ �������� �Ǵ�
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
