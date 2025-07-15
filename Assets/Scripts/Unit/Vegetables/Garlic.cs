using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Garlic : Unit{

    private void Update() {
         
    }


    protected override void Start() {


        this.skillList.Add("Foil Throw");
        this.skillList.Add("Daze");


        this.acc = 10;
        this.Speed = this.speed;
        this.maxhp = 15;
        this.hp = this.maxhp;
        this.atk = 4;
        this.def = 1;

        //UnitActionManager.Instance.StoreUnit(this);
        //this.charName = "Carrot";
        //this.ingredientType = EIngredientType.CARROT;
        //this.type = EUnitType.Ally;
        base.Start();
    }
}
