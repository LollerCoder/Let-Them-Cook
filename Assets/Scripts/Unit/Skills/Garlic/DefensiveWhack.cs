using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveWhack : Skill
{
    public DefensiveWhack()
    {
        this.skillName = "Defensive Whack";
        this.veggieType = EVeggie.GARLIC; 
        this.skillType = ESkillType.BASIC;

        //for skill progressions
        this.cost = 15;

        this.defaultIcon = Resources.Load<Sprite>("Skills/skillDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/skillHighlighted");
    }

    public override void SkillAction(Unit target, Unit origin)
    {
            origin.TakeDamage(10,target);
            origin.gainDefense(10,target);
            PopUpManager.Instance.addPopUp(this.skillName + " 10 HP", target.transform);
    } 
}
