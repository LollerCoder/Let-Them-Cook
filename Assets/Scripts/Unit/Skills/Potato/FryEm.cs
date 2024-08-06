using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Makes Potato Attack thrice
/// </summary>

public class FryEm : Skill  
{
    private string skillName = "Fry Em";
    public string SkillName
    {
        get { return this.skillName; }
    }
    private EVeggie veggieType = EVeggie.POTATO;

    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }
    public  void SkillAction(Unit target, Unit origin)
    {

    }
    public string GetName()
    {
        return this.skillName;

    }
}
