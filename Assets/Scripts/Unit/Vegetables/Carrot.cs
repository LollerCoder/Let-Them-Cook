using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Carrot : Unit{
    public string vegName = "Carrot";

    public override void Attack(Unit unit2) {
        
    }
    private void EndTurn() {
        this.OnUnitTurnEnd();
    }
    private void Start() {

        this.Range = 1;
        this.SPD = 2;
        this.MAXHP = 50;
        this.HP = this.MAXHP;
        this.ATK = 10;
        this.DEF = 5;

        UnitActionManager.Instance.StoreUnit(this);
        EventBroadcaster.Instance.AddObserver(EventNames.UnitActionEvents.ON_UNIT_TURN_END, this.EndTurn);
    }
}
