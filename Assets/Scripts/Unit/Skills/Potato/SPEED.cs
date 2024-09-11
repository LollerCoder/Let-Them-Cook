using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SPEED : Skill, IEffectable
{

    private EffectInfo skillData2;

    public SPEED()
    {
        this.skillName = "SPEED";
        //effectinfo atk
        int duration1 = 2;
        int mod1 = -100;
        EStatToEffect stat1 = EStatToEffect.ATTACK;

        this.skillData = new EffectInfo(duration1, mod1, stat1);

        //effectinfo spd
        int duration2 = 2;
        int mod2 = 50;
        EStatToEffect stat2 = EStatToEffect.SPEED;

        this.skillData2 = new EffectInfo(duration2, mod2, stat2);
    }



   

    private int sucessChance = 90;
    private int x = 0; //bs way to apply to effects








    public void ApplyEffect(Unit target, Unit origin, EffectInfo fInfo)
    {
        switch (x)
        {
            case 0:
                if (target.EffectManager.EFFECTLIST.ContainsKey(this.skillName))
                {
                    target.EffectManager.EFFECTLIST[this.skillName + "attack"].DURATION = this.skillData.DURATION;
                }
                else
                {
                    target.EffectManager.EFFECTLIST.Add(this.skillName + "attack", fInfo);
                    Debug.Log("Target affected");
                }
                break;
            case 1:
                if (target.EffectManager.EFFECTLIST.ContainsKey(this.skillName))
                {
                    target.EffectManager.EFFECTLIST[this.skillName + "speed"].DURATION = this.skillData2.DURATION;
                }
                else
                {
                    target.EffectManager.EFFECTLIST.Add(this.skillName + "speed", fInfo);
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
            this.ApplyEffect(target, origin, skillData);
            x++;
            this.ApplyEffect(target, origin, skillData2);
        }
        else
        {
            Debug.Log("Fail");
        }
        x = 0;

    }

    
}
