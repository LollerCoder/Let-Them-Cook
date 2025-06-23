using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BasicAttack : Skill
{
    int damage = 3;

    public BasicAttack()
    {
        this.skillName = "Basic Attack";
        this.veggieType = EVeggie.NONE;
        this.skillType = ESkillType.BASIC;

        this.skillRange = 1;

        this.defaultIcon = Resources.Load<Sprite>("Skills/basicAttackDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/basicAttackHighlighted");
    }

    public override void  SkillAction(Unit target, Unit origin)
    {
        target.TakeDamage(damage, origin);
        PopUpManager.Instance.addPopUp("-" + damage, target.transform);
    }

    
}
