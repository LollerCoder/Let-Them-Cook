using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripUp : Skill, IEffectable
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

        this.skillData = new EffectInfo(duration, mod, stat);
    }


  








    public void ApplyEffect(Unit target, Unit origin, EffectInfo fInfo)
    {
        if (target.EffectManager.EFFECTLIST.ContainsKey(this.skillName))
        {
            target.EffectManager.EFFECTLIST[this.skillName].DURATION = this.skillData.DURATION;
        }
        else
        {
            target.EffectManager.EFFECTLIST.Add(this.skillName, fInfo);
            Debug.Log("Target affected");
        }

    }
    public override void SkillAction(Unit target, Unit origin)
    {
        
        Unit appliedTo = target.GetComponent<Unit>();
        if (Random.Range(1, 100) < sucessChance)
        {
            this.ApplyEffect(target, origin, this.skillData);
            target.TakeDamage(0, origin);
        }
        else
        {
            Debug.Log("Fail");
        }

    }

   
}
