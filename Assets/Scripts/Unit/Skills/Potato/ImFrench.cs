using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImFrench : Skill
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
