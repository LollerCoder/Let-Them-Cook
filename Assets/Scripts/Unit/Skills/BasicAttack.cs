using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BasicAttack : Skill
{
    int damage = 3;

    public BasicAttack()
    {
        this.skillName = "Basic Attack";
        this.veggieType = EVeggie.NONE;
        this.skillType = ESkillType.BASIC;

        this.skillRange = 1;

        SkillDatabase.Instance.GetSkillSprite(this);
    }

    public override void  SkillAction(Unit target, Unit origin)
    {
        target.TakeDamage(damage, origin);
        PopUpManager.Instance.addPopUp("-" + damage, target.transform);
    }

    
}
