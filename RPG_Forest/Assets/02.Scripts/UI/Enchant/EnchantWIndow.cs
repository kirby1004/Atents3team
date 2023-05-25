using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnchantWIndow : MonoBehaviour
{

    public TMP_Text[] SoulLevel;
    public TMP_Text SuccessRate;
    public TMP_Text EnchantCost;

    public Button EnchantButton;
    public UnityEvent<int> EnchentAdd;

    // Start is called before the first frame update
    void Start()
    {
        //EnchantButton.onClick.RemoveAllListeners();
        EnchantButton.onClick.AddListener((UnityAction)(() =>
        {
            if (EnchantManager.Inst.EnchantPossibleCheck())
            {
                if(EnchantManager.Inst.EnchantSuccessCheck())
                {
                    GameManager.Inst.GetMoney
                    (-EnchantManager.Inst.myCostTable.CostTable[EnchantManager.Inst.EnchantLevel]);
                    EnchantManager.Inst.EnchantLevel++;
                    EnchantManager.Inst.mySuccessWindow.gameObject.SetActive(true);
                    EnchantManager.Inst.mySuccessWindow.LevelText.text = "Lv." + EnchantManager.Inst.EnchantLevel.ToString();
                }
                else
                {
                    GameManager.Inst.GetMoney
                    (-EnchantManager.Inst.myCostTable.CostTable[EnchantManager.Inst.EnchantLevel]);
                    EnchantManager.Inst.myFailWindow.gameObject.SetActive(true);
                    EnchantManager.Inst.myFailWindow.isType = true;
                }
            }
            else
            {
                EnchantManager.Inst.myFailWindow.gameObject.SetActive(true);
                EnchantManager.Inst.myFailWindow.isType = false;
            }
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshSoulLevel(int level)
    {
        SoulLevel[0].text = level.ToString();
        SoulLevel[1].text = (level+1).ToString();
    }

    public void RefreshSuccessRate(float Rate)
    {
        SuccessRate.text = Rate.ToString()+"%";
    }

    public void RefreshEnchentCost(int  Cost)
    {
        EnchantCost.text = Cost.ToString()+" Soul";
    }


}
