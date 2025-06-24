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

        this.defaultIcon = Resources.Load<Sprite>("Skills/skillDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/skillHighlighted");
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        PopUpManager.Instance.addPopUp("Dazed!", target.transform);

        target.AddEffect(new Dizzy(3, origin));
    }
}
