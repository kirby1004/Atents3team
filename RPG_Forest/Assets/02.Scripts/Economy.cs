using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEconomy
{

    public int Money { get; set; }
    

    public void GetMoney(int money);
    public bool CheckMoney(int money);
}


public class Economy : MonoBehaviour
{
    

}
