using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonelKorn : Unit
{
    protected override void Start()
    {


        this.skillList.Add("Pop Corn!");


        this.acc = 10;
        this.Speed = this.speed;
        this.maxhp = 45;
        this.hp = this.maxhp;
        this.atk = 6;
        this.def = 5;

        base.Start();
    }
}
