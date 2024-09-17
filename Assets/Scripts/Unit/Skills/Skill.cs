using UnityEngine;
public class Skill 
{
    protected string skillName;
    public string SkillName
    {
        get { return this.skillName; }
    }



    protected EVeggie veggieType;
    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }

    protected ESkillType skillType;

    public ESkillType SKILLTYPE
    {
        get { return this.skillType; }
    }

    public virtual void SkillAction(Unit target, Unit origin)
    {

    }

    //Debuffs/Buffs
    public EffectInfo skillData;



}