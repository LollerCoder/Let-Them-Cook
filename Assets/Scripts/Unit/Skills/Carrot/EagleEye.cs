using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Give carrot a 10% accuracy buff
public class EagleEye : Skill
{



    void Start()
    {
        this.skillName = "EagleEye";
        this.veggieType = EVeggie.CARROT;
    }
    public override void SkillAction(Unit target, Unit origin)
    {
        Unit appliedTo = target.GetComponent<Unit>();

     
    }
}
