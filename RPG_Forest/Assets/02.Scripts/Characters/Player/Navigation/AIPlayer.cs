using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : AIMovement
{
    public void OnMove(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            MoveByPath(path.corners);
        }
    }
    
    public void Attack()
    {
        myAnim.SetTrigger("Attack");
    }
}

