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

    protected float cost;
    public float COST
    {
        get { return this.cost; }
        
    }

    public virtual void SkillAction(Unit target, Unit origin)
    {

    }

    //Debuffs/Buffs
    public EffectInfo skillData;
    

    public Skill()
    {

    }
    public Skill(EffectInfo effects,float cost)
    {
        skillData = effects;
        this.cost = cost;    
    }
    public bool CheckCost(float rating)
    {
        if (rating >= cost)
        {
            return true;
        }
        return false;

    }




}