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

    bool toggleCameraRotation;
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

        if (Input.GetKeyDown(KeyCode.Space)) //-- ������ ������ --
        {
            if (rollPlayTime >= rollCoolTime)
            {
                Roll();
                myAnim.SetTrigger("Roll");
                rollPlayTime = 0.0f;

            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillManager.instance.RegisterSkill(Skillname.EnergyBall, WeaponPoint);
        }
    }

    Vector2 desireDirection;
    float SprintSpeed = 7.0f;
    float Speed;
    public override void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        Speed = (isSprint) ? SprintSpeed : MoveSpeed; //speed�� Sprint�� �ϴ��� ���ϴ��Ŀ� ���� ����, isSprint�� true�� sprintSpeed�� �ƴ϶�� MoveSpeed�� �����ȴ�.

        Vector2 inputDirection = Vector2.zero;
        if(inputDirection.y==0)inputDirection.x = Input.GetAxisRaw("Horizontal");
        if (inputDirection.x == 0) inputDirection.y = Input.GetAxisRaw("Vertical");

        desireDirection = Vector2.Lerp(desireDirection, inputDirection, Time.deltaTime * 5.0f);

        myAnim.SetFloat("xDir", desireDirection.x);
        if(isSprint) myAnim.SetFloat("yDir", desireDirection.y*2);
        else myAnim.SetFloat("yDir", desireDirection.y);
        //Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        //Vector3 right = transform.TransformDirection(Vector3.right).normalized;
        //float x = Input.GetAxisRaw("Vertical");
        //float z = Input.GetAxisRaw("Horizontal");
        //Vector3 moveDirection = forward * x + right * z;
        //bool isMoving = (new Vector3(x, 0, z).magnitude != 0);
        //myAnim.SetBool("isMoving", isMoving);
        //if (!isMoving)
        //{
        //    myAnim.SetFloat("xDir", 0);
        //    myAnim.SetFloat("yDir", 0);
        //}
        //else
        //{
        //    transform.position += moveDirection.normalized * Time.deltaTime * Speed;
        //    myAnim.SetFloat("xDir", z);
        //    if (isSprint) myAnim.SetFloat("yDir", x * 2);
        //    else myAnim.SetFloat("yDir", x);
        //}
        
    }


    int clickCount = 0;
    Coroutine coCheck = null;
    public void AttackEnter() //������ ���۵� �� ����Ǵ� �̺�Ʈ�Լ�, ���� �ִϸ��̼� �̺�Ʈ�� ���۵� �� ����ȴ�.
    {
        //myAnim.SetBool("ComboAttack", true); //������ ���۵Ǹ� �޺������� true�� �ٲٰ� isClick�� false�� �д�=> ���� �߰��� Ŭ���� ������ �� Ȯ���ϱ� ����.
        //myAnim.SetBool("isClick", false);
        coCheck = StartCoroutine(ComboChecking());
    }

    public void AttackExit() //���� �ִϸ��̼� �̺�Ʈ�� ������ ���� �� ����ȴ�.
    {
        //if (myAnim.GetBool("isClick")) //isClick�� true���� false���� Ȯ����.
        //{
        //    myAnim.SetBool("ComboAttack", true);  //true��� ���Է��� ���� ���̱� ������ �޺������� true�� ��� ������Ų��.
        //}
        //else
        //{
        //    myAnim.SetBool("ComboAttack", false); //�ƴ϶�� ComboAttack�� false
        //}
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


    void Roll()
    {
        StartCoroutine(Rolling());
    }

    float rollPlayTime = 3.0f;
    float rollCoolTime = 3.0f;
    IEnumerator Rolling()
    {
        myAnim.SetBool("isRolling", true);
        while (myAnim.GetBool("isRolling"))
        {
            gameObject.layer = 7; //�÷��̾��� ���̾ ���� ���̾�� �ٲ㼭 ���� �ʵ��� ��.
            //transform.position += transform.forward * Time.deltaTime * MoveSpeed;
            yield return null;
        }
        gameObject.layer = 8;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & npcMask) != 0)
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
