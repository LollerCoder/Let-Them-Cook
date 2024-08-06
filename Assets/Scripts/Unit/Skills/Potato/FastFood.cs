using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFood : Skill
{
    protected string skillName = "Fast Food";

    public string SkillName
    {
        get { return this.skillName; }
    }
    protected EVeggie veggieType = EVeggie.CARROT;
    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }


    public void SkillAction(Unit target, Unit origin)
    {
        
        for(int i = 0; i < 3; i++)
        {
            
            target.TakeDamage(0, origin);
        }
        

    }
    public string GetName()
    {
        return this.skillName;

    }

    public EVeggie GetVeggie()
    {
        return this.veggieType;

    }
}
