using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentItem : MonoBehaviour
{
    
    // 컴포넌트의 생성 및 삭제시 스텟창 갱신
    private void Start()
    {
        Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
    }
    private void Update()
    {
        if(Application.isPlaying == false)
        {
            GameObject.Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if(Application.isPlaying == true)
        {
            Gamemanager.instance.myUIManager.equipmentManager.RefreshStat();
        }
    }



}
