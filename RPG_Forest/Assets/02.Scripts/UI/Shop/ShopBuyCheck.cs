using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShopBuyCheck : MonoBehaviour
{
    public ShopItem myItem;

    public Button BuyButton;
    public Button CancelButton;

    public TMP_Text myItemname;

    // Start is called before the first frame update
    void Start()
    {

        CancelButton.onClick.AddListener(() => {
            BuyButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshWindow()
    {
        BuyButton.onClick.AddListener(() =>
        {
            if (GameManager.instance.CheckMoney(myItem.myCost))
            {
                myItem.BuyItem(myItem.myItem, true);
            }
            else
            {

            }
        });
    }
}
