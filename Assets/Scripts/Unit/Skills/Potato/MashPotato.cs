using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MashPotato : MonoBehaviour
{
    Skill, MultEffect
{
    private string skillName = "Mashed Potato";
    public string SkillName
    {
        get { return this.skillName; }
    }
    private EVeggie veggieType = EVeggie.CARROT;
    public EVeggie VEGGIETYPE
    {
        get { return this.veggieType; }
    }



    //effectinfo def 
    private int duration1 = 3;
    private int mod1 = -20;
    private EStatToEffect stat1 = EStatToEffect.DEFENSE;

    //effectinfo atk buff
    private int duration2 = 3;
    private int mod2 = 20;
    private EStatToEffect stat2 = EStatToEffect.ATTACK;

    private int sucessChance = 90;
    private int x = 0; //bs way to apply to effects








    public void ApplyEffect(Unit target, Unit origin, EffectInfo fInfo)
    {
        switch (x)
        {
            case 0:
                if (target.EFFECTLIST.ContainsKey(this.skillName))
                {
                    target.EFFECTLIST[this.skillName].DURATION = duration1;
                }
                else
                {
                    target.EFFECTLIST.Add(this.skillName, fInfo);
                    Debug.Log("Target affected");
                }
                break;
        }
        

    }
    public void SkillAction(Unit target, Unit origin)
    {
        //Def deBUF
        EffectInfo fInfo1 = new EffectInfo(duration1, mod1, stat1);
        //ATK BUF
        EffectInfo fInfo2 = new EffectInfo(duration2, mod2, stat2);



        Unit appliedTo = target.GetComponent<Unit>();
        if (Random.Range(1, 100) < sucessChance)
        {
            this.ApplyEffect(target, origin, fInfo1);
            this.ApplyEffect(target, origin, fInfo2);
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
