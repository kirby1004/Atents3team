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
        StartCoroutine(ShopViewing(obj));
    }

    IEnumerator ShopViewing(GameObject obj)
    {
        obj.transform.position = PlayerPoint.position;
        obj.transform.localRotation = playerPoint.localRotation;
        //while (!Mathf.Approximately(obj.GetComponent<PlayerController>().SpringArm.GetComponent<SpringArm>().CurRot.y, PlayerPoint.rotation.eulerAngles.y))
        {
            //obj.GetComponent<PlayerController>().SpringArm.GetComponent<SpringArm>().CurRot=Vector3.Lerp(obj.GetComponent<PlayerController>().SpringArm.GetComponent<SpringArm>().CurRot, PlayerPoint.rotation.eulerAngles, Time.deltaTime * 3.0f);
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IinterPlay>().SetisObjectNear(true);
        //other.gameObject.GetComponent<IinterPlay>().OpenUi.AddListener(() => { ShopManager.Inst.OpenShop(_npctype); });
        //other.gameObject.GetComponent<IinterPlay>().CloseUi.AddListener();
        other.gameObject.GetComponent<IinterPlay>()?.OpenUi.AddListener(()=> { ShopView(other.gameObject); });

    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<IinterPlay>()?.SetisObjectNear(false);
    }


}
