using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Brocolli : Unit {


    private void Update()
    {

    }

    protected override void Start() {


        this.skillList.Add("Rotten");

        this.acc = 5;
        this.Speed = this.speed;
        this.maxhp = 15;
        this.hp = this.maxhp;
        this.atk = 3;
        this.def = 5;


        //Slider hpSlide = this.hpBar.transform.Find("Slider").GetComponent<Slider>();
        //hpSlide.maxValue = this.maxhp;
        //hpSlide.value = hp;
        //Slider easeSlide = this.hpBar.transform.Find("EaseSlider").GetComponent<Slider>();
        //easeSlide.maxValue = this.maxhp;
        //easeSlide.value = hp;

        base.Start();
    }


}
