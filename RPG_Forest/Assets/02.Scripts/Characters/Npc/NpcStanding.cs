using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStanding : NpcMovement
{
    #region ���ĵ� NPC ��� ����
   
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

    #region ���� ��ȯ �Լ�
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

    #region �ִϸ��̼� ���� �Լ�
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

    #region �������ִ� �Լ�
    float delaySeconds()
    {
        return UnityEngine.Random.Range(8.0f, 30.0f);
    }
    #endregion

    #region ������� �Լ�
    protected override void Disappear()
    {
        Avartar.SetActive(false);
        StartCoroutine(Appearing());
    }
    #endregion

    #region �ٽ� ��Ÿ���� �Լ�
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
