using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotten : Skill
{

    public Rotten()
    {
        this.skillName = "Rotten";
        this.veggieType = EVeggie.NONE;
        this.skillType = ESkillType.BASIC;

        this.skillRange = 1;
    }

    public override void  SkillAction(Unit target, Unit origin)
    {
        origin.PrintEffects();
        //Check if skill is being recharged
        RechargingSkill rck = (RechargingSkill)origin.GetEffect("Recharging Skill");
        Debug.Log(origin.GetEffect("Recharging Skill"));
        if (rck != null)
        {
            Debug.Log("Found Effect");
            if (((RechargingSkill)rck).SkillName == "Rotten")
            {
                Debug.Log("Found Rotten");
                return; // exit if yes
            }
        }

        //adding recharge to origin
        Parameters rckParam = new Parameters();
        rckParam.PutExtra("duration", 3);
        rckParam.PutExtra("SkillName", "Rotten");
        origin.AddEffect(EnumEffects.RechargeSkill,
            origin,
            rckParam
            );

        PopUpManager.Instance.addPopUp("Poisoned!", target.transform);

        target.GetComponent<SpriteRenderer>().color =  target.poisonUnit(target.GetComponent<SpriteRenderer>());

        //adding poison to target
        Parameters pParam = new Parameters();
        pParam.PutExtra("duration", 2);
        target.AddEffect(
            EnumEffects.Poison,
            origin,
            pParam
            );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
