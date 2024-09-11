using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MashPotato : Skill, IEffectable
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





    public void ApplyEffect(Unit target, Unit origin, EffectInfo fInfo)
    {
        switch (x)
        {
            case 0:
                if (target.EffectManager.EFFECTLIST.ContainsKey(this.skillName))
                {
                    target.EffectManager.EFFECTLIST[this.skillName + "defense"].DURATION = this.skillData.DURATION;
                }
                else
                {
                    target.EffectManager.EFFECTLIST.Add(this.skillName + "defense", fInfo);
                    Debug.Log("Target affected");
                }
                break;
            case 1:
                if (target.EffectManager.EFFECTLIST.ContainsKey(this.skillName))
                {
                    target.EffectManager.EFFECTLIST[this.skillName + "attack"].DURATION = this.skillData2.DURATION;
                }
                else
                {
                    target.EffectManager.EFFECTLIST.Add(this.skillName + "attack", fInfo);
                    Debug.Log("Target affected");
                }
                break;
        }
        

    }
    public override void SkillAction(Unit target, Unit origin)
    {
        



        Unit appliedTo = target.GetComponent<Unit>();
        if (Random.Range(1, 100) < sucessChance)
        {
            this.ApplyEffect(target, origin, this.skillData);
            x++;
            this.ApplyEffect(target, origin, this.skillData2);
        }
        else
        {
            Debug.Log("Fail");
        }
        x = 0;

    }

    
}
