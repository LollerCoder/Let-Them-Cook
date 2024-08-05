using UnityEngine;
public abstract class Skill :ScriptableObject
{
    protected string skillName = "None";
    private EVeggie veggie = EVeggie.NONE;
    public abstract void SkillAction();
    
}