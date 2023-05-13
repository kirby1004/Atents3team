using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : CharacterMovement_V2, IBattle
{
    public Transform SpringArm;
    public Transform WeaponPoint;

    public LayerMask npcMask;
    public LayerMask warpMask; //������ ����ҰŴϱ� �̸� ������.

    Vector3 playerRotate = Vector3.zero;

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
        if (!isShop || myAnim.GetBool("isAttacking"))
        {
            MoveToPos(Vector3.zero);
        }
    }

    protected override void LateUpdate()
    {
        //    if (toggleCameraRotation != true&&!isShop)
        //    {
        //        playerRotate = Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)); //���� ������� �� ��.
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * 15.0f); //���� �������� ĳ������ Rotation�� playerRotate�� �ٶ󺸰�
        //    }
    }

    bool isShop = false; //���� UI�� ���� �ִ��� ���θ� �����ϴ� bool isShop
    bool isNpc = false; //OnTriggerEnter, OnTriggerExit���� Npc ������ �����ִ��� �ƴ��� �Ǻ���.
    bool isSprint;
    void InputMethod()
    {
        if (!isShop)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //myAnim.SetBool("isClick", true); //isClick�� �̿��� ���콺 Ŭ���� ���Դ��� üũ
                myAnim.SetTrigger("Attack"); // ���� �ִϸ��̼� ����
            }

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                SpringArm.GetComponent<SpringArm>().toggleCameraRotation = true;
            }
            else
            {
                SpringArm.GetComponent<SpringArm>().toggleCameraRotation = false;
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
                }
                else //������ ���� ���� �� isShop�� false�� �ϰ� UI ����.
                {
                    isShop = false;
                    //���� �Ŵ������� ���� UI �ݾ��ֱ�
                    myCamera.GetComponent<FollowCamera>().Camera_OtherToPlayer(SpringArm); //ī�޶� �ٸ� �������� �÷��̾�� �ű�� �Լ��� ���������.
                }
            }
        }

        if (!myAnim.GetBool("isRolling")) rollPlayTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillManager.instance.RegisterSkill(Skillname.EnergyBall, WeaponPoint);
        }
    }

    Vector3 desireDirection;
    float SprintSpeed = 7.0f;
    float Speed;
    public override void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        Speed = (isSprint) ? SprintSpeed : MoveSpeed; //speed�� Sprint�� �ϴ��� ���ϴ��Ŀ� ���� ����, isSprint�� true�� sprintSpeed�� �ƴ϶�� MoveSpeed�� �����ȴ�.

        Vector3 inputDirection = Vector3.zero;
        if (inputDirection.z == 0) inputDirection.x = Input.GetAxisRaw("Horizontal");
        if (inputDirection.x == 0) inputDirection.z = Input.GetAxisRaw("Vertical");

        desireDirection = Vector3.Lerp(desireDirection, inputDirection, Time.deltaTime * 5.0f);

        myAnim.SetFloat("xDir", desireDirection.x);
        if (isSprint) myAnim.SetFloat("yDir", desireDirection.z * 2);
        else myAnim.SetFloat("yDir", desireDirection.z);

        if (!myAnim.GetBool("isRolling")) transform.Translate(inputDirection.normalized * Time.deltaTime * Speed);

        if (Input.GetKeyDown(KeyCode.Space) && inputDirection.magnitude != 0)
        {
            if (rollPlayTime >= rollCoolTime)
            {
                Roll(inputDirection);
                myAnim.SetTrigger("Roll");
                rollPlayTime = 0.0f;

            }
        }
    }


    int clickCount = 0;
    Coroutine coCheck = null;
    public void AttackEnter() //������ ���۵� �� ����Ǵ� �̺�Ʈ�Լ�, ���� �ִϸ��̼� �̺�Ʈ�� ���۵� �� ����ȴ�.
    {
        coCheck = StartCoroutine(ComboChecking());
    }

    public void AttackExit() //���� �ִϸ��̼� �̺�Ʈ�� ������ ���� �� ����ȴ�.
    {
        StopCoroutine(coCheck);
        if (clickCount == 0)
        {
            myAnim.SetTrigger("FailedCombo");
            myAnim.SetBool("isFailedCombo", true);
        }
    }

    IEnumerator ComboChecking()
    {
        myAnim.SetBool("isFailedCombo", false);
        clickCount = 0;
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickCount++;
            }
            yield return null;
        }
    }


    void Roll(Vector3 dir)
    {
        StartCoroutine(Rolling(dir));
    }

    float rollPlayTime = 3.0f;
    float rollCoolTime = 3.0f;
    IEnumerator Rolling(Vector3 dir)
    {
        myAnim.SetBool("isRolling", true);
        while (myAnim.GetBool("isRolling"))
        {
            gameObject.layer = 7; //�÷��̾��� ���̾ ���� ���̾�� �ٲ㼭 ���� �ʵ��� ��.
            transform.position += dir.normalized * Time.deltaTime * 1.5f;
            yield return null;
        }
        gameObject.layer = 8;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & npcMask) != 0)
        {
            isNpc = true;
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
        }
    }
}
