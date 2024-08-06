using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Give carrot a 10% accuracy buff
public class EagleEye : Skill, MultEffect
{
    private string skillName = "EagleEye";
    public string SkillName
    {
        get { return this.skillName; }
    }
    private  EVeggie veggieType = EVeggie.CARROT;
    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }

    

    //effectinfo
    private int duration = 3;
    private int sucessChance = 80;
    private int mod = 10;
    private EStatToEffect stat = EStatToEffect.ACCURACY;






    

    public  void ApplyEffect(Unit target, Unit origin, EffectInfo fInfo)
    {
       target.EFFECTLIST.Add(this.skillName, fInfo);
        Debug.Log("Target affected");
    }    
    public void SkillAction(Unit target, Unit origin)
    {
        EffectInfo fInfo = new EffectInfo(duration, mod, stat);
        Unit appliedTo = target.GetComponent<Unit>();
        if(Random.Range(1,100) < sucessChance)
        {
            this.ApplyEffect(target, origin, fInfo);
        }
        else
        {
            Debug.Log("Fail");
        }

    }

  

}
