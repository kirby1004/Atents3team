using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScriptWindow : MonoBehaviour
{
    public float ShowDelay = 0.05f;
    public enum IdType
    {
        normal,Start,End,NewLoad
    }
    public UnityAction DoneAction;

    public TMP_Text myText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScriptAdd(IdType id , string text)
    {
        switch (id)
        {
            case IdType.normal:
                StartCoroutine(ShowScript(text));
                break; 
            case IdType.Start:
                DoneAction = null;
                break;
            case IdType.End:
                StartCoroutine(ShowScript(text));
                DoneAction += () => ScriptManager.Inst.isScriptEnd = true;
                break; 
            case IdType.NewLoad:
                ScriptManager.Inst.RefreshScript(text);
                break;
        }

        

    }

    IEnumerator ShowScript(string text)
    {
        StringBuilder sb = new StringBuilder();
        WaitForSeconds ws = new WaitForSeconds(ShowDelay);
        sb.Clear();
        int index = 0;
        while (sb.Length<text.Length)
        {
            sb.Append(text[index]);
            myText.text = sb.ToString();
            index++;
            yield return ws;
        }
        DoneAction?.Invoke();
    }

}
