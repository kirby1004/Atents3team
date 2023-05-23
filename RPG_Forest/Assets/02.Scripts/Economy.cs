using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEconomy
{

    public int Money { get; set; }
    

    public void GetMoney(int money);
    public bool CheckMoney(int money);
}


public class Economy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
