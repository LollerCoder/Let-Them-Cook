using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager
{
    /*
     * TODO
     * 
     * YOU MADE A SKILLDATABASE BUT DIDNT USE IT HERE YOU DUMBO
     * IT WAS SUPPOSED MAKE SKILLS NOT TAKE SPACE BUT NOW IT IS HORRIBLY.
     * 
     */
    private Dictionary<string, int> effectList; 
    public Dictionary<string, int> EFFECTLIST
    {
        get { return this.effectList; }
        set { this.effectList = value; }
    }


    public EffectManager()
    {
        effectList = new Dictionary<string, int>();
    }
    public void EffectAccess(Unit applyTo)
    {
        Debug.Log("In EffectAccess");
        
        foreach (string key in applyTo.EffectManager.effectList.Keys)
        {
            Skill foundSkill = SkillDatabase.Instance.findSkill(key);

            if (foundSkill == null)
            {
                float augment = foundSkill.skillData.MOD;
                EStatToEffect stat = foundSkill.skillData.STAT;
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
                        Debug.Log("Invalid Effect");
                        break;

                }
            }


           
        }
    }

    public void EffectTimer()
    {
        List<string> toDelete = new List<string>();
        List<string> toIterateThrough;
        if(EFFECTLIST.Count != 0)
        {
            toIterateThrough = new List<string>(EFFECTLIST.Keys);
            foreach (string key in toIterateThrough)
            {
                EFFECTLIST[key] -= 1;
                Debug.Log("Effect " + key + " has " + EFFECTLIST[key] + " left");

                if (EFFECTLIST[key] <= 0)
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


    public void ApplyEffect(Unit target, Unit origin, string skillName, int duration)
        {

        if (target.EffectManager.EFFECTLIST.ContainsKey(skillName))
        {
            target.EffectManager.EFFECTLIST[skillName] = duration;
            Debug.Log("Should have been refreshed dur: " + target.EffectManager.EFFECTLIST[skillName]);
        }
        else
        {
            target.EffectManager.EFFECTLIST.Add(skillName, duration);
            Debug.Log("Target affected");
        }

    }

    public void ApplyTileEffect(Unit target, string tileName, int duration)
    {
        if (target.EffectManager.EFFECTLIST.ContainsKey(tileName))
        {
            target.EffectManager.EFFECTLIST[tileName] = duration;
            Debug.Log("Should have been refreshed dur: " + target.EffectManager.EFFECTLIST[tileName]);
        }
        else
        {
            target.EffectManager.EFFECTLIST.Add(tileName, duration);
            Debug.Log("Target affected");
        }
    }
}

