using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class BasicAttack : Skill
{
    void Start()
    {
        this.skillName = "Basic Attack";
        this.veggieType = EVeggie.NONE;
    }
    public override void  SkillAction(Unit target, Unit origin)
    {
        target.TakeDamage(target.Attack, origin);
    }   
}
