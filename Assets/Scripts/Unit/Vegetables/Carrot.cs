using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Carrot : Unit{

    [SerializeField]
    Animator BufController;

    [SerializeField]
    Animator DebufController;
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

        if (this.Type == EUnitType.Enemy)//enemy
        {
            this.hpBar.transform.Find("Slider").GetComponentInChildren<Image>().color = new Color(0.8941177f, 0, 0.05098039f, 1);

        }

        if (this.Type == EUnitType.Ally)//ally
        {
            this.hpBar.transform.Find("Slider").GetComponentInChildren<Image>().color = new Color(0.0619223f, 0.2870282f, 0.8415094f, 1);
        }

        if (UnitActionManager.Instance.UnitOrder[0] == this && this.Type != EUnitType.Enemy)//its you
        {
            this.hpBar.transform.Find("Slider").GetComponentInChildren<Image>().color = new Color(0.2638531f, 0.8943396f, 0.2008044f, 1);
        }

        if (unit == this) {
            this.hpBar.GetComponentInChildren<HpBar>().hpPopUp(this.hpBar, this.maxhp, this.hp);
        }
    }

    private void HpBarHide(Parameters param)
    {
        Unit unit = param.GetUnitExtra(UNIT);



        if (unit == this)
        {
            this.hpBar.GetComponentInChildren<HpBar>().hpHide(this.hpBar);
        }

    }

    private void BuffArrowShow(Parameters param)
    {   
        if (this == param.GetUnitExtra("UNIT"))
        {
            this.BufController.SetBool("isBuffed", true);
            //Debug.Log("Buffed");
        }

    }

    private void DebuffArrowShow(Parameters param)
    {
        if (this == param.GetUnitExtra("UNIT"))
        {
            this.DebufController.SetBool("isDebuffed", true);
           // Debug.Log("Debuffed");
        }
    }

    private void BuffArrowHide(Parameters param)
    {
        if (this == param.GetUnitExtra("UNIT"))
        {
            this.BufController.SetBool("isBuffed", false);
            //Debug.Log("buffGone");
        }

    }

    private void DebuffArrowHide(Parameters param)
    {
        if (this == param.GetUnitExtra("UNIT"))
        {
            this.DebufController.SetBool("isDebuffed", false);
           // Debug.Log("DebuffedGone");
        }
    }



    protected override void Start() {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.HIDE_HP, this.HpBarHide);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.SHOW_HP, this.HpBarShow);

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.DEBUFF_SHOW, this.DebuffArrowShow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.BUFF_SHOW, this.BuffArrowShow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.DEBUFF_HIDE, this.DebuffArrowHide);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.BUFF_SHOW, this.BuffArrowHide);

        this.animator = this.GetComponent<Animator>();

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


        this.acc = 10;
        this.spd = 3;
        this.maxhp = 20;
        this.hp = this.maxhp;
        this.atk = 4;
        this.def = 1;

        //UnitActionManager.Instance.StoreUnit(this);
        //this.charName = "Carrot";
        //this.ingredientType = EIngredientType.CARROT;
        //this.type = EUnitType.Ally;
        base.Start();
    }
}
