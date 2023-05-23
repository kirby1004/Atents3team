using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimEvent : MonoBehaviour
{
    public UnityEvent AttackFunc;
    public UnityEvent AttackEnterFunc;
    public UnityEvent AttackExitFunc;
    public UnityEvent SkillFunc;
    public UnityEvent SkillFunc2;
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
        SkillFunc?.Invoke();
    }

   public void SkillOn2()
    {
        SkillFunc2?.Invoke();
    }
}
