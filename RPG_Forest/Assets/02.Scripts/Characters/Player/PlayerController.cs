using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : CharacterMovement_V2, IBattle,IinterPlay
{
    public Transform SpringArm;
    public Transform WeaponPoint;

    public LayerMask npcMask;
    public LayerMask warpMask; //워프도 사용할거니까 미리 만들어둠.

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

    protected override void Start()
    {
        base.Start();
        interPlay = new UnityEvent();
        OpenUi = new UnityEvent();
        CloseUi = new UnityEvent();
    }
    public bool isEnterUI = false;

    // Update is called once per frame
    protected override void Update()
    {
        
        InputMethod();
        
        if (!isUi&&!myAnim.GetBool("isAttacking"))
        {
            MoveToPos(Vector3.zero);
        }
    }

    protected override void LateUpdate()
    {
    //    if (toggleCameraRotation != true&&!isShop)
    //    {
    //        playerRotate = Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)); //벡터 구성요소 별 곱.
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * 15.0f); //구형 보간으로 캐릭터의 Rotation을 playerRotate로 바라보게
    //    }
    }

    [SerializeField]
    bool isObjectNear;
    bool isUi;
    public void SetisObjectNear(bool n)
    {
        isObjectNear = n;
    }
    public void SetisUI(bool n)
    {
        isUi = n;
    }
    public UnityEvent OpenUi { get; set; }    
    public UnityEvent interPlay
    {
        get; set;
    }
    public UnityEvent CloseUi { get; set; }
    public UnityAction OpenLoot { get; set; }

    bool isSprint;
    void InputMethod()
    {
        if (!isUi )
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isEnterUI == true) return;
                //myAnim.SetBool("isClick", true); //isClick을 이용해 마우스 클릭이 들어왔는지 체크
                myAnim.SetTrigger("Attack"); // 공격 애니메이션 실행
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

        if (isObjectNear)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!isUi)
                {
                    if (!isUi)    //상점이 열려있지 않을 때, isShop을 true로 해주고 움직임 애니메이션을 강제로 idle로 바꿔줌.
                    {
                        OpenUi?.Invoke();
                    }
                    else //상점이 열려 있을 때 isShop을 false로 하고 UI 끄기.
                    {


                        CloseUi?.Invoke();
                    }
                }
            }
        }
        else
        {
            OpenUi.RemoveAllListeners();
            CloseUi.RemoveAllListeners();
        }


        if (!myAnim.GetBool("isRolling")) rollPlayTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillManager.instance.RegisterSkill(Skillname.EnergyBall, WeaponPoint);
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SpringArm.GetComponent<SpringArm>()?.ViewPointTransformation(TestTrans);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SpringArm.GetComponent<SpringArm>()?.ViewPointReset(SpringArm);
        }
    }
    public Transform TestTrans;

    Vector3 desireDirection;
    float SprintSpeed = 5.0f;
    float Speed;
    public override void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        Speed = (isSprint) ? SprintSpeed : MoveSpeed; //speed를 Sprint를 하느냐 안하느냐에 따라 결정, isSprint가 true면 sprintSpeed를 아니라면 MoveSpeed로 설정된다.

        Vector3 inputDirection = Vector3.zero;
        if(inputDirection.z==0)inputDirection.x = Input.GetAxisRaw("Horizontal");
        if (inputDirection.x == 0) inputDirection.z = Input.GetAxisRaw("Vertical");

        desireDirection = Vector3.Lerp(desireDirection, inputDirection, Time.deltaTime * 5.0f);

        myAnim.SetFloat("xDir", desireDirection.x);
        if(isSprint) myAnim.SetFloat("yDir", desireDirection.z*2);
        else myAnim.SetFloat("yDir", desireDirection.z);

        if(!myAnim.GetBool("isRolling"))transform.Translate(inputDirection.normalized * Time.deltaTime * Speed);

        if (Input.GetKeyDown(KeyCode.Space)&&inputDirection.magnitude!=0) 
        {
            if (rollPlayTime >= rollCoolTime)
            {
                Roll(inputDirection);
                myAnim.SetTrigger("Roll");
                rollPlayTime = 0.0f;

            }
        }
    }

    public void SetIsEnterUI(bool bools)
    {
        isEnterUI = bools;
    }
    int clickCount = 0;
    Coroutine coCheck = null;
    public void AttackEnter() //공격이 시작될 때 실행되는 이벤트함수, 공격 애니메이션 이벤트로 시작될 때 실행된다.
    {
        coCheck = StartCoroutine(ComboChecking());
    }

    public void AttackExit() //공격 애니메이션 이벤트로 공격이 끝날 때 실행된다.
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
            gameObject.layer = 10; //플레이어의 레이어를 무적 레이어로 바꿔서 맞지 않도록 함.
            transform.position += dir.normalized * Time.deltaTime*1.5f;
            yield return null;
        }
        gameObject.layer = 8;
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if(((1 << other.gameObject.layer) & npcMask) != 0)
    //    {
    //        isNpc = true;
    //    }
        
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (isUi) //isShop이 트루일 때 
    //    {
    //        myCamera.GetComponent<FollowCamera>().Camera_PlayerToOther(other.gameObject.GetComponent<Npc>()?.ViewPoint);          //카메라를 플레이어에서 다른 오브젝트로 이동시키는 함수 실행, 보이는 위치는 NPC에서 가져옴.
    //        Transform playerPoint = other.gameObject.GetComponent<Npc>()?.playerPoint;      //플레이어 이동도 필요하기 때문에 NPC에서 포인트를 받아서 저장.
    //        transform.position = playerPoint.position;                                      //캐릭터의 위치와 회전을 NPC에서 미리 저장한 Point의 위치와 회전을 가져와 설정.
    //        transform.rotation = Quaternion.Euler(0, playerPoint.rotation.eulerAngles.y, 0); 
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (((1 << other.gameObject.layer) & npcMask) != 0)
    //    {
    //        isNpc = false;
    //    }
    //}
}
