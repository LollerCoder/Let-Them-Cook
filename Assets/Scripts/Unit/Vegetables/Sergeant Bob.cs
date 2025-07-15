using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SergeantBob : Unit
{
    protected override void Start()
    {

        
        this.skillList.Add("Shove");
        this.skillList.Add("Yell");


        this.acc = 10;
        this.Speed = this.speed;
        this.maxhp = 25;
        this.hp = this.maxhp;
        this.atk = 2;
        this.def = 4;

        base.Start();
    }
}
