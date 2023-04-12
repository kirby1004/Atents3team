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


    //스폰지역에 들어오면 제자리에 멈추게하기
    private void OnTriggerEnter(Collider other)
    {
        if ((Spawnarea & 1 << other.gameObject.layer) != 0)
        {
            if (myRecallTarget != null)
            {
                myRecallTarget = transform.parent; //복귀상태 해제조건
                myParent.Rangeout(myRecallTarget);
                myRecallTarget = null;
            }
        }
    }

    //몬스터 활동지역을 벗어났을때 복귀시키기
    private void OnTriggerExit(Collider other)
    {
        if ((RecallArea & 1 << other.gameObject.layer) != 0) 
        {
            if (myRecallTarget == null) 
            {
                myRecallTarget = other.transform; //스포너의 좌표를 받아오고
                myParent.Rangeout(myRecallTarget);//복귀좌표로 설정하기
            }
        }
    }
}
