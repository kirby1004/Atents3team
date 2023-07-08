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
    private void Start()
    {
        DataSaverManager.Inst.LoadEquipment();
        RefreshStat(0);
    }

    // ���� ���� �ڷ�ƾ �۵�
    public void RefreshStat(float A)
    {
        StartCoroutine(RefreshStsus(A));
    }

    //���� ���� �ڷ�ƾ
    IEnumerator RefreshStsus(float A)
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

        GameObject newItemGo = Instantiate(equipmentItemPrefab, equipslot[index].transform);    //������ ������Ʈ ���� �޾ƿͼ� ������ �ڽ����� ����
        newItemGo.GetComponent<Item>().InitialiseItem(items);      //������ ������Ʈ�� InventoryItem �Ӽ� �߰� �� item���� ��������
        newItemGo.AddComponent<EquipmentItem>();

    }

}
