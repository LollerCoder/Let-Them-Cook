using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Unit : MonoBehaviour
{

    public const string UNIT = "UNIT";

    [SerializeField]
    public GameObject hpBar;

    public GameObject HPBAR
    {
        get { return this.hpBar; }
    }

    protected List<string> skillList = new List<string>();

    protected EffectManager effectManager = new EffectManager();

    public EffectManager EffectManager
    {
        get { return this.effectManager; }
    }

    public List<string> SKILLLIST
    {
        get { return skillList; }
    }

    [SerializeField]
    protected EIngredientType ingredientType;
    public EIngredientType IngredientType
    {
        get { return this.ingredientType; }
        set { this.ingredientType = value; }
    }

    [SerializeField]
    protected EUnitType type = EUnitType.Ally;
    public EUnitType Type
    {
        get { return this.type; }
        set { this.type = value; }
    }

    [SerializeField]
    protected string charName; // unit name
    public string Name
    {
        get { return this.charName; }
    }

    protected float acc; // hit

    [SerializeField]
    public float Accuracy
    {
        get { return this.acc; }
        set { this.acc = value; }
    }

    protected float accMult = 1; // hit
    public float AccuracyMultiplier
    {
        get { return this.accMult; }
        set { this.accMult = value; }
    }

    protected float spd; // movement range

    [SerializeField]
    public float Speed
    {
        get { return this.spd; }
        set { this.spd = value; }
    }

    protected float spdMult = 1; // hit

    [SerializeField]
    public float SpeedMultiplier
    {
        get { return this.spdMult; }
        set { this.spdMult = value; }
    }

    protected float atk; // dmg

    [SerializeField]
    public float Attack
    {
        get { return this.atk; }
        set { this.atk = value; }
    }

    protected float atkMult = 1; // hit
    public float AttackMultiplier
    {

        get { return this.atkMult; }
        set { this.atkMult = value; }

    }

    [SerializeField]
    protected int hp; // health
    public int HP
    {
        get { return this.hp; }
        set { this.hp = value; }
    }

    protected float def; // defense

    [SerializeField]
    public float Defense
    {
        get { return this.def; }
        set { this.def = value; }
    }

    protected float exp; // exp

    [SerializeField]
    public float Experience
    {
        get { return this.exp; }
        set { this.exp = value; }
    }

    protected float defMult = 1; // defense
    public float DefenseMultiplier
    {
        get { return this.defMult; }
        set { this.defMult = value; }
    }

    protected int maxhp; // max health
    public int MAXHP { get { return this.maxhp; } }


    protected float maxexp; // max experience
    public float MAXEXP { get { return this.maxexp; } }

    protected int basicrange = 2; // range
    public int BasicRange { get { return this.basicrange; } }

    protected float move = 3; // move
    public float Move
    {
        get { return this.move; }
        set { this.move = value; }
    }





    protected Animator animator;

    protected bool defend = false;
    public bool Defend
    {
        get { return this.defend; }
        set { this.defend = value; }
    }

    [SerializeField]
    private Tile _tile; // tile on the grid
    public Tile Tile
    {
        get { return _tile; }
        set { _tile = value; }
    }

    public bool InRange = false;

    //
    [Header("Buff and Debuff Controllers")]

    [SerializeField]
    Animator BufController;

    [SerializeField]
    Animator DebufController;

    [Header("Effects")]
    [SerializeField]
    private List<Effect> effects = new List<Effect>();

    /// <summary>
    /// BELOW ARE THE METHODS
    /// </summary>

    public void TakeDamage(float damage, Unit attacker)
    {

        Debug.Log("Unit name: " + attacker.Name);
        Debug.Log(attacker.effectManager);

        attacker.effectManager.EffectAccess(attacker); // attacker
        this.effectManager.EffectAccess(this); //target

        //check if its true damage
        if (damage == 0)
        {
            if (this.isDodged(attacker) || GameSettingsManager.Instance.turnOffDodge)
            {
                Debug.Log("HP before :" + this.hp);
                int dmg = CalculateDamage(attacker);
                if (this.defend)
                {

                    dmg = dmg - (int)Mathf.Round(dmg * 0.2f);
                }
                this.hp -= dmg;
                this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0
                Debug.Log("Dealt Damage: " + dmg);

                PopUpManager.Instance.addPopUp(dmg.ToString(), this.transform);
                Debug.Log("My name is: " + this.Name + "Yo");
                Debug.Log("HP after :" + this.hp);

                //PopUpManager.Instance.addpopUpHealth(this.MAXHP, this.HP,this.transform);

                Debug.Log("HP after :" + this.hp);

                this.defend = false;


            }
            else
            {
                PopUpManager.Instance.addPopUp("DODGE", this.transform);
                Debug.Log("DODGE");
            }
        }

        else
        {
            //PopUpManager.Instance.addPopUp(damage.ToString(), this.transform);
            this.hp -= (int)damage;
            this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0

        }


        if (this.hp == 0)
        {
            Debug.Log("Its Dead");
            this.Tile.isWalkable = true;

            if (attacker.type == EUnitType.Ally)
            {
                Debug.Log(this.type);
                GameManager.Instance.OnDiedCallback.Invoke(this.ingredientType);
            }

            this.HandleDeath();

        }


        attacker.effectManager.EffectReset(attacker);
        this.effectManager.EffectReset(this);

        Parameters param = new Parameters();
        param.PutExtra(UNIT, this);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);
    }

    public void gainHealth(float healPts, Unit partyMember)
    {

        Debug.Log("Unit name: " + partyMember.Name);
        Debug.Log(partyMember.effectManager);

        partyMember.effectManager.EffectAccess(partyMember); // attacker
        this.effectManager.EffectAccess(this); //target
        this.hp += (int)healPts;
        this.hp = Mathf.Max(HP, 0);



        if (this.hp == 0)
        {
            Debug.Log("Its Dead");
            this.Tile.isWalkable = true;

            if (partyMember.type == EUnitType.Ally)
            {
                Debug.Log(this.type);
                GameManager.Instance.OnDiedCallback.Invoke(this.ingredientType);
            }

            this.HandleDeath();

        }


        partyMember.effectManager.EffectReset(partyMember);
        this.effectManager.EffectReset(this);

        Parameters param = new Parameters();
        param.PutExtra(UNIT, this);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);
    }

    public void gainDefense(float defensePoints, Unit defender)
    {

        Debug.Log("Unit name: " + defender.Name);
        Debug.Log(defender.effectManager);

        defender.effectManager.EffectAccess(defender); // attacker
        this.effectManager.EffectAccess(this); //target
        this.def += (int)defensePoints;
        this.def = Mathf.Max(HP, 0);

        if (this.hp == 0)
        {
            Debug.Log("Its Dead");
            this.Tile.isWalkable = true;

            if (defender.type == EUnitType.Ally)
            {
                Debug.Log(this.type);
                GameManager.Instance.OnDiedCallback.Invoke(this.ingredientType);
            }

            this.HandleDeath();

        }


        defender.effectManager.EffectReset(defender);
        this.effectManager.EffectReset(this);

        Parameters param = new Parameters();
        param.PutExtra(UNIT, this);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_DEFENSE, param);

    }
    public bool isDodged(Unit attacker)
    {
        float chance = 50 + ((attacker.spd + attacker.acc - this.spd));
        float x = UnityEngine.Random.Range(1, 100);

        if (x < chance)
        {
            Debug.Log("HIT");
            return true;
        }
        Debug.Log("MISS");
        return false;
    }
    public int CalculateDamage(Unit attacker)
    {
        float dmg = 1 + attacker.Attack * (1 - (this.def + this.spd) / 100);
        dmg = (float)Math.Floor(dmg);
        return (int)dmg;
    }
    public void Eat(Unit target)
    {

    }
    public void Heal()
    {
        this.hp += 5;
        //Debug.Log($"New HP: {this.hp}");
    }
    public void OnTurn(bool value)
    {
        if (this.animator != null)
        {
            this.animator.SetBool("Turn", value);
        }
    }
    public void OnMovement(bool value)
    {
        if (this.animator != null)
        {
            this.animator.SetBool("Walk", value);
        }
    }

    private void HandleDeath()
    {
        DroppedVegetableManager.Instance.CreateDropVegetable(this);

        this.GetComponent<Animator>().enabled = false;
        this.gameObject.SetActive(false);
        this.Tile.isWalkable = true;
        UnitActionManager.Instance.RemoveUnitFromOrder(this);
    }

    private void OnMouseEnter()
    {
        UnitActions.UnitHover(this, true);
    }

    private void OnMouseExit()
    {
        UnitActions.UnitHover(this, false);
    }
    protected void OnMouseUp()
    {
        UnitActions.UnitSelect(this);
    }

    //////////////////////////////////
    //


    private void HpBarShow(Parameters param)
    {
        bool isYou = false;
        Unit unit = param.GetUnitExtra(UNIT);


        //if (this.Type == EUnitType.Enemy)//enemy
        //{
        //    this.hpBar.transform.Find("Slider").GetComponentInChildren<Image>().color = new Color(0.8941177f, 0, 0.05098039f, 1);

        //}

        //if (this.Type == EUnitType.Ally)//ally
        //{
        //    this.hpBar.transform.Find("Slider").GetComponentInChildren<Image>().color = new Color(0.0619223f, 0.2870282f, 0.8415094f, 1);
        //}

        if (UnitActionManager.Instance.UnitOrder[0] == this && this.Type != EUnitType.Enemy)//its you
        {
            isYou = true;
        }
        this.hpBar.GetComponent<HpBar>().setColor(unit.Type, isYou);

        if (unit == this)
        {
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

    /// <summary>
    /// ////////////////////////////////////////
    /// </summary>
    /// 

    protected virtual void Start()
    {
        //ADDING OBSERVERS
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.HIDE_HP, this.HpBarHide);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.SHOW_HP, this.HpBarShow);

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.DEBUFF_SHOW, this.DebuffArrowShow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.BUFF_SHOW, this.BuffArrowShow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.DEBUFF_HIDE, this.DebuffArrowHide);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.BUFF_SHOW, this.BuffArrowHide);

        this.animator = this.GetComponent<Animator>();

        if (this.type == EUnitType.Ally)
        {
            this.animator.SetBool("Ally", true);
        }
        if (this.type != EUnitType.Ally)
        {
            this.animator.SetBool("Ally", false);
        }

        Slider hpSlide = this.hpBar.transform.Find("Slider").GetComponent<Slider>();
        hpSlide.maxValue = this.maxhp;
        hpSlide.value = hp;
        Slider easeSlide = this.hpBar.transform.Find("EaseSlider").GetComponent<Slider>();
        easeSlide.maxValue = this.maxhp;
        easeSlide.value = hp;

        UnitActionManager.Instance.UnitList.Add(this);
    }

    //protected abstract void HandleDeath();

    // public Unit(Unit unit)
    // {
    //     this.Accuracy = unit.Accuracy;
    //     this.Speed = unit.Speed;
    //     this.Attack = unit.Attack;
    //     this.Defense = unit.Defense;
    //     this.Experience = unit.Experience;
    // }



    /*COLOR CHANGER*/

    public Color poisonUnit(SpriteRenderer spriteAsset)
    {

        if (spriteAsset != null)Debug.Log("POISON");
        //add damage logic here later

        //Color change

        /*
        R - 79
        G - 224
        B - 90
        */
        Color poison = Color.green;

        spriteAsset.color = poison;
        // float rValue = 0, gValue = 0, bValue = 0;

        // for (int counter = 0; counter <= 224; counter++)
        // {
        //     if (gValue <= 224) { poison.g = gValue; gValue++; }
        //     if (rValue <= 79) { poison.r = rValue; rValue++; }
        //     if (bValue <= 90) { poison.b = bValue; bValue++; }


        //     spriteAsset.color = poison;
        // }

        //back to normal color


        return poison;
        

    }

    ////////////////////////////
    ///
    ///     EFFECTS

    //Adding effects
    public void AddEffect(Effect effect)
    {
        this.effects.Add(effect);
    }

    public void AddEffect(EnumEffects effects, Unit origin, Parameters effectParam)
    {
        Debug.Log("Adding effect");

        string effectName = "null";
        int duration = effectParam.GetIntExtra("duration", 1);

        switch (effects)
        {
            case EnumEffects.Poison:
                Poison poisonEffect = new Poison(duration, origin);
                this.effects.Add(poisonEffect);
                effectName = "Poison";
                break;
            case EnumEffects.RechargeSkill:
                RechargingSkill rck = new RechargingSkill(duration, origin, effectParam.GetStringExtra("SkillName", "Skill"));
                Debug.Log(rck);
                this.effects.Add(rck);
                effectName = "Recharge Skill";
                break;
            default:
                Debug.Log("Effect not found!");
                break;
        }

        Debug.Log("Added effect " + effectName);
    }

    //Applying effects
    public void ApplyEffects()
    {
        foreach (Effect effect in this.effects)
        {
            effect.ApplyEffect(this);
        }

        this.effects.RemoveAll(ef => ef.Duration <= 0);
    }

    public Effect GetEffect(string effect)
    {

        //for (int i = 0; i < this.effects.Count; i++)
        //{
        //    if (String.Equals(this.effects[i].EffectName, effect))
        //    {
        //        Debug.Log("EF: " + this.effects[i]);
        //        return this.effects[i];
        //    }
        //}

        bool isSame;

        foreach (Effect ef in this.effects)
        {
            isSame = String.Equals(effect, ef.EffectName);
            Debug.Log(effect + " vs " + ef.EffectName + " is " + isSame);
            Debug.Log("EF: " + ef);
            if (isSame) return ef;
        }

        Debug.Log("Didn't find effect");
        return null;
        //return this.effects.Find(ef => String.Equals(effect, ef.EffectName));
    }

    public void PrintEffects()
    {
        Debug.Log("Effects of " + this.Name);

        foreach (Effect ef in this.effects)
            Debug.Log(ef.EffectName);
    }
}