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
        if (target.GetEffect("Attack Boost") != null) return;

        target.AddEffect(new AttackBoost(1, target));
        target.gainHealth(3, origin);

    }

}
