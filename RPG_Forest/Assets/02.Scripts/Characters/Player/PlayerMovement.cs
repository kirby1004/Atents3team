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
    //���ľ��� ��: mycamera�� �ű�°� �ƴ϶� viewpoint�� �Űܼ� ī�޶� �ϱ�,
    //ĳ���Ͱ� �̻��ϰ� �����̴°� ���ĺ���, ���� Ʈ������ �غ���.
    //����, �޺����õ� �̸� ¥�� �� ���� ondamage, on ��¼�� �����غ���..
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
            Vector3 playerRotate = Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)); //���� ������� �� ��.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * 15.0f); //���� �������� ĳ������ Rotation�� playerRotate�� �ٶ󺸰�
        }

       
    }

    private void Move() //ĳ���Ͱ� �����̴� �Լ�
    {
        Speed = (isSprint) ? SprintSpeed : MoveSpeed; //speed�� Sprint�� �ϴ��� ���ϴ��Ŀ� ���� ����, isSprint�� true�� sprintSpeed�� �ƴ϶�� MoveSpeed�� �����ȴ�.

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
        Collider[] list = Physics.OverlapSphere(WeaponPoint.position, 0.75f, enemyMask); //���� ����Ʈ�� ĳ���� ������ ��ġ�� ������ ����.
                                                                                        //  �� ��ġ�� 0.75 �������� ���� ���� ���ݹ����� ������ �ִ�. (���߿� ���� ����)
                                                                                        // overLap�� ������ �ݶ��̴����� list�� ����.
        Debug.DrawLine(WeaponPoint.position, WeaponPoint.position + new Vector3(0.5f, 0.5f, 0.5f)); //�̰� ������ Ȯ���ϱ� ���� DrawLine (���߿� ���� ����)
        foreach (Collider col in list) //list�� �ݶ��̴����� �ϳ��ϳ� ������ IBattle ������Ʈ�� ������ �ִ��� Ȯ���ϰ� ������ => OnDamage �Լ� ����.
        {
            col.transform.GetComponent<IBattle>()?.OnDamage(AttackPoint);
        }
    }

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
}
