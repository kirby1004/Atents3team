using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverWindow : MonoBehaviour 
{
    public ItemStatus myItem = null;
    public GameObject myObj = null;

    public Image myImage = null;

    public List<TMP_Text> myOptionType;
    public List<TMP_Text> myOptionNum;
    public Transform Optionparent;

    private void Start()
    {
        myImage.sprite = myItem.Image;
        InsertOption(myItem);
        

    }

    public void MakeOptionText()
    {
        GameObject obj = Instantiate(Resources.Load("UIResource/MouseOverWindow/OP1") as GameObject,Optionparent);
        myOptionType.Add(obj.GetComponent<TMP_Text>());
    }

    public void MakeOptionTexts(int count) 
    {
        for (int i = 0; i < count; i++)
        {
            MakeOptionText();
        }
    }

    public void InsertOption(ItemStatus myItem)
    {
        switch (myItem.MyItemType)
        {
            case ItemType.Weapon:
                MakeOptionTexts(2);
                myOptionType[0].text = "AP : " + myItem.AttackPoint.ToString();
                myOptionType[1].text = "AS : " + myItem.AttackSpeed.ToString();
                break;
            case ItemType.Armor:
                MakeOptionTexts(2);
                myOptionType[0].text = "HP : " + myItem.MaxHpIncrese.ToString();
                myOptionType[1].text = "DP : " + myItem.DefensePoint.ToString();
                break;
            case ItemType.Leggins:
                MakeOptionTexts(2);

                myOptionType[0].text = "HP : " + myItem.MaxHpIncrese.ToString();
                myOptionType[1].text = "DP : " + myItem.DefensePoint.ToString();
                break;
            case ItemType.Headgear:
                MakeOptionTexts(2);
                myOptionType[0].text = "HP : " + myItem.MaxHpIncrese.ToString();
                myOptionType[1].text = "DP : " + myItem.DefensePoint.ToString();
                break;
            case ItemType.Boots:
                MakeOptionTexts(3);
                myOptionType[0].text = "HP : " + myItem.MaxHpIncrese.ToString();
                myOptionType[1].text = "DP : " + myItem.DefensePoint.ToString();
                myOptionType[2].text = "Speed : " + myItem.MoveSpeed.ToString();
                break;
            case ItemType.Soul:
                //myOptionType[0].text = "HP";
                break;
            default:
                break;
        }
    }

}
