using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPEED : Skill, MultEffect
{
    private string skillName = "SPEED";
    public string SkillName
    {
        get { return this.skillName; }
    }
    private EVeggie veggieType = EVeggie.CARROT;
    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }



    //effectinfo atj
    private int duration1 = 2;
    private int mod1 = -100;
    private EStatToEffect stat1 = EStatToEffect.ATTACK;

    //effectinfo spd
    private int duration2 = 2;
    private int mod2 = 50;
    private EStatToEffect stat2 = EStatToEffect.SPEED;

    private int sucessChance = 90;
    private int x = 0; //bs way to apply to effects








    public void ApplyEffect(Unit target, Unit origin, EffectInfo fInfo)
    {
        switch (x)
        {
            case 0:
                if (target.EFFECTLIST.ContainsKey(this.skillName))
                {
                    target.EFFECTLIST[this.skillName + "attack"].DURATION = duration1;
                }
                else
                {
                    target.EFFECTLIST.Add(this.skillName + "attack", fInfo);
                    Debug.Log("Target affected");
                }
                break;
            case 1:
                if (target.EFFECTLIST.ContainsKey(this.skillName))
                {
                    target.EFFECTLIST[this.skillName + "speed"].DURATION = duration1;
                }
                else
                {
                    target.EFFECTLIST.Add(this.skillName + "speed", fInfo);
                    Debug.Log("Target affected");
                }
                break;
        }


    }
    public void SkillAction(Unit target, Unit origin)
    {
        //atk debuf
        EffectInfo fInfo1 = new EffectInfo(duration1, mod1, stat1);
        //spd buf
        EffectInfo fInfo2 = new EffectInfo(duration2, mod2, stat2);



        Unit appliedTo = target.GetComponent<Unit>();
        if (Random.Range(1, 100) < sucessChance)
        {
            this.ApplyEffect(target, origin, fInfo1);
            x++;
            this.ApplyEffect(target, origin, fInfo2);
        }
        else
        {
            Debug.Log("Fail");
        }
        x = 0;

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
