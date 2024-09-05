using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Give carrot a 10% accuracy buff
public class EagleEye : Skill, MultEffect
{


    EffectInfo skillData; 

    //effectinfo
   
    private int sucessChance = 80;
    
   

    public EagleEye()
    {

        this.skillName = "EagleEye";
        this.veggieType = EVeggie.CARROT;
        this.skillType = ESkillType.BUFFDEBUFF;

        //EFFECT INFO
        int duration = 3;
        EStatToEffect stat = EStatToEffect.ACCURACY;
        int mod = 10;

        skillData = new EffectInfo(duration, mod, stat);

    }




    

    public  void ApplyEffect(Unit target, Unit origin, EffectInfo fInfo)
    {
        if (target.EFFECTLIST.ContainsKey(this.skillName))
        {
            target.EFFECTLIST[this.skillName].DURATION = this.skillData.DURATION;
        }
        else
        {
            target.EFFECTLIST.Add(this.skillName, fInfo);
            Debug.Log("Target affected");
        }
       
    }    


    public override void SkillAction(Unit target, Unit origin)
    {
        
        Unit appliedTo = target.GetComponent<Unit>();
        if(Random.Range(1,100) < 1000/*sucessChance*/)
        {
            this.ApplyEffect(target, origin, this.skillData);
        }
        else
        {
            Debug.Log("Fail");
        }

    }

   



}
