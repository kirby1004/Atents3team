using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterAttack : CharacterProperty 
{
    public float RagePoint = 0.0f;
    public int randPattern = 0;
    public float myBasicDamage = 0.0f;
    public enum Pattern
    {
         None ,Attack1, Attack2, Attack3, Skill , Dodge
    }
    Pattern myPattern = Pattern.None;
    // Start is called before the first frame update
    void Start()
    {
        myBasicDamage = AttackPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(RagePoint < 0.0f) RagePoint = 0.0f;
        //randPattern = Random.Range(1, 5);
        myPattern = (Pattern)Random.Range(1, 4);
    }
    public void Attacking()
    {

    }

    private void AttackPattern(Pattern myPattern)
    {
        switch(myPattern)
        {
            case Pattern.None:
                break;
            case Pattern.Attack1:
                break;
            case Pattern.Attack2:
                break;
            case Pattern.Attack3:
                
                break;
            case Pattern.Skill:
                break;
            case Pattern.Dodge:
                break;
            default:
                break;
        }
    }
}
