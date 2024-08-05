using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Carrot : Unit{
    public override void UnitAttack(Unit unit2) {
        
    }
    private void EndTurn() {
        this.OnUnitTurnEnd();
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
        EventBroadcaster.Instance.AddObserver(EventNames.UnitActionEvents.ON_UNIT_TURN_END, this.EndTurn);
    }
}
