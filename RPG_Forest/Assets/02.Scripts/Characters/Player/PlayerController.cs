using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : CharacterMovement_V2, IBattle
{
    public Transform CameraPoint;
    public Transform WeaponPoint;

    bool isSprint;
    bool toggleCameraRotation;

    float SprintSpeed = 7.0f;
    float Speed;

    bool isShop = false; //���� UI�� ���� �ִ��� ���θ� �����ϴ� bool isShop
    bool isNpc = false; //OnTriggerEnter, OnTriggerExit���� Npc ������ �����ִ��� �ƴ��� �Ǻ���.
    public LayerMask npcMask;

    public LayerMask warpMask; //������ ����ҰŴϱ� �̸� ������.


    Vector3 playerRotate=Vector3.zero;

    public bool IsLive
    {
        get => !Mathf.Approximately(curHp, 0.0f);
    }

    public void OnDamage(float dmg)
    {
        curHp -= dmg;

        if (Mathf.Approximately(curHp, 0.0f))
        {
            Collider[] list = transform.GetComponentsInChildren<Collider>();
            foreach (Collider col in list)
            {
                col.enabled = false;
            }
            DeathAlarm?.Invoke();
            myAnim.SetTrigger("Die");
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        InputMethod();
        if (!isShop)
        {
            MoveToPos(Vector3.zero);
        }
    }

    protected override void LateUpdate()
    {
        if (toggleCameraRotation != true&&!isShop)
        {
            playerRotate = Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)); //���� ������� �� ��.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * 15.0f); //���� �������� ĳ������ Rotation�� playerRotate�� �ٶ󺸰�
        }
    }

    void InputMethod()
    {
        if (!isShop)
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetBool("isClick", true); //isClick�� �̿��� ���콺 Ŭ���� ���Դ��� üũ
                myAnim.SetTrigger("Attack"); // ���� �ִϸ��̼� ����
            }

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                toggleCameraRotation = true;
            }
            else
            {
                toggleCameraRotation = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = true;
        }
        else
        {
            isSprint = false;
        }

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if (isNpc)  //F�� ������ �� NPC���� �ƴ���
            {
                if (!isShop)    //������ �������� ���� ��, isShop�� true�� ���ְ� ������ �ִϸ��̼��� ������ idle�� �ٲ���.
                {
                    isShop = true;
                    myAnim.SetFloat("xDir", 0);
                    myAnim.SetFloat("yDir", 0);
                    //���ӸŴ������� Shop�� � Shop���� enum���� ���ϰ� �׿� �´� UI�� ������������.
                    // �÷��̾ ��ȣ�ۿ밡���� ���� �ֽ� ����� ����Ƽ�׼��� �۵���Ű��
                    // ����ִ� �Լ� - ���� ����
                    myInteractTarget.GetComponent<Npc>().ShopOpen?.Invoke(myInteractTarget.transform);
                }
                else //������ ���� ���� �� isShop�� false�� �ϰ� UI ����.
                {
                    isShop = false;
                    //���� �Ŵ������� ���� UI �ݾ��ֱ�
                    myCamera.GetComponent<FollowCamera>().Camera_OtherToPlayer(CameraPoint); //ī�޶� �ٸ� �������� �÷��̾�� �ű�� �Լ��� ���������.
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) //-- ������ ������ --
        {
            Roll();
        }
        
    }

    public override void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        Speed = (isSprint) ? SprintSpeed : MoveSpeed; //speed�� Sprint�� �ϴ��� ���ϴ��Ŀ� ���� ����, isSprint�� true�� sprintSpeed�� �ƴ϶�� MoveSpeed�� �����ȴ�.

        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        Vector3 right = transform.TransformDirection(Vector3.right).normalized;
        float x = Input.GetAxisRaw("Vertical");
        float z = Input.GetAxisRaw("Horizontal");
        Vector3 moveDirection = forward * x + right * z;
        bool isMoving = (new Vector3(x, 0, z).magnitude != 0);
        myAnim.SetBool("isMoving", isMoving);
        if (!isMoving)
        {
            myAnim.SetFloat("xDir", 0);
            myAnim.SetFloat("yDir", 0);
        }
        else
        {
            transform.position += moveDirection.normalized * Time.deltaTime * Speed;
            myAnim.SetFloat("xDir", z);
            if (isSprint) myAnim.SetFloat("yDir", x * 2);
            else myAnim.SetFloat("yDir", x);
        }
    }

    #region ����
    public void AttackEnter() //������ ���۵� �� ����Ǵ� �̺�Ʈ�Լ�, ���� �ִϸ��̼� �̺�Ʈ�� ���۵� �� ����ȴ�.
    {
        myAnim.SetBool("ComboAttack", true); //������ ���۵Ǹ� �޺������� true�� �ٲٰ� isClick�� false�� �д�=> ���� �߰��� Ŭ���� ������ �� Ȯ���ϱ� ����.
        myAnim.SetBool("isClick", false);
    }

    public void AttackExit() //���� �ִϸ��̼� �̺�Ʈ�� ������ ���� �� ����ȴ�.
    {
        if (myAnim.GetBool("isClick")) //isClick�� true���� false���� Ȯ����.
        {
            myAnim.SetBool("ComboAttack", true);  //true��� ���Է��� ���� ���̱� ������ �޺������� true�� ��� ������Ų��.
        }
        else
        {
            myAnim.SetBool("ComboAttack", false); //�ƴ϶�� ComboAttack�� false
        }
    }
    #endregion

    void Roll()
    {
        StartCoroutine(Rolling());
    }

    IEnumerator Rolling()
    {
        gameObject.layer = 7; //�÷��̾��� ���̾ ���� ���̾�� �ٲ㼭 ���� �ʵ��� ��.
        yield return null;
    }
    // ��ȣ�ۿ��� ������ ��� ����
    // -> ���� ����� Ȯ���� ��� ã�ƾ��� & 1�� ���ɸ� �ٽ� ���ɱ� �������
    // npc �� ���������� ī�޶� Ÿ���� ���̰�
    public Transform myInteractTarget = null;
    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & npcMask) != 0)
        {
            isNpc = true;

            // ��ȣ�ۿ� ����� ���� ���߿� ���� ������� ���� ��
            // ShopOpen ����Ƽ�׼ǿ� �������� �Լ� �߰�
            myInteractTarget = other.transform;
            other.GetComponent<Npc>().ShopOpen +=
                other.GetComponent<Npc>().OpenMyShop2;
        }
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (isShop) //isShop�� Ʈ���� �� 
        {
            myCamera.GetComponent<FollowCamera>().Camera_PlayerToOther(other.gameObject.GetComponent<Npc>()?.ViewPoint);          //ī�޶� �÷��̾�� �ٸ� ������Ʈ�� �̵���Ű�� �Լ� ����, ���̴� ��ġ�� NPC���� ������.
            Transform playerPoint = other.gameObject.GetComponent<Npc>()?.playerPoint;      //�÷��̾� �̵��� �ʿ��ϱ� ������ NPC���� ����Ʈ�� �޾Ƽ� ����.
            transform.position = playerPoint.position;                                      //ĳ������ ��ġ�� ȸ���� NPC���� �̸� ������ Point�� ��ġ�� ȸ���� ������ ����.
            transform.rotation = Quaternion.Euler(0, playerPoint.rotation.eulerAngles.y, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & npcMask) != 0)
        {
            isNpc = false;

            // ��ȣ�ۿ� �������� ������ npc�� ShopOpen�� �Լ��� �� ������ ���Ž�Ű��
            if (other.GetComponent<Npc>().ShopOpen != null)
            {
                other.GetComponent<Npc>().ShopOpen -=
                    other.GetComponent<Npc>().OpenMyShop2;
            }
        }
    }
}
