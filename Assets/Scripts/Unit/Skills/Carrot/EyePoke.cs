using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePoke : Skill, MultEffect
{
    private string skillName = "EyePoke";
    public string SkillName
    {
        get { return this.skillName; }
    }
    private EVeggie veggieType = EVeggie.CARROT;
    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }



    //effectinfo
    private int duration = 3;
    private int sucessChance = 70;
    private int mod = -10;
    private EStatToEffect stat = EStatToEffect.ACCURACY;








    public void ApplyEffect(Unit target, Unit origin, EffectInfo fInfo)
    {
        if (target.EFFECTLIST.ContainsKey(this.skillName))
        {
            target.EFFECTLIST[this.skillName].DURATION = duration;
        }
        else
        {
            target.EFFECTLIST.Add(this.skillName, fInfo);
            Debug.Log("Target affected");
        }

    }
    public void SkillAction(Unit target, Unit origin)
    {
        EffectInfo fInfo = new EffectInfo(duration, mod, stat);
        Unit appliedTo = target.GetComponent<Unit>();
        if (Random.Range(1, 100) < sucessChance)
        {
            this.ApplyEffect(target, origin, fInfo);
            target.TakeDamage(0, origin);
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
