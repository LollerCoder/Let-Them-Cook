using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Potato : Unit {
    [SerializeField]
    private Sprite potato;

    public bool ondefend = false;

    [SerializeField]
    public GameObject hpBar;

    public override void UnitAttack(Unit unit2) {

    }

    public override void Selected() {

    }

    public override void GetAttackOptions() {

    }

    protected override void HandleDeath() {
        this.GetComponent<SpriteRenderer>().sprite = potato;
        this.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
        this.eatable = true;
    }

    private void Update() {
        this.ondefend = this.defend;

       
    }

    void HpBarShow(Parameters param)
    {

        Unit unit = param.GetUnitExtra(UNIT);

        if (unit == this) {
            this.hpBar.SetActive(true);

            StartCoroutine(HpUpdate());
        }

    }

    IEnumerator HpUpdate()
    {
        Debug.Log("Dying");
        yield return new WaitForSeconds(0.5f);
        hpBar.GetComponentInChildren<Slider>().maxValue = this.maxhp;
        hpBar.GetComponentInChildren<Slider>().value = this.hp;
        this.hpBar.SetActive(false);
    }

    protected override void Start() {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.UNIT_ATTACK, this.HpBarShow);

        this.ondefend = this.defend;
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
