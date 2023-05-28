using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class IgnoreMouseClick : MonoBehaviour ,IPointerEnterHandler , IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Gamemanager.Inst.myPlayer.SetIsEnterUI(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Gamemanager.Inst.myPlayer.SetIsEnterUI(false);
    }
}
