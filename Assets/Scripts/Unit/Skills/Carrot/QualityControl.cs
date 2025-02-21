using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class QualityControl : Skill
{
    public QualityControl()
    {
        this.skillName = "QualityControl";
        this.veggieType = EVeggie.CARROT; 
        this.skillType = ESkillType.HEAL;

        //for skill progressions
        this.cost = 15;
    }



    public override void SkillAction(Unit target, Unit origin)
    {
            target.gainHealth(10,origin);
            //target.TakeDamage(-10, origin);
            PopUpManager.Instance.addPopUp(this.skillName + " 10 HP", target.transform);
        
            

    } 
}