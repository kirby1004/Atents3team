using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//���� ��� �޴� ��ũ��Ʈ
public class ToggleButton : MonoBehaviour 
{
    public Button mybutton;

    //�޴� ���
    public List<GameObject> mySystems;
    // Start is called before the first frame update
    void Start()
    {
        //�޴���� 1.�κ��丮 2.���â 3.�������ͽ� 4.�ҿﰭȭ(�߰�����)
        mybutton = GetComponent<Button>();
        mySystems.Add(InventoryManager.Inst.gameObject);
        mySystems.Add(EquipmentManager.Inst.gameObject);
        mySystems.Add(StatusManager.Inst.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if(mybutton.enabled)
        //{
        //    if (gameObject.activeSelf)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        gameObject.SetActive(true);
        //    }
        //}
    }
    public void ToggleBUtton(Button button)
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // �޴�â �����ѱ�
    public void OnbuttonClick(int index)
    {
        mySystems[index].gameObject.SetActive(!mySystems[index].gameObject.activeSelf);
        //if (mySystems[index].gameObject.activeSelf)
        //{
        //    mySystems[index].gameObject.SetActive(false);
        //}
        //else
        //{
        //    mySystems[index].gameObject.SetActive(true);
        //}
    }

}
