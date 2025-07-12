using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoilThrow : Skill
{
    private int damage = 5;

    public FoilThrow()
    {
        this.skillName = "Foil Throw";
        this.veggieType = EVeggie.GARLIC;
        this.skillType = ESkillType.RANGE;

        this.skillRange = 3;

        SkillDatabase.Instance.GetSkillSprite(this);
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        PopUpManager.Instance.addPopUp("-" + damage, target.transform);

        target.TakeDamage(damage, origin);
    }
}
