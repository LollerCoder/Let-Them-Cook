using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daze : Skill
{
    public Daze() 
    {
        this.skillName = "Daze";
        this.veggieType = EVeggie.GARLIC;
        this.skillType = ESkillType.BASIC;

        this.skillRange = 1;

        SkillDatabase.Instance.GetSkillSprite(this);
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        PopUpManager.Instance.addPopUp("Dazed!", target.transform);

        if (target.GetEffect("Dizzy") != null)
        {
            origin.TakeDamage(3.0f, origin);
        }

        target.AddEffect(new Dizzy(2, target));
        origin.TakeDamage(2.0f, origin);
        //origin.AddEffect(new Poison(3, origin));
    }
}
