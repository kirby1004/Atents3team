using System.Collections;
using System.Collections.Generic;
using System.IO;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Events;

public class PlayerMovement : CharacterMovement,IBattle
{
    //고쳐야할 거: mycamera를 옮기는게 아니라 viewpoint를 옮겨서 카메라 하기,
    //캐릭터가 이상하게 움직이는거 고쳐보기, 블렌드 트리까지 해보기.
    //어택, 콤보어택도 미리 짜둔 거 쓰고 ondamage, on 어쩌구 까지해보깅..
    public Transform ViewPoint;
    public Transform WeaponPoint;
    

    public LayerMask enemyMask;

    bool isSprint;
    bool toggleCameraRotation;

    float SprintSpeed = 7.0f;
    float Speed;

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

    void Update()
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = true;
        }
        else
        {
            isSprint = false;
        }
        Move();
    }

    private void FixedUpdate()
    {
      
        
    }

    private void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)); //벡터 구성요소 별 곱.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * 15.0f); //구형 보간으로 캐릭터의 Rotation을 playerRotate로 바라보게
        }

       
    }

    private void Move() //캐릭터가 움직이는 함수
    {
        Speed = (isSprint) ? SprintSpeed : MoveSpeed; //speed를 Sprint를 하느냐 안하느냐에 따라 결정, isSprint가 true면 sprintSpeed를 아니라면 MoveSpeed로 설정된다.

        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        Vector3 right = transform.TransformDirection(Vector3.right).normalized;
        float x = Input.GetAxisRaw("Vertical");
        float z = Input.GetAxisRaw("Horizontal");
        Vector3 moveDirection = forward * x+ right *z ;
        bool isMoving = (new Vector3(x,0,z).magnitude != 0);
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
            if(isSprint) myAnim.SetFloat("yDir", x*2);
            else myAnim.SetFloat("yDir", x);
        }
    }

    public void Attack()
    {
        Collider[] list = Physics.OverlapSphere(WeaponPoint.position, 0.75f, enemyMask); //웨폰 포인트는 캐릭터 무기의 위치를 가지고 있음.
                                                                                        //  그 위치의 0.75 반지름을 가진 구의 공격범위를 가지고 있다. (나중에 수정 예정)
                                                                                        // overLap에 감지된 콜라이더들을 list에 저장.
        Debug.DrawLine(WeaponPoint.position, WeaponPoint.position + new Vector3(0.5f, 0.5f, 0.5f)); //이건 범위를 확인하기 위한 DrawLine (나중에 지울 예정)
        foreach (Collider col in list) //list의 콜라이더들을 하나하나 꺼내서 IBattle 컴포넌트를 가지고 있는지 확인하고 맞으면 => OnDamage 함수 실행.
        {
            col.transform.GetComponent<IBattle>()?.OnDamage(AttackPoint);
        }
    }

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
}
