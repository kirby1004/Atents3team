using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
public class SkillData : ScriptableObject
{
    [SerializeField]
    protected Sprite _skillImage;
    public Sprite SkillImage { get { return _skillImage; } }
    [SerializeField]
    protected string _skillName;
    public string SkillName { get { return _skillName; } }
    [SerializeField]
    protected float _skillValue1; //Attack인지 Buff 인지에 따라 Attack-> Damage, Buff-> Buff 수치
    public float SkillValue1 { get { return _skillValue1; } }
    [SerializeField]
    protected float _skillValue2; //Attack인지 Buff 인지에 따라 Attack-> CC기 시간, Buff-> Buff 유지 시간
    public float SkillValue2 { get { return _skillValue2; } }
    [SerializeField]
    protected float _skillRange;
    public float SkillRange { get { return _skillRange; } }
    [SerializeField]
    protected float _skillDistance;
    public float SkillDistance { get { return _skillDistance; } }
    [SerializeField]
    protected float _coolTime;
    public float CoolTime { get { return _coolTime; } }
    [SerializeField]
    protected float _skillSpeed;
    public float SkillSpeed { get { return _skillSpeed; } }
    [SerializeField]
    protected CrowndControlType _crowdControlType;
    public CrowndControlType crowdControlType { get { return _crowdControlType; } }
    [SerializeField]
    protected SkillType _skillType;
    public SkillType skillType { get { return _skillType; } }
    [SerializeField]
    protected GameObject _skillEffect;
    public GameObject SkillEffect { get { return _skillEffect; } set { _skillEffect = value; } }

}

public enum SkillType
{
    Attack, Buff
}

public enum CrowndControlType
{
    None,Burn, Freeze, Electricshock
}