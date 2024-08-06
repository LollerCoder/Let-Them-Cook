using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
 
public class BasicAttack : Skill
{

    public string skillName = "Basic Attack";
    private EVeggie veggieType = EVeggie.NONE;

    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }


    void Start()
    {

    }
    public void  SkillAction(Unit target, Unit origin)
    {
      
        target.TakeDamage(target.Attack, origin);
    }   
}
