using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImFrench : Skill, IEffectable
{
    
    
    


    //effectinfo
   
    private int sucessChance = 60;
    

    public ImFrench()
    {

        this.skillName = "Im French";
        this.veggieType = EVeggie.POTATO;
        this.skillType = ESkillType.BUFFDEBUFF;

        //EFFECT INFO
        int duration = 3;
        EStatToEffect stat = EStatToEffect.SPEED;
        int mod = 20;

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
        }
        else
        {
            Debug.Log("Fail");
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
