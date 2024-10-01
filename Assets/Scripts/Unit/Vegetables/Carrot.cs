using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Carrot : Unit{
    [SerializeField]
    private Sprite carrot;

    public bool ondefend = false;

    [SerializeField]
    public GameObject hpBar;
    
    public override void UnitAttack(Unit unit2) {
        
    }

    public override void Selected() {

    }

    public override void GetAttackOptions() {

    }

    private void Update() {
        this.ondefend = this.defend;
        hpBar.GetComponentInChildren<Slider>().maxValue = this.maxhp;
        hpBar.GetComponentInChildren<Slider>().value = this.hp;
    }

    protected override void HandleDeath() {
        this.GetComponent<SpriteRenderer>().sprite = carrot;
        this.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
        this.eatable = true;
    }
    protected override void Start() { 

        this.animator = this.GetComponent<Animator>();
        base.Start();

        this.ondefend = this.defend;

        Skill basic = new BasicAttack();
        Skill trueStrike = new TrueStrike();
        Skill eyePoke = new EyePoke();
        Skill tripUp = new TripUp();
        Skill eagle = new EagleEye();

        this.skillList.Add("Basic Attack");
        this.skillList.Add("TrueStrike");
        this.skillList.Add("EyePoke");
        this.skillList.Add("TripUp");
        this.skillList.Add("EagleEye");

        

        this.charName = "Carrot";
        this.ingredientType = EIngredientType.CARROT;
        //this.type = EUnitType.Ally;
        this.acc = 10;
        this.spd = 3;
        this.maxhp = 20;
        this.hp = this.maxhp;
        this.atk = 4;
        this.def = 1;

        UnitActionManager.Instance.StoreUnit(this);

        
    }
}
