using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimEvent : MonoBehaviour
{
    public UnityEvent AttackFunc;
    public UnityEvent AttackEnterFunc;
    public UnityEvent AttackExitFunc;
    public UnityEvent QSkillFunc;
    // Start is called before the first frame update
    public void OnAttackEnter() //Attack�� ���۵� �� ����.
    {
        AttackEnterFunc?.Invoke();
    }
    public void OnAttack() //Ÿ�� ����
    {
        AttackFunc?.Invoke();
    }

    public void OnAttackExit() //Attack�� ���� �� ����.
    {
        AttackExitFunc?.Invoke();
    }

    public void SkillOn()
    {
        QSkillFunc?.Invoke();
    }
}
