using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour
{
    // ���â ����
    public List<EquipmentSlot> equipslot = new();
    // ��� ���� , ������ ����
    public GameObject equipmentSlotPrefab;
    public GameObject equipmentItemPrefab;

    // ������ ����� ���� �� ���
    public float equipmentHP;
    public float equipmentAP;
    public float equipmentDP;
    public float equipmentAS;
    public float equipmentSpeed;


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
        for(int i = 0;i < equipslot.Count;i++)
        {
            // �������� ����� ���� �ջ�
            if (equipslot[i].GetComponentInChildren<InventoryItem>() != null)
            {
                equipmentHP += equipslot[i].GetComponentInChildren<InventoryItem>().item.MaxHpIncrese;  // ü��
                equipmentAP += equipslot[i].GetComponentInChildren<InventoryItem>().item.AttackPoint;   //���ݷ�
                equipmentDP += equipslot[i].GetComponentInChildren<InventoryItem>().item.DefensePoint;  //����
                equipmentAS += equipslot[i].GetComponentInChildren<InventoryItem>().item.AttackSpeed;   //���ݼӵ�
                equipmentSpeed += equipslot[i].GetComponentInChildren<InventoryItem>().item.MoveSpeed;  //�̵��ӵ�
            }
        }
    }

}
