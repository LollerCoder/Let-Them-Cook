using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Carrot : Unit{
    public override void UnitAttack(Unit unit2) {
        
    }

    public override void Selected() {

    }

    public override void GetAttackOptions() {

    }

    private void Update() {
         
    }
    
    private void HpBarShow(Parameters param)
    {
        Unit unit = param.GetUnitExtra(UNIT);

        if(unit == this) {
            PopUpManager.Instance.hpPopUp(hpBar, this.maxhp, this.hp);
        }
    }

    private void HpBarHide(Parameters param)
    {

        Unit unit = param.GetUnitExtra(UNIT);

        if (unit == this)
        {
            PopUpManager.Instance.hpHide(this.hpBar);
        }

    }

    protected override void Start() {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.HIDE_HP, this.HpBarHide);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.SHOW_HP, this.HpBarShow);

        this.animator = this.GetComponent<Animator>();
        base.Start();

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
