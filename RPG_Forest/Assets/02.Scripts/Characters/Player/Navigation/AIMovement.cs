using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : CharacterMovement
{
    protected void MoveByPath(Vector3[] pathList)
    {
        StopAllCoroutines();
        StartCoroutine(MovingByPath(pathList));
    }

    IEnumerator MovingByPath(Vector3[] pathList)
    {
        myAnim.SetFloat("Speed", 1.0f);
        int i = 1;
        while (i < pathList.Length)
        {
            bool done = false;
            MoveToPos(pathList[i], () => done = true);
            while (!done)
            {
                for(int n=i; n<pathList.Length; n++)
                {
                    Debug.DrawLine(n==i ? transform.position : pathList[n-1], pathList[n], Color.red);
                }
                yield return null;
            }
            ++i;
        }
        myAnim.SetFloat("Speed", 0.0f);
    }
}
