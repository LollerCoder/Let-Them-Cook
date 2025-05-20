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
    }

       public override void  SkillAction(Unit target, Unit origin)
    {
       target.GetComponent<SpriteRenderer>().color =  target.poisonUnit(target.GetComponent<SpriteRenderer>());
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
