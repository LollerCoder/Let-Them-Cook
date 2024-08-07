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



    private int sucessChance = 40;



    public void SkillAction(Unit target, Unit origin)
    {
        if (Random.Range(1, 100) < sucessChance)
        {
            target.TakeDamage(10, origin);
        }
        else
        {
            target.TakeDamage(1, origin);
        }
            

    }

    public string GetName()
    {
        return this.skillName;

    }
    public EVeggie GetVeggie()
    {
        return this.veggieType;

    }
}
