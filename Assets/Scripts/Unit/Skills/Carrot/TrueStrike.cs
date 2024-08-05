using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueStrike : Skill
{
    // Start is called before the first frame update
    void Start()
    {
        this.skillName = "True Strike";
        this.veggieType = EVeggie.CARROT;
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        

        target.TakeDamage(1000,origin);

    }
}
