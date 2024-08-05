using UnityEngine;
public abstract class Skill :ScriptableObject
{
    protected string skillName = "None";
    protected EVeggie veggieType = EVeggie.NONE;
    public abstract void SkillAction(Unit target, Unit origin);
    
}