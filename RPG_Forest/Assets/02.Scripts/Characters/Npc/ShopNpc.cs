using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShopNpc : NpcProperty
{
    #region ShopNpc �������
    public LayerMask customer;
    public Animator npcAnim;
    #endregion

    private void Awake()
    {
        _npcType = NPCType.Shop;
    }

    #region ���� ���� �Լ�
    public void ShopView(GameObject obj,UnityAction e=null,UnityAction viewstart = null, UnityAction viewEnd = null)
    {
        StartCoroutine(ShopViewing(obj,e, viewstart, viewEnd));
    }

    IEnumerator ShopViewing(GameObject obj,UnityAction e=null,UnityAction viewstart=null,UnityAction viewEnd=null)
    {
        viewstart?.Invoke();
        obj.transform.position = PlayerPoint.position;
        obj.GetComponentInChildren<SpringArm>().CameraChange = true;
        while (!Mathf.Approximately(obj.transform.localRotation.eulerAngles.y, PlayerPoint.rotation.eulerAngles.y))
        {
            obj.transform.localRotation = Quaternion.Slerp(obj.transform.localRotation, playerPoint.localRotation, Time.deltaTime * 15.0f);
            yield return null;
        }
        obj.GetComponentInChildren<SpringArm>().CurRot = obj.transform.localRotation.eulerAngles;
        e?.Invoke();
        yield return null;
        viewEnd?.Invoke();
    }
    #endregion

    #region �÷��̾� ��ȣ�ۿ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & customer) != 0)
        {
            other.gameObject.GetComponent<IinterPlay>().SetisObjectNear(true);
            other.gameObject.GetComponent<IinterPlay>()?.OpenUi.AddListener(() => {
                ShopView(other.gameObject, () => other.GetComponentInChildren<SpringArm>().ViewPointTransformation(ViewPoint, 
                    () => ShopManager.Inst.OpenShop(NpcType, () => { other.gameObject.GetComponent<IinterPlay>().SetisUI(true); })), 
                    () => { other.gameObject.GetComponent<IinterPlay>().SetisInterPlay(true); }, 
                    () => { other.gameObject.GetComponent<IinterPlay>().SetisInterPlay(false); });
            });
            other.gameObject.GetComponent<IinterPlay>().CloseUi.AddListener(() => 
                { ShopManager.Inst.CloseShop(
                    () => other.GetComponentInChildren<SpringArm>().ViewPointReset(other.GetComponentInChildren<SpringArm>().transform, 
                    () => other.gameObject.GetComponent<IinterPlay>().SetisUI(false)), () => npcAnim.SetTrigger("Greet")); });
            ShopManager.Inst.ExitButton.onClick.AddListener(() => other.gameObject.GetComponent<IinterPlay>().CloseUi?.Invoke());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & customer) != 0)
        {
            other.gameObject.GetComponent<IinterPlay>()?.SetisObjectNear(false);
        }
    }
    #endregion

}
