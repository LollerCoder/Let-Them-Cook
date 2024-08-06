using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Carrot : Unit{

    
    public override void UnitAttack(Unit unit2) {
        
    }

    public override void Selected() {

    }

    public override void GetAttackOptions() {

    }


    private void Start() {

        this.animator = this.GetComponent<Animator>();


        Skill basic = new BasicAttack();
        Skill trueStrike = new TrueStrike();
        Skill fastFood = new FastFood();
        Skill eagle = new EagleEye();


        this.skillList.Add(basic);
        this.skillList.Add(trueStrike);
        this.skillList.Add(fastFood);
        this.skillList.Add(eagle);

        this.charName = "Carrot";
        this.acc = 10;
        this.spd = 2;
        this.maxhp = 20;
        this.hp = this.maxhp;
        this.atk = 3;
        this.def = 1;

        UnitActionManager.Instance.StoreUnit(this);

    }
}
