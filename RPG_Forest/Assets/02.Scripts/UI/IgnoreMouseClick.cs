using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class IgnoreMouseClick : MonoBehaviour ,IPointerEnterHandler , IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Inst.myPlayer.SetIsEnterUI(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Inst.myPlayer.SetIsEnterUI(false);
    }
}
