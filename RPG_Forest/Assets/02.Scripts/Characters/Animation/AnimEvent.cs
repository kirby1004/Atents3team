using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackFunc;
    public UnityEvent BiteFunc;
    public UnityEvent RightAttackFunc;
    public UnityEvent DeadFunc;

    public void OnAttack()
    {
        AttackFunc?.Invoke();
        BiteFunc?.Invoke();
        RightAttackFunc?.Invoke();
    }

    public void OnDead()
    {
        DeadFunc?.Invoke();
    }

    
}
