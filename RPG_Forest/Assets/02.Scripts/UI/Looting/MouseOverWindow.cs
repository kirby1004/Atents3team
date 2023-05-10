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



    }



    public void InsertOption(ItemStatus myItem)
    {
        switch (myItem.MyItemType)
        {
            case ItemType.Weapon:
                myOptionType[0].text = "AP";
                myOptionType[1].text = "AS";
                break;
            case ItemType.Armor:
                myOptionType[0].text = "HP";
                myOptionType[1].text = "DP";
                break;
            case ItemType.Leggins:
                myOptionType[0].text = "HP";
                myOptionType[1].text = "DP";
                break;
            case ItemType.Headgear:
                myOptionType[0].text = "HP";
                myOptionType[1].text = "DP";
                break;
            case ItemType.Boots:
                myOptionType[0].text = "HP";
                myOptionType[1].text = "DP";
                myOptionType[2].text = "Speed";
                break;
            case ItemType.Soul:
                myOptionType[0].text = "HP";
                break;
            default:
                break;
        }
    }

}
