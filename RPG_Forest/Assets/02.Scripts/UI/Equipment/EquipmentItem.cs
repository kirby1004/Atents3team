using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentItem : MonoBehaviour
{
    
    // 착용상태 적용시 스텟창 갱신
    private void Start()
    {
        Gamemanager.instance.myUIManager.equipmentManager.RefreshStat(); // 장비 매니저의 스텟갱신 동작
    }
    //게임종료시 오브젝트파괴
    private void Update()
    {
        if(Application.isPlaying == false)
        {
            GameObject.Destroy(gameObject);
        }
    }
    //착용상태 해제시 스텟 갱신
    private void OnDestroy()
    {
        if(Application.isPlaying == true)
        {
            Gamemanager.instance.myUIManager.equipmentManager.RefreshStat(); // 장비 매니저의 스탯갱신 동작
        }
    }



}
