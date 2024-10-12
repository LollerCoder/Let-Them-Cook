using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.UIElements;
public class Potato : Unit {
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

        if (unit == this) {
            PopUpManager.Instance.hpPopUp(this.hpBar, this.maxhp, this.hp);
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

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.SHOW_HP, this.HpBarShow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.HIDE_HP, this.HpBarHide);
        this.animator = this.GetComponent<Animator>();
        base.Start();


        Skill basic = new BasicAttack();
        Skill fast = new FastFood();
        Skill french = new ImFrench();
        Skill mash = new MashPotato();
        Skill speed = new SPEED();

        this.skillList.Add("Basic Attack");
        this.skillList.Add("Fast Food");
        this.skillList.Add("Im French");
        this.skillList.Add("Mashed Potato");
        this.skillList.Add("SPEED");


        this.charName = "Potato";
        this.acc = 5;
        this.spd = 2;
        this.maxhp = 15;
        this.hp = this.maxhp;
        this.atk = 3;
        this.def = 5;

        UnitActionManager.Instance.StoreUnit(this);

    }
}
