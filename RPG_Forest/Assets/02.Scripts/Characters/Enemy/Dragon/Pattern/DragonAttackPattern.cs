using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPhase
{
    protected Dragon dragon;

    public AttackPhase(Dragon dragon)
    {
        this.dragon = dragon;
    }

    // Rotation �ڷ�ƾ�� ����ٰ� �߰��ؼ� �������̵� �ϴ� �������� �����غ��� 

    public abstract IEnumerator DoPhase();
}

public class DragonAttackPattern
{
    private Dragon dragon;
    private List<AttackPhase> phases = new List<AttackPhase>();

    public DragonAttackPattern(Dragon dragon)
    {
        this.dragon = dragon;
        InitializePhases();
    }   

    private void InitializePhases()
    {
        phases.Add(new DragonState_Bite(dragon));
        phases.Add(new DragonState_LeftClawAttack(dragon));
        phases.Add(new DragonState_RightClawAttack(dragon));
    }

    public IEnumerator DoAttackPattern()
    {
        float delayTime = 3.0f;
        var wfs = new WaitForSeconds(delayTime);

        while(true)
        {
            foreach (AttackPhase phase in phases)
            {
                if (Mathf.Approximately(dragon.curHp, 0)) break;

                yield return dragon.StartCoroutine(phase.DoPhase());
                yield return wfs;                                        // Delay Between Phases
            }
            yield return null;
        }
        
    }

}
