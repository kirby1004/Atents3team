using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStanding : NpcMovement
{
    Vector3 spawnPoint;
    Quaternion Rotate;
    public GameObject Avartar;

    private void Start()
    {
       ChangeState(NpcState.Standing);
        spawnPoint = transform.position;
        Rotate=transform.rotation;
    }
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

    IEnumerator Active()
    {
        yield return new WaitForEndOfFrame();

        while (true)
        {
            yield return new WaitForSeconds(delaySeconds());
            myAnimator.SetTrigger("Active");
        }
    }

    float delaySeconds()
    {
        return UnityEngine.Random.Range(8.0f, 30.0f);
    }

    protected override void Disappear()
    {
        Avartar.SetActive(false);
        StartCoroutine(Appearing());
    }

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
}
