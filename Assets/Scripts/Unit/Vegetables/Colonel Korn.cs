using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonelKorn : Unit
{
    protected override void Start()
    {


        this.skillList.Add("Pop Corn!");


        this.acc = 10;
        this.spd = 4;
        this.maxhp = 40;
        this.hp = this.maxhp;
        this.atk = 5;
        this.def = 5;

        base.Start();
    }
}
