using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTile : Tile {

    public new void Start() {
        base.Start();
        this.tileType = ETileType.HEAL;
    }
    public override void ApplyEffect(Unit _unit) {
        _unit.Heal();
        Debug.Log("ONHEAL");
    }

}
