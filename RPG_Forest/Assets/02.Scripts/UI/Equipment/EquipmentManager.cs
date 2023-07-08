using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : Singleton<EquipmentManager>
{
    // 장비창 슬롯
    public List<Slot> equipslot = new();
    // 장비 슬롯 , 아이템 원본
    public GameObject equipmentSlotPrefab;
    public GameObject equipmentItemPrefab;

    // 장착한 장비의 스텟 합 상수
    public float equipmentHP;
    public float equipmentAP;
    public float equipmentDP;
    public float equipmentAS;
    public float equipmentSpeed;

    private void Awake()
    {
        base.Initialize();
    }
    private void Start()
    {
        DataSaverManager.Inst.LoadEquipment();
        RefreshStat(0);
    }

    // 스텟 갱신 코루틴 작동
    public void RefreshStat(float A)
    {
        StartCoroutine(RefreshStsus(A));
    }

    //스텟 갱신 코루틴
    IEnumerator RefreshStsus(float A)
    {
        yield return new WaitForEndOfFrame();
        //스텟 초기화 후 합산
        equipmentHP = equipmentAP = equipmentAS = equipmentSpeed = equipmentDP = 0; // 모든스텟 0으로 초기화
        for (int i = 0; i < equipslot.Count; i++)
        {
            // 장착중인 장비의 스텟 합산
            if (equipslot[i].GetComponentInChildren<Item>() != null)
            {
                equipmentHP += equipslot[i].GetComponentInChildren<Item>().item.MaxHpIncrese;  // 체력
                equipmentAP += equipslot[i].GetComponentInChildren<Item>().item.AttackPoint;   //공격력
                equipmentDP += equipslot[i].GetComponentInChildren<Item>().item.DefensePoint;  //방어력
                //equipmentAS += equipslot[i].GetComponentInChildren<Item>().item.AttackSpeed;   //공격속도 활성화시 추가
                equipmentSpeed += equipslot[i].GetComponentInChildren<Item>().item.MoveSpeed;  //이동속도
            }
        }
        Gamemanager.Inst.myPlayer.curHp += A;
        //Gamemanager.Inst.myPlayer.curHp = (Gamemanager.Inst.myPlayer.MaxHp * Rate);
        Debug.Log($"{Gamemanager.Inst.myPlayer.curHp}");
        UIManager.instance.hpBar.RefreshHPBar(Gamemanager.Inst.myPlayer.curHp);
        //Gamemanager.Inst.myPlayer.curHp = Gamemanager.Inst.myPlayer.
        //Debug.Log($" HP : {equipmentHP} , AP : {equipmentAP} , DP : {equipmentDP} , AS : {equipmentAS} , Speed : {equipmentSpeed} ");
    }

    public void CreateEquipmentItems(ItemStatus items , int index)
    {
        //equipslot[index]

        GameObject newItemGo = Instantiate(equipmentItemPrefab, equipslot[index].transform);    //아이템 오브젝트 원본 받아와서 슬롯의 자식으로 생성
        newItemGo.GetComponent<Item>().InitialiseItem(items);      //생성한 오브젝트에 InventoryItem 속성 추가 후 item으로 정보주입
        newItemGo.AddComponent<EquipmentItem>();

    }

}
