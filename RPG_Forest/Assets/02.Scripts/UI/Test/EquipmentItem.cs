using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentItem : MonoBehaviour , IItems
{
    public Component myState
    {
        get => this as Component;
    }

    // ������� ����� ����â ����
    private void Start()
    {
        transform.parent.GetComponent<Slot>().mySlotItems = transform;
        EquipmentManager.Inst.RefreshStat(); // ��� �Ŵ����� ���ݰ��� ����

    }
    //��������� ������Ʈ�ı�
    private void Update()
    {
        if (Application.isPlaying == false)
        {
            GameObject.Destroy(gameObject);
        }
    }
    //������� ������ ���� ����
    private void OnDestroy()
    {
        if (Application.isPlaying == true)
        {
            transform.parent.GetComponent<Slot>().mySlotItems = null;
            EquipmentManager.Inst.RefreshStat(); // ��� �Ŵ����� ���Ȱ��� ����
        }
    }
}
