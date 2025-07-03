using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : Skill
{
    int damage = 5;
    public Harvest()
    {
        this.skillName = "Harvest";
        this.veggieType = EVeggie.PUMPKIN;
        this.skillType = ESkillType.BASIC;

        this.skillRange = 1;

        this.defaultIcon = Resources.Load<Sprite>("Skills/skillDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/skillHighlighted");
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        target.TakeDamage(damage - 2, origin);
        origin.gainHealth(damage, origin);
    }
}
