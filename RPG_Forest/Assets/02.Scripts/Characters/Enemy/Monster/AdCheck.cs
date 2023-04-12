using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;

public interface I_Ad
{
    void Rangeout(Transform target);

}

public class AdCheck : MonoBehaviour
{
    I_Ad myParent = null;


    public Transform myRecallTarget = null;
    public LayerMask Spawnarea;
    public LayerMask RecallArea;

    // Start is called before the first frame update
    void Start()
    {
        myParent = transform.parent.GetComponent<I_Ad>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //���������� ������ ���ڸ��� ���߰��ϱ�
    private void OnTriggerEnter(Collider other)
    {
        if ((Spawnarea & 1 << other.gameObject.layer) != 0)
        {
            if (myRecallTarget != null)
            {
                myRecallTarget = transform.parent; //���ͻ��� ��������
                myParent.Rangeout(myRecallTarget);
                myRecallTarget = null;
            }
        }
    }

    //���� Ȱ�������� ������� ���ͽ�Ű��
    private void OnTriggerExit(Collider other)
    {
        if ((RecallArea & 1 << other.gameObject.layer) != 0) 
        {
            if (myRecallTarget == null) 
            {
                myRecallTarget = other.transform; //�������� ��ǥ�� �޾ƿ���
                myParent.Rangeout(myRecallTarget);//������ǥ�� �����ϱ�
            }
        }
    }
}
