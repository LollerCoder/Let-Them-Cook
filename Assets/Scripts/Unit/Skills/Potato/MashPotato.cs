using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MashPotato : Skill
{
    


    private EffectInfo skillData2;

    

    private int sucessChance = 90;
    private int x = 0; //bs way to apply to effects


    public MashPotato()
    {

        this.skillName = "Mashed Potato";
        this.veggieType = EVeggie.POTATO;
        this.skillType = ESkillType.BUFFDEBUFF;

        
        //effectinfo def 
        int duration1 = 3;
        int mod1 = -20;
        EStatToEffect stat1 = EStatToEffect.DEFENSE;

        this.skillData = new EffectInfo(duration1, mod1, stat1);

        //effectinfo atk buff
        int duration2 = 3;
        int mod2 = 20;
        EStatToEffect stat2 = EStatToEffect.ATTACK;

        
        this.skillData2 = new EffectInfo(duration2, mod2, stat2);
    }





   
    public override void SkillAction(Unit target, Unit origin)
    {
        



        Unit appliedTo = target.GetComponent<Unit>();
        if (Random.Range(1, 100) < sucessChance)
        {
           
            target.EffectManager.ApplyEffect(target, origin, this.skillName + "defense", this.skillData.DURATION);
            x++;
            
            target.EffectManager.ApplyEffect(target, origin, this.skillName + "attack", this.skillData2.DURATION);
        }
        else
        {
            Debug.Log("Fail");
        }
        x = 0;

    }

    
}
