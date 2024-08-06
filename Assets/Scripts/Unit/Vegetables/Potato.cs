using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : Unit {


    public override void UnitAttack(Unit unit2) {

    }

    public override void Selected() {

    }

    public override void GetAttackOptions() {

    }


    private void Start() {

        this.animator = this.GetComponent<Animator>();

        Skill basic = new BasicAttack();
        Skill FastFood = new FastFood();
        Skill ImFrench = new ImFrench();
        Skill MashPotato = new MashPotato();
        Skill Speed = new SPEED();

        this.skillList.Add(basic);
        this.skillList.Add(FastFood);
        this.skillList.Add(ImFrench);
        this.skillList.Add(MashPotato);
        this.skillList.Add(Speed);

        this.charName = "Potato";
        this.acc = 5;
        this.spd = 2;
        this.maxhp = 15;
        this.hp = this.maxhp;
        this.atk = 3;
        this.def = 5;

        UnitActionManager.Instance.StoreUnit(this);

    }
}
