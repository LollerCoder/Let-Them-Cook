using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
 
public class BasicAttack : Skill
{

    public BasicAttack()
    {
        this.skillName = "Basic Attack";
        this.veggieType = EVeggie.NONE;
        this.skillType = ESkillType.BASIC;
    }

    public override void  SkillAction(Unit target, Unit origin)
    {
        target.TakeDamage(0, origin);
    }

    
}
