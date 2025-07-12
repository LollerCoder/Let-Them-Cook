using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : Unit
{

    protected override void Start()
    {

        this.skillList.Add("Basic Attack");
        this.skillList.Add("Circular Cut");

        this.acc = 10;
        this.Speed = 15;
        this.maxhp = 20;
        this.hp = this.maxhp;
        this.atk = 2;
        this.def = 1;


        //UnitActionManager.Instance.StoreUnit(this);
        //this.charName = "Carrot";
        //this.ingredientType = EIngredientType.CARROT;
        //this.type = EUnitType.Ally;
        base.Start();
    }
}
