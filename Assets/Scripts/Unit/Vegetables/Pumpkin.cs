using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Unit
{
    protected override void Start()
    {


        this.skillList.Add("Harvest");
        this.skillList.Add("Photosynthesis");


        this.acc = 10;
        this.spd = 3;
        this.maxhp = 25;
        this.hp = this.maxhp;
        this.atk = 2;
        this.def = 4;

        base.Start();
    }
}
