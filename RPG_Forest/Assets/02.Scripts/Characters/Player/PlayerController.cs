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

    bool isShop = false; //상점 UI가 열려 있는지 여부를 저장하는 bool isShop
    bool isNpc = false; //OnTriggerEnter, OnTriggerExit으로 Npc 범위에 들어와있는지 아닌지 판별함.
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
            playerRotate = Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)); //벡터 구성요소 별 곱.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * 15.0f); //구형 보간으로 캐릭터의 Rotation을 playerRotate로 바라보게
        }
    }

    void InputMethod()
    {
        if (!isShop)
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetBool("isClick", true); //isClick을 이용해 마우스 클릭이 들어왔는지 체크
                myAnim.SetTrigger("Attack"); // 공격 애니메이션 실행
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
            if (isNpc)  //F를 눌렀을 때 NPC인지 아닌지
            {
                if (!isShop)    //상점이 열려있지 않을 때, isShop을 true로 해주고 움직임 애니메이션을 강제로 idle로 바꿔줌.
                {
                    isShop = true;
                    myAnim.SetFloat("xDir", 0);
                    myAnim.SetFloat("yDir", 0);
                    //게임매니저에서 Shop이 어떤 Shop인지 enum으로 비교하고 그에 맞는 UI를 실행시켜줘야함.
                    // 플레이어가 상호작용가능한 가장 최신 대상의 유니티액션을 작동시키기
                    // 담겨있는 함수 - 상점 열기
                    myInteractTarget.GetComponent<Npc>().ShopOpen?.Invoke(myInteractTarget.transform);
                }
                else //상점이 열려 있을 때 isShop을 false로 하고 UI 끄기.
                {
                    isShop = false;
                    //게임 매니저에서 상점 UI 닫아주기
                    myCamera.GetComponent<FollowCamera>().Camera_OtherToPlayer(CameraPoint); //카메라를 다른 시점에서 플레이어로 옮기는 함수를 실행시켜줌.
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) //-- 구르기 구현중 --
        {
            Roll();
        }
        
    }

    public override void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        Speed = (isSprint) ? SprintSpeed : MoveSpeed; //speed를 Sprint를 하느냐 안하느냐에 따라 결정, isSprint가 true면 sprintSpeed를 아니라면 MoveSpeed로 설정된다.

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

    #region 공격
    public void AttackEnter() //공격이 시작될 때 실행되는 이벤트함수, 공격 애니메이션 이벤트로 시작될 때 실행된다.
    {
        myAnim.SetBool("ComboAttack", true); //공격이 시작되면 콤보어택을 true로 바꾸고 isClick을 false로 둔다=> 공격 중간에 클릭이 들어오는 지 확인하기 위함.
        myAnim.SetBool("isClick", false);
    }

    public void AttackExit() //공격 애니메이션 이벤트로 공격이 끝날 때 실행된다.
    {
        if (myAnim.GetBool("isClick")) //isClick이 true인지 false인지 확인함.
        {
            myAnim.SetBool("ComboAttack", true);  //true라면 재입력이 들어온 것이기 때문에 콤보어택을 true로 계속 유지시킨다.
        }
        else
        {
            myAnim.SetBool("ComboAttack", false); //아니라면 ComboAttack은 false
        }
    }
    #endregion

    void Roll()
    {
        StartCoroutine(Rolling());
    }

    IEnumerator Rolling()
    {
        gameObject.layer = 7; //플레이어의 레이어를 무적 레이어로 바꿔서 맞지 않도록 함.
        yield return null;
    }
    // 상호작용이 가능한 대상 저장
    // -> 여러 대상을 확인할 방법 찾아야함 & 1번 말걸면 다시 말걸기 어려워짐
    // npc 가 뭉쳐잇으면 카메라 타겟이 맛이감
    public Transform myInteractTarget = null;
    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & npcMask) != 0)
        {
            isNpc = true;

            // 상호작용 대상을 가장 나중에 들어온 대상으로 설정 후
            // ShopOpen 유니티액션에 상점열기 함수 추가
            myInteractTarget = other.transform;
            other.GetComponent<Npc>().ShopOpen +=
                other.GetComponent<Npc>().OpenMyShop2;
        }
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (isShop) //isShop이 트루일 때 
        {
            myCamera.GetComponent<FollowCamera>().Camera_PlayerToOther(other.gameObject.GetComponent<Npc>()?.ViewPoint);          //카메라를 플레이어에서 다른 오브젝트로 이동시키는 함수 실행, 보이는 위치는 NPC에서 가져옴.
            Transform playerPoint = other.gameObject.GetComponent<Npc>()?.playerPoint;      //플레이어 이동도 필요하기 때문에 NPC에서 포인트를 받아서 저장.
            transform.position = playerPoint.position;                                      //캐릭터의 위치와 회전을 NPC에서 미리 저장한 Point의 위치와 회전을 가져와 설정.
            transform.rotation = Quaternion.Euler(0, playerPoint.rotation.eulerAngles.y, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & npcMask) != 0)
        {
            isNpc = false;

            // 상호작용 범위에서 나가는 npc의 ShopOpen에 함수가 들어가 있을때 제거시키기
            if (other.GetComponent<Npc>().ShopOpen != null)
            {
                other.GetComponent<Npc>().ShopOpen -=
                    other.GetComponent<Npc>().OpenMyShop2;
            }
        }
    }
}
