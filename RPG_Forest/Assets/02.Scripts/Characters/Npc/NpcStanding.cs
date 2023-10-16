using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStanding : NpcMovement
{
    #region 스탠딩 NPC 멤버 변수
   
    Vector3 spawnPoint;
    Quaternion Rotate;
    public GameObject Avartar;
    #endregion

    private void Start()
    {
       ChangeState(NpcState.Standing);
        spawnPoint = transform.position;
        Rotate=transform.rotation;
    }

    #region 상태 변환 함수
    override protected void ChangeState(NpcState ns)
    {
        if (myState == ns) return;
        myState = ns;
        switch (myState)
        { 
            case NpcState.Standing:
                StopAllCoroutines();
                StartCoroutine(Active());
                break;
            case NpcState.Run:
                StopAllCoroutines();
                Fear(Target);
                break;
            case NpcState.Disappear:
                StopAllCoroutines();
                Disappear();
                break;
        }
    }
    #endregion

    #region 애니메이션 실행 함수
    IEnumerator Active()
    {
        yield return new WaitForEndOfFrame();

        while (true)
        {
            yield return new WaitForSeconds(delaySeconds());
            myAnimator.SetTrigger("Active");
        }
    }
    #endregion

    #region 딜레이주는 함수
    float delaySeconds()
    {
        return UnityEngine.Random.Range(8.0f, 30.0f);
    }
    #endregion

    #region 사라지는 함수
    protected override void Disappear()
    {
        Avartar.SetActive(false);
        StartCoroutine(Appearing());
    }
    #endregion

    #region 다시 나타나는 함수
    IEnumerator Appearing()
    {
        yield return new WaitForSeconds(10.0f);

        while (true)
        {
            transform.position = spawnPoint;
            transform.rotation = Rotate;

            myCollider.enabled = true;
            ChangeState(NpcState.Standing);
            Avartar.SetActive(true);
            break;
        }
    }
    #endregion
}
