using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Potato : Unit {

    [SerializeField]
    Animator BufController;

    [SerializeField]
    Animator DebufController;
    public override void UnitAttack(Unit unit2) {

    }

    public override void Selected()
    {

    }

    public override void GetAttackOptions()
    {

    }
    private void Update()
    {

    }
    private void HpBarShow(Parameters param)
    {

        Unit unit = param.GetUnitExtra(UNIT);

        if (this.Type == EUnitType.Enemy)//enemy
        {
            this.hpBar.GetComponentInChildren<Slider>().GetComponentInChildren<Image>().color = new Color(0.8941177f, 0, 0.05098039f, 1);

        }

        if (this.Type == EUnitType.Ally)//ally
        {
            this.hpBar.GetComponentInChildren<Slider>().GetComponentInChildren<Image>().color = new Color(0.0619223f, 0.2870282f, 0.8415094f, 1);
        }

        if (UnitActionManager.Instance.UnitOrder[0] == this && this.Type != EUnitType.Enemy)//its you
        {
            this.hpBar.GetComponentInChildren<Slider>().GetComponentInChildren<Image>().color = new Color(0.2638531f, 0.8943396f, 0.2008044f, 1);
        }


        if (unit == this)
        {
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


    private void BuffArrowShow()
    {
        this.BufController.SetBool("isBuffed", true);
        Debug.Log("Buffed");
    }

    private void DebuffArrowShow()
    {
        this.DebufController.SetBool("isDebuffed", true);
        Debug.Log("Debuffed");
    }


    protected override void Start() {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.SHOW_HP, this.HpBarShow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.HIDE_HP, this.HpBarHide);

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.DEBUFF_SHOW, this.DebuffArrowShow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.BUFF_SHOW, this.BuffArrowShow);

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
        this.ingredientType = EIngredientType.POTATO;
    }


}
