using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Carrot : Unit{
    public override void OnMouseEnter() {
        UnitActionManager.Instance.UnitHover(this);
    }

    public override void OnMouseExit() {
        UnitActionManager.Instance.UnitHover(this);
    }

    public override void OnMouseUp() {
        UnitActionManager.Instance.UnitSelect(this);
    }

    public override void UnitAttack(Unit unit2) {
        
    }
    private void Start() {
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
