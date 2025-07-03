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

        this.defaultIcon = Resources.Load<Sprite>("Skills/skillDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/skillHighlighted");
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        origin.gainHealth(3, target);
        target.AddEffect(new AttackBoost(3, target));
    }

}
