using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Events;

public class ShopNpc : NpcProperty
{
    delegate void ShopNpcDelegate();
    private void Awake()
    {
        _npctype = NPCType.Shop;
    }
    public void ShopView(GameObject obj,UnityAction e=null)
    {
        StartCoroutine(ShopViewing(obj,e));
    }

    IEnumerator ShopViewing(GameObject obj,UnityAction e=null)
    {
        obj.transform.position = PlayerPoint.position;
        //obj.GetComponent<PlayerController>().SpringArm.GetComponent<SpringArm>().CurRot.Set(0, playerPoint.localRotation.eulerAngles.y, 0);
        //while (!Mathf.Approximately(obj.GetComponent<PlayerController>().SpringArm.localRotation.y, PlayerPoint.rotation.eulerAngles.y))
        //{
        //    obj.GetComponent<PlayerController>().SpringArm.localRotation=Quaternion.Slerp(obj.GetComponent<PlayerController>().SpringArm.localRotation,playerPoint.localRotation, Time.deltaTime * 3.0f);
        //    yield return null;
        //}
        obj.GetComponentInChildren<SpringArm>().CameraChange = true;
        while (!Mathf.Approximately(obj.transform.localRotation.eulerAngles.y, PlayerPoint.rotation.eulerAngles.y))
        {
            obj.transform.localRotation = Quaternion.Slerp(obj.transform.localRotation, playerPoint.localRotation, Time.deltaTime * 15.0f);
            yield return null;
        }
        obj.GetComponentInChildren<SpringArm>().CurRot = obj.transform.localRotation.eulerAngles;
        e?.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IinterPlay>().SetisObjectNear(true);
        other.gameObject.GetComponent<IinterPlay>()?.OpenUi.AddListener(()=> { ShopView(other.gameObject,()=>other.GetComponentInChildren<SpringArm>().ViewPointTransformation(ViewPoint)); });

    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<IinterPlay>()?.SetisObjectNear(false);
    }


}
