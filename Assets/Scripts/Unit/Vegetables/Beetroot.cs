using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetroot : Unit
{

    protected override void Start()
    {
        this.skillList.Add("Circular Cut");

        this.acc = 5;
        this.spd = 2;
        this.maxhp = 15;
        this.hp = this.maxhp;
        this.atk = 3;
        this.def = 5;

        base.Start();
    }
}
