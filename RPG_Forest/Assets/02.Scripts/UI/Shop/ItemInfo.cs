using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

// �������� ���콺 ���� �� �������� ���� ���
public class ItemInfo : MonoBehaviour
{
    // ���콺 ���� �� �������� �޾ƿ� ������Ʈ
    public ItemStatus myItem;

    // �������� �̹��� , �̸� , ���� �� ��� �� ������Ʈ
    public Image myImage;
    public TMP_Text ItemName;
    public TMP_Text Cost;
    // ������ ��Ƶ� ���
    public int myCost;

    // �ɼ��� ���� ex ) AP , DP , HP ���� ���ڵ�
    // �ɼ��� ���� , �θ� , ����  �߿�!(�� �ƴ�)
    public List<TMP_Text> Options = new List<TMP_Text>();
    public Transform Option;
    public GameObject OptionPrefab;
    // �ɼ��� ���� ����� ������Ʈ ���ϻ��� MakeOption , �������� MakeOptions
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
    // ��� �ɼ��� ���� ������Ʈ ����
    public void RemoveAllOptions(int count)
    {
        for(int i = 0;i < count; i++)
        {
            Destroy(Options[Options.Count-1].gameObject);
            Options.RemoveAt(Options.Count-1);
        }
    }
    // �ɼ��� ���� �߰�
    public void InsertOption(ItemStatus myItem)
    {
        switch (myItem.MyItemType) 
        {
            case ItemType.Weapon:
                Options[0].text = "AP";
                //Options[1].text = "AS"; ���� �߰��� Ȱ��ȭ
                break;
            case ItemType.Armor:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case ItemType.Leggins:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case ItemType.Headgear:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case ItemType.Boots:
                Options[0].text = "HP";
                Options[1].text = "DP";
                Options[2].text = "Speed";
                break;
            case ItemType.Soul:
                Options[0].text = "HP";
                break;
            default: 
                break;

        }
    }

    // �ɼ��� ��ġ 
    // �ɼ��� ���� , �θ� , ���� 
    public List<TMP_Text> OptionNums = new List<TMP_Text>();
    public Transform OptionNum;
    public GameObject OptionNumPrefab;
    // �ɼ��� ��ġ ����� ������Ʈ ���ϻ��� MakeOptionNum , �������� MakeOptionNums
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
    // ��� �ɼ��� ��ġ ������Ʈ ���� ���ڷ� OptionNums.Count �� ���°� ��õ
    public void RemoveAllOptionNums(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Destroy(OptionNums[OptionNums.Count - 1].gameObject);
            OptionNums.RemoveAt(OptionNums.Count - 1);
        }
    }
    // �ɼ��� ��ġ ��Ʈ�� ��ȯ�� ���
    public void InsertOptionNums(ItemStatus item)
    {
        switch(item.MyItemType) 
        {
            case ItemType.Weapon:
                OptionNums[0].text = myItem.AttackPoint.ToString();
                //OptionNums[1].text = myItem.AttackSpeed.ToString(); ���� �߰��� Ȱ��ȭ
                break;
            case ItemType.Armor:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Leggins:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Headgear:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Boots:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                OptionNums[2].text = myItem.MoveSpeed.ToString();
                break;
            case ItemType.Soul:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                break;
            default:
                break;
        }
    }

    // ������ ���� ���� �ڷ�ƾ => �ڷ�ƾ�Ⱦ��� �ٷΰ����� �ȵ�
    IEnumerator RefreshingItemInfo(ItemStatus item)
    {
        if (item != null)
        {
            myItem = item;
            RemoveAllOptionNums(OptionNums.Count);
            RemoveAllOptions(Options.Count);
            switch (item.MyItemType)
            {
                case ItemType.Weapon:
                    MakeOptions(1);
                    MakeOptionNums(1);
                    break;
                case ItemType.Armor:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case ItemType.Leggins:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case ItemType.Headgear:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case ItemType.Boots:
                    MakeOptions(3);
                    MakeOptionNums(3);
                    break;
                case ItemType.Soul:
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

    // �������� �ڷ��� ����
    public void RefreshItemInfo(ItemStatus item)
    {
        StartCoroutine(RefreshingItemInfo(item));
    }

    
    // Update is called once per frame
    //void Update()
    //{
        
    //}

}
