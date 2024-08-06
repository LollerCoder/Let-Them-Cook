using UnityEngine;
public interface Skill 
{
    
    public void SkillAction(Unit target, Unit origin);

    public string GetName();
    
}