using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class EnchantManager : Singleton<EnchantManager>
{
    // ��ȭâ , ���� ���� â
    public EnchantWIndow myWindow;
    public EnchantSuccess mySuccessWindow;
    public EnchantFail myFailWindow;

    // ��ȭ �ڽ�Ʈ �� Ȯ�����̺�
    public EnchantCostTable myCostTable;

    public TMP_Text[] SoulLevel;
    public TMP_Text SuccessRate;
    public TMP_Text EnchantCost;

    public UnityEvent<int> UpdateLevel;
    private int _enchantLevel;
    public int EnchantLevel
    {
        get
        {
            return _enchantLevel;
        }
        set
        {
            _enchantLevel = value;
            UpdateLevel?.Invoke(value);
        }
    }

    private void Awake()
    {
        base.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateLevel.AddListener(UpdateLevelTask);
        UpdateLevelTask(EnchantLevel);
    }

 
    // ��ȭ ������ ���� �ִ��� üũ
    public bool EnchantPossibleCheck()
    {
        if (GameManager.Inst.CheckMoney(myCostTable.CostTable[EnchantLevel]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // ��ȭ Ȯ���� ����޴��� üũ
    public bool EnchantSuccessCheck()
    {
        if (GameManager.Inst.ProbabilityChoose(myCostTable.SuccessRate[EnchantLevel]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void UpdateLevelTask(int value)
    {
        UIManager.instance.myLevel.text = value.ToString();
        //StatusManager.Inst.myLevel.text = "Lv." + value.ToString();
        StatusManager.Inst.myEnchantDamage.text = value.ToString();
        myWindow.RefreshEnchentCost(myCostTable.CostTable[value]);
        myWindow.RefreshSoulLevel(value);
        myWindow.RefreshSuccessRate(myCostTable.SuccessRate[value]);
    }

}
