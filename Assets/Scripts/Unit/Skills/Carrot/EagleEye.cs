using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Give carrot a 10% accuracy buff
public class EagleEye : Skill
{


    

    //effectinfo
   
    private int sucessChance = 80;

    

    public EagleEye()
    {

        this.skillName = "EagleEye";
        this.veggieType = EVeggie.CARROT;
        this.skillType = ESkillType.BUFFDEBUFF;


        //EFFECT INFO
        int duration = 3;
        EStatToEffect stat = EStatToEffect.ACCURACY;
        int mod = 10;

        //for skill progressions
        this.cost = 50;


        this.skillData = new EffectInfo(duration, mod, stat);

        this.defaultIcon = Resources.Load<Sprite>("Skills/skillDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/skillHighlighted");

    }




    

       


    public override void SkillAction(Unit target, Unit origin)
    {
        
        Unit appliedTo = target.GetComponent<Unit>();
        if(Random.Range(1,100) < sucessChance)
        {
            target.EffectManager.ApplyEffect(target, origin, this.skillName, this.skillData.DURATION);
            PopUpManager.Instance.addPopUp(this.skillName, target.transform);
            target.EffectManager.ArrowShower(target);

        }
        else
        {
                   
            PopUpManager.Instance.addPopUp("MISS", target.transform);
            
        }

    }

   



}
