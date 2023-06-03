using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingPerception : MonoBehaviour
{
    //public LayerMask enemyMask;
    IinterPlay myPlayer;
    Transform myTarget = null;
    public float LootRange = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider sphereCollider = transform.GetComponent<SphereCollider>();
        sphereCollider.radius = LootRange;

        Collider[] list = Physics.OverlapSphere(transform.parent.position, LootRange); 

        foreach (Collider col in list) 
        {
            if(col.GetComponent<IinterPlay>() != null)
            {
                myPlayer = col.GetComponent<IinterPlay>();
                myPlayer.SetisObjectNear(true);
                myPlayer.OpenUi.RemoveAllListeners();
                myPlayer.CloseUi.RemoveAllListeners();
                if (!LootingManager.Inst.PossibleList.Contains(transform.parent.GetComponent<Monster>()))
                {
                    LootingManager.Inst.PossibleList.Add(transform.parent.GetComponent<Monster>());
                }

                myPlayer.OpenUi.AddListener(LootingManager.Inst.OpenTarget);
                myPlayer.CloseUi.AddListener(LootingManager.Inst.CloseWindows);

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IinterPlay>() != null)
        {
            myPlayer = other.GetComponent<IinterPlay>();
            myPlayer.SetisObjectNear(true);
            myPlayer.OpenUi.RemoveAllListeners();
            myPlayer.CloseUi.RemoveAllListeners();
            if (!LootingManager.Inst.PossibleList.Contains(transform.parent.GetComponent<Monster>()))
            {
                LootingManager.Inst.PossibleList.Add(transform.parent.GetComponent<Monster>());
            }
            myPlayer.OpenUi.AddListener(LootingManager.Inst.OpenTarget);
            myPlayer.CloseUi.AddListener(LootingManager.Inst.CloseWindows);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent <IinterPlay>() != null)
        {
            myPlayer = other.GetComponent<IinterPlay>();
            LootingManager.Inst.RemoveMonster(transform.parent.GetComponent<Monster>());
            if( LootingManager.Inst.PossibleList.Count == 0)
            {
                myPlayer.OpenUi.RemoveAllListeners();
                myPlayer.CloseUi.RemoveAllListeners();
                myPlayer.SetisObjectNear(false);
            }
        }
    }

    private void OnDestroy()
    {
        if (LootingManager.Inst.PossibleList.Count == 0)
        {
            myPlayer.OpenUi.RemoveAllListeners();
            myPlayer.CloseUi.RemoveAllListeners();
            myPlayer.SetisObjectNear(false);
        }
    }
}
