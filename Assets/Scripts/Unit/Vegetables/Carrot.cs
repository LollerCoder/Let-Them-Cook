using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Carrot : Unit{
    [Header("Customize Stat")]
    [SerializeField]
    private float spd = 3;

    private void Update()
    {
   
         
    }
    
    protected override void Start()
    {

        //EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.HIDE_HP, this.HpBarHide);
        //EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.SHOW_HP, this.HpBarShow);

        //EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.DEBUFF_SHOW, this.DebuffArrowShow);
        //EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.BUFF_SHOW, this.BuffArrowShow);
        //EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.DEBUFF_HIDE, this.DebuffArrowHide);
        //EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.BUFF_SHOW, this.BuffArrowHide);

        //this.animator = this.GetComponent<Animator>();

        //this.skillList.Add("Basic Attack");
        this.skillList.Add("Foil Throw");
        this.skillList.Add("Photosynthesis");

        this.acc = 10;
        this.Speed = this.spd;
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
