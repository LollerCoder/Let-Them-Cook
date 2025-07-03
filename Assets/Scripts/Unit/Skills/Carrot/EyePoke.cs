using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePoke : Skill
{
    private int sucessChance = 70;
   

    public EyePoke()
    {
        this.skillName = "EyePoke";
        this.veggieType = EVeggie.CARROT;
        this.skillType = ESkillType.BUFFDEBUFF;

        //effectinfo
        int duration = 3;
        int mod = -10;
        EStatToEffect stat = EStatToEffect.ACCURACY;

        //for skill progressions
        this.cost = 50;

        this.skillData = new EffectInfo(duration, mod, stat);

        this.defaultIcon = Resources.Load<Sprite>("Skills/skillDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/skillHighlighted");
    }


    








    
    public override void SkillAction(Unit target, Unit origin)
    {
        
        Unit appliedTo = target.GetComponent<Unit>();
        if (Random.Range(1, 100) < sucessChance)
        {
            target.EffectManager.ApplyEffect(target, origin, this.skillName, this.skillData.DURATION);
            target.TakeDamage(0, origin);
            PopUpManager.Instance.addPopUp(this.skillName, target.transform);


            target.EffectManager.ArrowShower(target);

        }
        else
        {
            PopUpManager.Instance.addPopUp("MISS", target.transform);
            Debug.Log("Fail");
        }

    }

    
}
