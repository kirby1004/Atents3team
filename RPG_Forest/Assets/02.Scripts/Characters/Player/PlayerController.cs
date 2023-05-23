using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : CharacterMovement_V2, IBattle,IinterPlay
{
    #region �÷��̾� ��� ����
    public Transform SpringArm;
    public Transform WeaponPoint;

    //UI�� IinterPlay�� ���õ� ����
    [SerializeField]
    bool isObjectNear;
    [SerializeField]
    bool isUi;
    [SerializeField]
    public bool isEnterUI = false;
    
    //�̵� ���� ����
    bool isSprint;
    Vector3 desireDirection;
    float SprintSpeed = 5.0f;
    float Speed;

    //���� ���� ����
    int clickCount = 0;
    Coroutine coCheck = null;
    // �÷��̾� ���� ����ջ� ������
    public new float AttackPoint {get{return myBaseStatus.AttackPoint + EquipmentManager.Inst.equipmentAP;}}
    public new float DefensePoint { get { return myBaseStatus.DefensePoint + EquipmentManager.Inst.equipmentDP; } }
    public new float MaxHp { get { return myBaseStatus.MaxHp + EquipmentManager.Inst.equipmentAP; } }
    public new float MoveSpeed { get { return myBaseStatus.MoveSpeed + EquipmentManager.Inst.equipmentAP; } }

    //������ ���� ����
    float rollPlayTime = 3.0f;
    float rollCoolTime = 3.0f;
    #endregion

    #region IBattle �������̵�
    public bool IsLive
    {
        get => !Mathf.Approximately(curHp, 0.0f);
    }
    public void OnDamage(float dmg)
    {
        curHp -= GameManager.instance.DamageDecrease(dmg, DefensePoint);

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
    #endregion

    #region IinterPlay �������̵�
    public UnityEvent OpenUi { get; set; }
    public UnityEvent interPlay { get; set; }
    public UnityEvent CloseUi { get; set; }

    public void SetisObjectNear(bool n)
    {
        isObjectNear = n;
    }
    public void SetisUI(bool n)
    {
        isUi = n;
    }
    #endregion

    #region Start, Update��
    protected override void Start()
    {
        base.Start();
        interPlay = new UnityEvent();
        OpenUi = new UnityEvent();
        CloseUi = new UnityEvent();
    }

    protected override void Update()
    {
        
        InputMethod();
        
        if (!isUi&&!myAnim.GetBool("isAttacking")&&!myAnim.GetBool("isSkill"))
        {
            MoveToPos(Vector3.zero);
        }
    }
    #endregion

    #region InputMethod (�Է��Լ�)
    void InputMethod()
    {
        if (!isUi&& !myAnim.GetBool("isSkill"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isEnterUI == true) return;

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

        if (isObjectNear)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!isUi)
                {
                     OpenUi?.Invoke();
                }
                else
                {
                    CloseUi?.Invoke();
                }
            }
        }
        else
        {
            OpenUi.RemoveAllListeners();
            CloseUi.RemoveAllListeners();
        }


        if (!myAnim.GetBool("isRolling")) rollPlayTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1) && !SkillManager.Instance.playerSkillCooldown[PlayerSkillName.EnergyBall])
        {
            myAnim.SetTrigger("Skill");
            myAnim.SetInteger("skillNum", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)&& !SkillManager.Instance.playerSkillCooldown[PlayerSkillName.EnergyTornado])
        {
            myAnim.SetTrigger("Skill");
            myAnim.SetInteger("skillNum", 2);
        }
    }
    #endregion

    #region MoveToPos (������ �Լ�)
    public override void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        Speed = (isSprint) ? SprintSpeed : MoveSpeed; //speed�� Sprint�� �ϴ��� ���ϴ��Ŀ� ���� ����, isSprint�� true�� sprintSpeed�� �ƴ϶�� MoveSpeed�� �����ȴ�.

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
    #endregion

    #region UI ���� �Լ�
    public void SetIsEnterUI(bool bools)
    {
        isEnterUI = bools;
    }
    #endregion

    #region ���� ���� �Լ�
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

    public void SkillOn1(Transform SkillPoint)
    {
        SkillManager.instance.RegisterSkill(PlayerSkillName.EnergyBall, SkillPoint.position, transform.rotation);
    }

    public void SkillOn2(Transform SkillPoint)
    {
        SkillManager.instance.RegisterSkill(PlayerSkillName.EnergyTornado, SkillPoint.position, transform.rotation) ;
    }

    #endregion

    #region ������ �Լ�
    void Roll(Vector3 dir)
    {
        StartCoroutine(Rolling(dir));
    }

    IEnumerator Rolling(Vector3 dir)
    {
        myAnim.SetBool("isRolling", true);
        while (myAnim.GetBool("isRolling"))
        {
            gameObject.layer = 10; //�÷��̾��� ���̾ ���� ���̾�� �ٲ㼭 ���� �ʵ��� ��.
            transform.position += dir.normalized * Time.deltaTime*1.5f;
            yield return null;
        }
        gameObject.layer = 8;
    }
    #endregion
}
