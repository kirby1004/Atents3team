using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class IgnoreMouseClick : MonoBehaviour ,IPointerEnterHandler , IPointerExitHandler , IPointerMoveHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Gamemanager.Inst != null)
        {
            if (Gamemanager.Inst.myPlayer != null)
            {
                Gamemanager.Inst.myPlayer.SetIsEnterUI(true);
            }
        }
    }

    //public void onpointet

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Gamemanager.Inst != null)
        {
            if (Gamemanager.Inst.myPlayer != null)
            {
                Gamemanager.Inst.myPlayer.SetIsEnterUI(false);
            }
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (Gamemanager.Inst != null)
        {
            if (Gamemanager.Inst.myPlayer != null)
            {
                Gamemanager.Inst.myPlayer.SetIsEnterUI(true);
            }
        }
    }

    private void OnDestroy()
    {
        if(Gamemanager.Inst != null)
        {
            if(Gamemanager.Inst.myPlayer != null)
            {
                Gamemanager.Inst.myPlayer.SetIsEnterUI(false);
            }
        }
    }
    private void OnDisable()
    {
        if (Gamemanager.Inst != null)
        {
            if (Gamemanager.Inst.myPlayer != null)
            {
                Gamemanager.Inst.myPlayer.SetIsEnterUI(false);
            }
        }
    }
}
