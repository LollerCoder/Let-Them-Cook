using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class TrueStrike : Skill
{


    private int sucessChance = 20;

    public TrueStrike()
    {
        this.skillName = "TrueStrike";
        this.veggieType = EVeggie.CARROT; 
        this.skillType = ESkillType.BASIC;
        //for skill progressions
        this.cost = 30;
    }



    public override void SkillAction(Unit target, Unit origin)
    {
        if (Random.Range(1, 100) < sucessChance)
        {
            target.TakeDamage(10, origin);
            PopUpManager.Instance.addPopUp(this.skillName + " 10 DMG", target.transform);
        }
        else
        {
            PopUpManager.Instance.addPopUp(this.skillName + " 1 DMG", target.transform);
            target.TakeDamage(1, origin);
        }
            

    }

   
}
 