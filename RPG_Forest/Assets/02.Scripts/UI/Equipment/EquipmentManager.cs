using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : Singleton<EquipmentManager>
{
    // ���â ����
    public List<Slot> equipslot = new();
    // ��� ���� , ������ ����
    public GameObject equipmentSlotPrefab;
    public GameObject equipmentItemPrefab;

    // ������ ����� ���� �� ���
    public float equipmentHP;
    public float equipmentAP;
    public float equipmentDP;
    public float equipmentAS;
    public float equipmentSpeed;

    private void Awake()
    {
        base.Initialize();
    }

    // ���� ���� �ڷ�ƾ �۵�
    public void RefreshStat()
    {
        StartCoroutine(RefreshStsus());
    }

    //���� ���� �ڷ�ƾ
    IEnumerator RefreshStsus()
    {
        yield return new WaitForEndOfFrame();
        //���� �ʱ�ȭ �� �ջ�
        equipmentHP = equipmentAP = equipmentAS = equipmentSpeed = equipmentDP = 0; // ��罺�� 0���� �ʱ�ȭ
        for (int i = 0; i < equipslot.Count; i++)
        {
            // �������� ����� ���� �ջ�
            if (equipslot[i].GetComponentInChildren<Item>() != null)
            {
                equipmentHP += equipslot[i].GetComponentInChildren<Item>().item.MaxHpIncrese;  // ü��
                equipmentAP += equipslot[i].GetComponentInChildren<Item>().item.AttackPoint;   //���ݷ�
                equipmentDP += equipslot[i].GetComponentInChildren<Item>().item.DefensePoint;  //����
                //equipmentAS += equipslot[i].GetComponentInChildren<Item>().item.AttackSpeed;   //���ݼӵ� Ȱ��ȭ�� �߰�
                equipmentSpeed += equipslot[i].GetComponentInChildren<Item>().item.MoveSpeed;  //�̵��ӵ�
            }
        }
        Debug.Log($" HP : {equipmentHP} , AP : {equipmentAP} , DP : {equipmentDP} , AS : {equipmentAS} , Speed : {equipmentSpeed} ");

    }
}
