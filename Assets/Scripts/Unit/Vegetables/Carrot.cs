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

        Skill trueStrike = ScriptableObject.CreateInstance<TrueStrike>();
        Skill basic = ScriptableObject.CreateInstance<BasicAttack>();
        this.skillList.Add(basic);
        this.skillList.Add(trueStrike);

        this.charName = "Carrot";
        this.ingredientType = EIngredientType.CARROT;
        //this.type = EUnitType.Ally;
        this.acc = 10;
        this.spd = 2;
        this.maxhp = 1;
        this.hp = this.maxhp;
        this.atk = 3;
        this.def = 1;

        UnitActionManager.Instance.StoreUnit(this);

    }
}
