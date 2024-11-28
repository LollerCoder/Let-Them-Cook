using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFood : Skill
{



    public FastFood()
    {

        this.skillName = "Fast Food";
        this.veggieType = EVeggie.POTATO;
        this.skillType = ESkillType.BASIC;

        //for skill progressions
        this.cost = 10;



    }

    public override void SkillAction(Unit target, Unit origin)
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
