using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoilStackin : Skill
{
 public FoilStackin()
    {
        this.skillName = "Foil Stackin";
        this.veggieType = EVeggie.GARLIC;
        this.skillType = ESkillType.DEFEND;

        //for skill progressions
        this.cost = 15;
        SkillDatabase.Instance.GetSkillSprite(this);
    }

    public override void SkillAction(Unit target, Unit origin)
    {
            origin.gainDefense(10,target);
            PopUpManager.Instance.addPopUp(this.skillName + " 10 HP", target.transform);
    } 
}
