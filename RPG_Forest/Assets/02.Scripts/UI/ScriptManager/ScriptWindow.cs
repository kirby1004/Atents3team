using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScriptWindow : MonoBehaviour , IPointerClickHandler
{
    public float ShowDelay = 0.05f;
    public enum IdType
    {
        normal,Start,End,NewLoad,PlayerChange
    }

    public UnityAction StartAction;
    public UnityAction DoneAction;

    public TMP_Text myName;
    public TMP_Text myText;

    // Start is called before the first frame update
    void Start()
    {
        DoneAction += () => ScriptManager.Inst.isScriptEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScriptAdd(IdType id , string text , string teller)
    {
       
        switch (id)
        {
            case IdType.normal:
                StartCoroutine(ShowScript(text));
                break; 
            case IdType.Start:
                //DoneAction = null;
                StartCoroutine(ShowScript(text));
                myName.text = teller;
                break;
            case IdType.End:
                //DoneAction += () => ScriptManager.Inst.isScriptEnd = true;
                StartCoroutine(ShowScript(text));
                ScriptManager.Inst.index = 0;
                this.gameObject.SetActive(false);
                break; 
            case IdType.NewLoad:
                ScriptManager.Inst.RefreshScript(text);
                ScriptManager.Inst.CurIndexAdd(ScriptManager.Inst.index);
                //ScriptManager.Inst.index++;
                break;
            case IdType.PlayerChange:
                myName.text = teller;
                StartCoroutine(ShowScript(text));
                break;
        }
    }
    StringBuilder sb = new StringBuilder();

    IEnumerator ShowScript(string text)
    {
        ScriptManager.Inst.isScriptEnd = false;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ScriptManager.Inst.isScriptEnd)
        {
            ScriptManager.Inst.CurIndexAdd(ScriptManager.Inst.index);
            ScriptManager.Inst.index++;
        }
    }
}
