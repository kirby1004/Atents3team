using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusManager : Singleton<StatusManager>
{
    public TMP_Text[] myStat;

    public TMP_Text myLevel;
    public TMP_Text myEnchantDamage;

    private void Awake()
    {
        base.Initialize();
    }
    public void RefreshingStat()
    {
        for (int i = 0; i < myStat.Length; i++)
        {
            switch ((StatType)i)
            {
                case StatType.HP:
                    myStat[i].text = Gamemanager.Inst.myPlayer.myBaseStatus.MaxHp.ToString()+ $"(+{EquipmentManager.Inst.equipmentHP})";
                    break;
                case StatType.AP:
                    myStat[i].text = Gamemanager.Inst.myPlayer.myBaseStatus.AttackPoint.ToString()+ $"(+{EquipmentManager.Inst.equipmentAP})";
                    break;
                case StatType.DP:
                    myStat[i].text = Gamemanager.Inst.myPlayer.myBaseStatus.DefensePoint.ToString() + $"(+{EquipmentManager.Inst.equipmentDP})";
                    break;
                case StatType.Speed:
                    myStat[i].text = (Gamemanager.Inst.myPlayer.myBaseStatus.MoveSpeed /3 ).ToString("F1") + $"(+{(EquipmentManager.Inst.equipmentSpeed/3).ToString("F1")})";
                    break;
                //case StatType.AS:
                //    myStat[i].text = Gamemanager.Inst.myPlayer.AttackDelay.ToString() + $"(+{EquipmentManager.Inst.equipmentAS.ToString()})";
                //    break;
            }
        }
    }
}
public enum StatType
{
    HP, AP, DP, Speed, AS
}
