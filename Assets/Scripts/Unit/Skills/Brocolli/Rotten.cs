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
        //origin.PrintEffects();
        //Check if skill is being recharged
        RechargingSkill rck = (RechargingSkill)origin.GetEffect("Recharging Skill");
        //Debug.Log(origin.GetEffect("Recharging Skill"));
        if (rck != null)
        {
            //Debug.Log("Found Effect");
            if (((RechargingSkill)rck).SkillName == "Rotten")
            {
                //Debug.Log("Found Rotten");
                return; // exit if yes
            }
        }

        //adding recharging skill
        origin.AddEffect(new RechargingSkill(3, origin, "Rotten"));

        PopUpManager.Instance.addPopUp("Poisoned!", target.transform);

        //target.GetComponent<SpriteRenderer>().color =  target.poisonUnit(target.GetComponent<SpriteRenderer>());

        //adding poison to target
        target.ChangeColor(Color.green);
        target.AddEffect(new Poison(2, origin));
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
