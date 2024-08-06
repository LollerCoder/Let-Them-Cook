using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class TrueStrike : Skill
{

    protected string skillName = "True Strike";
    public string SkillName
    {
        get { return this.skillName; }
    }
    protected EVeggie veggieType = EVeggie.CARROT;
    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }

    public void SkillAction(Unit target, Unit origin)
    {
        

        target.TakeDamage(1000,origin);

    }

    public string GetName()
    {
        return this.skillName;

    }
}
