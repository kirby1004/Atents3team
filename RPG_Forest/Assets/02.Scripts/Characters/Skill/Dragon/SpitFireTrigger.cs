using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpitFireTrigger : MonoBehaviour
{

    public LayerMask enemyMask;
    ParticleSystem ps = null;
    bool isHIt = false;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            other.GetComponent<IBattle>()?.OnDamage(10);
        }
        
    }
    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> list = new List<ParticleSystem.Particle>();
        int count = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, list, out var data);
        for(int i = 0; i < count; ++i)
        {
            if(data.GetCollider(i, 0) == ps.trigger.GetCollider(0))
            {
                if (!isHIt)
                {
                    //플레이어랑 충돌
                    isHIt = true;
                }
            }
            else if(data.GetCollider(i, 0) == ps.trigger.GetCollider(1))
            {
                //그라운드랑 충돌
            }
        }
    }

}
