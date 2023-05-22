using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class IgnoreMouseClick : MonoBehaviour ,IPointerEnterHandler , IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Gamemanager.instance.myPlayer.SetIsEnterUI(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Gamemanager.instance.myPlayer.SetIsEnterUI(false);
    }
}
