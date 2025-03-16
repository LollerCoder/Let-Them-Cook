using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class Photosynthesis : Skill
{
    public Photosynthesis()
    {
        this.skillName = "Photosynthesis";
        this.veggieType = EVeggie.CARROT; 
        this.skillType = ESkillType.HEAL;

        //for skill progressions
        this.cost = 15;
    }

    public override void SkillAction(Unit target, Unit origin)
    {
            origin.gainHealth(10,target);
            PopUpManager.Instance.addPopUp(this.skillName + " 10 HP", target.transform);
    } 
}