using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{

    private Dictionary<string, EffectInfo> effectList; 
    public Dictionary<string, EffectInfo> EFFECTLIST
    {
        get { return this.effectList; }
    }


    public EffectManager()
    {
        effectList = new Dictionary<string, EffectInfo>();
    }


    public void EffectAccess(Unit applyTo)
    {
        Debug.Log("In EffectAccess");
        
        foreach (string key in applyTo.EffectManager.effectList.Keys)
        {
        
            float augment = applyTo.EffectManager.effectList[key].MOD;
            EStatToEffect stat = applyTo.EffectManager.effectList[key].STAT;
            //Debug.Log(applyTo.EFFECTLIST[key].STAT);
            switch (stat)
            {
                case EStatToEffect.ACCURACY:
                    applyTo.AccuracyMultiplier += augment / 100;
                    //apply
                    applyTo.Accuracy *= applyTo.AccuracyMultiplier;
                    
                    break;
                case EStatToEffect.SPEED:
                    applyTo.SpeedMultiplier += augment / 100;
                    //apply
                    applyTo.Speed *= applyTo.SpeedMultiplier;
                    break;
                case EStatToEffect.DEFENSE:
                    applyTo.DefenseMultiplier += augment / 100;
                    //apply
                    applyTo.Defense *= applyTo.DefenseMultiplier;
                    break;
                case EStatToEffect.ATTACK:
                    applyTo.AttackMultiplier += augment / 100;
                    //apply
                    applyTo.Attack *= applyTo.AttackMultiplier;
                    break;
                default:
                    Debug.Log("Invalid");

                    break;

            }
        }
    }

    public void EffectTimer()
    {
        List<string> toDelete = new List<string>();
        if(EFFECTLIST.Count != 0)
        {
            foreach (string key in EFFECTLIST.Keys)
            {


                EFFECTLIST[key].DURATION -= 1;
                Debug.Log("Effect " + key + " has " + EFFECTLIST[key].DURATION + " left");

                if (EFFECTLIST[key].DURATION == 0)
                {
                    toDelete.Add(key);

                }
            }
        }
        foreach(string keyDelete in toDelete)
        {
            EFFECTLIST.Remove(keyDelete);
        }
       
        
    }
    public void EffectReset(Unit applyTo)
    {
       

        applyTo.Accuracy /= applyTo.AccuracyMultiplier;
        applyTo.AccuracyMultiplier = 1;

        applyTo.Defense /= applyTo.DefenseMultiplier;
        applyTo.DefenseMultiplier = 1;

        applyTo.Attack /= applyTo.AttackMultiplier;
        applyTo.AttackMultiplier = 1;

        applyTo.Speed /= applyTo.SpeedMultiplier;
        applyTo.SpeedMultiplier = 1;


       
    }
}
