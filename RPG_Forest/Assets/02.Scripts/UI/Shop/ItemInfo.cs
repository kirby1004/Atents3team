using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemInfo : MonoBehaviour
{
    public ItemStatus myItem;

    public Image myImage;
    public TMP_Text ItemName;
    public TMP_Text Cost;
    public int myCost;

    public List<TMP_Text> Options = new List<TMP_Text>();
    public Transform Option;
    public GameObject OptionPrefab;

    public void MakeOption()
    {
        GameObject Newoptions = Instantiate(OptionPrefab, Option);
        Options.Add(Newoptions.GetComponent<TMP_Text>());
    }
    public void MakeOptions(int count)
    {
        for (int i = 0; i < count; i++)
        {
            MakeOption();
        }
    }
    public void RemoveAllOptions(int count)
    {
        for(int i = 0;i < count; i++)
        {
            Destroy(Options[Options.Count-1].gameObject);
            Options.RemoveAt(Options.Count-1);
        }
    }
    public void InsertOption(ItemStatus myItem)
    {
        switch (myItem.MyEquipmentType) 
        {
            case EquipmentType.Weapon:
                Options[0].text = "AP";
                Options[1].text = "AS";
                break;
            case EquipmentType.Armor:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case EquipmentType.Leggins:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case EquipmentType.Headgear:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case EquipmentType.Boots:
                Options[0].text = "HP";
                Options[1].text = "DP";
                Options[2].text = "Speed";
                break;
            case EquipmentType.Soul:
                Options[0].text = "HP";
                break;
            default: 
                break;

        }

    }

    public List<TMP_Text> OptionNums = new List<TMP_Text>();
    public Transform OptionNum;
    public GameObject OptionNumPrefab;
    public void MakeOptionNum()
    {
        GameObject NewoptionNums = Instantiate(OptionNumPrefab, OptionNum);
        OptionNums.Add(NewoptionNums.GetComponent<TMP_Text>());
    }
    public void MakeOptionNums(int count)
    {
        for(int i = 0;i < count; i++)
        {
            MakeOptionNum();
        }
    }
    public void RemoveAllOptionNums(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Destroy(OptionNums[OptionNums.Count - 1].gameObject);
            OptionNums.RemoveAt(OptionNums.Count - 1);
        }
    }
    public void InsertOptionNums(ItemStatus item)
    {
        switch(item.MyEquipmentType) 
        {
            case EquipmentType.Weapon:
                OptionNums[0].text = myItem.AttackPoint.ToString();
                OptionNums[1].text = myItem.AttackSpeed.ToString();
                break;
            case EquipmentType.Armor:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case EquipmentType.Leggins:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case EquipmentType.Headgear:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case EquipmentType.Boots:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                OptionNums[2].text = myItem.MoveSpeed.ToString();
                break;
            case EquipmentType.Soul:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                break;
            default:
                break;
        }
    }
    IEnumerator RefreshingItemInfo(ItemStatus item)
    {
        if (item != null)
        {
            myItem = item;
            RemoveAllOptionNums(OptionNums.Count);
            RemoveAllOptions(Options.Count);
            switch (item.MyEquipmentType)
            {
                case EquipmentType.Weapon:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case EquipmentType.Armor:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case EquipmentType.Leggins:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case EquipmentType.Headgear:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case EquipmentType.Boots:
                    MakeOptions(3);
                    MakeOptionNums(3);
                    break;
                case EquipmentType.Soul:
                    MakeOptions(1);
                    MakeOptionNums(1);
                    break;
                default:
                    break;
            }
            InsertOption(item);
            InsertOptionNums(item);
            yield return new WaitForEndOfFrame();
            ItemName.text = item.ItemName;
            myImage.sprite = item.Image;
            Cost.text = myCost.ToString();
        }
    }

    public void RefreshItemInfo(ItemStatus item)
    {
        StartCoroutine(RefreshingItemInfo(item));
        //if( item != null)
        //{
        //    myItem = item;
        //    RemoveAllOptionNums(OptionNums.Count);
        //    RemoveAllOptions(Options.Count);
        //            switch (item.MyEquipmentType)
        //            {
        //                case EquipmentType.Weapon:
        //                    MakeOptions(2);
        //                    MakeOptionNums(2);
        //                    break;
        //                case EquipmentType.Armor:
        //                    MakeOptions(2);
        //                    MakeOptionNums(2);
        //                    break;
        //                case EquipmentType.Leggins:
        //                    MakeOptions(2);
        //                    MakeOptionNums(2);
        //                    break;
        //                case EquipmentType.Headgear:
        //                    MakeOptions(2);
        //                    MakeOptionNums(2);
        //                    break;
        //                case EquipmentType.Boots:
        //                    MakeOptions(3);
        //                    MakeOptionNums(3);
        //                    break;
        //                case EquipmentType.Soul:
        //                    MakeOptions(1);
        //                    MakeOptionNums(1);
        //                    break;
        //                default:
        //                    break;
        //            }
        //    InsertOption(item);
        //    InsertOptionNums(item);
        //    ItemName.text = item.ItemName;
        //    myImage.sprite = item.Image;
        //    Cost.text = myCost.ToString();
        //}
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

}
