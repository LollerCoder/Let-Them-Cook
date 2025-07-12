using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yell : Skill
{
    public Yell()
    {
        this.skillName = "Yell";
        this.veggieType = EVeggie.NONE;
        this.skillType = ESkillType.HEAL;

        this.skillRange = 4;

        SkillDatabase.Instance.GetSkillSprite(this);
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        origin.gainHealth(3, target);
        target.AddEffect(new AttackBoost(3, target));
    }

}
