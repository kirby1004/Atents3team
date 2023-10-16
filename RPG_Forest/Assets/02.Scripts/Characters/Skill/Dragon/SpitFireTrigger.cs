using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpitFireTrigger : MonoBehaviour
{

    public LayerMask enemyMask;
    public SpitFire spitFire;

   

    private void OnParticleCollision(GameObject other)
    {
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            other.GetComponent<IBattle>()?.OnDamage(spitFire.skillData.Value1);
        }
    }

   

}
