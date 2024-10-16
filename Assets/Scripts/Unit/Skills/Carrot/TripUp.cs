using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripUp : Skill
{

    private int sucessChance = 80;
    

    public TripUp()
    {
        this.skillName = "TripUp";
        this.veggieType = EVeggie.CARROT;
        this.skillType = ESkillType.BUFFDEBUFF;

        //effectinfo
        int duration = 3;
        int mod = -10;
        EStatToEffect stat = EStatToEffect.SPEED;

        //for skill progressions
        this.cost = 30;

        this.skillData = new EffectInfo(duration, mod, stat);
    }


  








    
    public override void SkillAction(Unit target, Unit origin)
    {
        
        Unit appliedTo = target.GetComponent<Unit>();
        if (Random.Range(1, 100) < sucessChance)
        {
            target.EffectManager.ApplyEffect(target, origin, this.skillName, this.skillData.DURATION);
            target.TakeDamage(0, origin);
        }
        else
        {
            Debug.Log("Fail");
        }

    }

   
}
