using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShopNpc : NpcProperty
{
    delegate void ShopNpcDelegate();
    private void Awake()
    {
        _npctype = NPCType.Shop;
    }
    public void ShopView(GameObject obj)
    {
        obj.GetComponent<PlayerController>().SpringArm.gameObject.GetComponent<SpringArm>().ChangeViewPoint(ViewPoint);
        obj.transform.position = PlayerPoint.position;
        obj.transform.rotation = Quaternion.Euler(0, PlayerPoint.rotation.eulerAngles.y, 0);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IinterPlay>().SetisObjectNear(true);
        //other.gameObject.GetComponent<IinterPlay>().OpenUi.AddListener(() => { ShopManager.Inst.OpenShop(_npctype); });
        //other.gameObject.GetComponent<IinterPlay>().CloseUi.AddListener();
        other.gameObject.GetComponent<IinterPlay>()?.interPlay.AddListener(()=> { ShopView(other.gameObject); });

    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<IinterPlay>()?.SetisObjectNear(false);
    }


}
