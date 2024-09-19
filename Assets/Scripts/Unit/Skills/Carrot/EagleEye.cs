using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Give carrot a 10% accuracy buff
public class EagleEye : Skill
{


    

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

        this.skillData = new EffectInfo(duration, mod, stat);

    }




    

       


    public override void SkillAction(Unit target, Unit origin)
    {
        
        Unit appliedTo = target.GetComponent<Unit>();
        if(Random.Range(1,100) < sucessChance+1000)
        {
            target.EffectManager.ApplyEffect(target, origin, this.skillName, this.skillData.DURATION);
            
        }
        else
        {
            Debug.Log("Fail");
        }

    }

   



}
