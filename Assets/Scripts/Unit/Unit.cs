using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Unit : MonoBehaviour, ITurnTaker {

    public SpriteRenderer spriteRenderer;
    public float Speed { get; set; }
    public Sprite Sprite { get; set; }

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

    [Header("Unit Stats")]
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

    public float speed;
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

    protected float spdMult = 1; // hit

    [SerializeField]
    public float SpeedMultiplier
    {
        get { return this.spdMult; }
        set { this.spdMult = value; }
    }

    [SerializeField]
    protected float atk; // dmg
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

    [SerializeField]
    protected int basicrange = 2; // range
    public int BasicRange { get { return this.basicrange; } }

    [SerializeField]
    protected float move = 3; // movement range
    public float Move
    {
        get { return this.move; }
        set { this.move = value; }
    }

    [SerializeField]
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

    public bool OnWeapon = false;

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

        //Debug.Log("Unit name: " + attacker.Name);
        //Debug.Log(attacker.effectManager);

        attacker.effectManager.EffectAccess(attacker); // attacker
        this.effectManager.EffectAccess(this); //target

        //check if its true damage
        //if (damage == 0)
        //{
        //    if (this.isDodged(attacker) || GameSettingsManager.Instance.turnOffDodge)
        //    {
        //        Debug.Log("HP before :" + this.hp);
        //        int dmg = CalculateDamage(attacker);
        //        if (this.defend)
        //        {

        //            dmg = dmg - (int)Mathf.Round(dmg * 0.2f);
        //        }
        //        this.hp -= dmg;
        //        this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0
        //        Debug.Log("Dealt Damage: " + dmg);

        //        PopUpManager.Instance.addPopUp(dmg.ToString(), this.transform);
        //        Debug.Log("My name is: " + this.Name + "Yo");
        //        Debug.Log("HP after :" + this.hp);

        //        //PopUpManager.Instance.addpopUpHealth(this.MAXHP, this.HP,this.transform);

        //        Debug.Log("HP after :" + this.hp);

        //        this.defend = false;


        //    }
        //    else
        //    {
        //        PopUpManager.Instance.addPopUp("DODGE", this.transform);
        //        Debug.Log("DODGE");
        //    }
        //}


        //Doing the damage
        int dmg = CalculateDamage(attacker);
        //Debug.Log("Damage taken: " + ((int)damage + dmg));
        this.hp -= (int)damage + dmg;
        this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0


        //unit is dead
        if (this.hp == 0)
        {
            Debug.Log("Its Dead");
            this.Tile.isWalkable = true;

            //if (attacker.type == EUnitType.Ally)
            //{
            //    Debug.Log(this.type);
            //    GameManager.Instance.OnDiedCallback.Invoke(this.ingredientType);
            //}

            this.HandleDeath();

        }


        attacker.effectManager.EffectReset(attacker);
        this.effectManager.EffectReset(this);

        Parameters param = new Parameters();
        param.PutExtra(UNIT, this);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);
    }

    public void TakeDamageFromTile(int damage) {
        this.hp -= damage;
        PopUpManager.Instance.addPopUp(damage.ToString(), this.transform);

        if (this.hp <= 0) {
            Debug.Log("Its Dead");
            this.Tile.isWalkable = true;

            this.HandleDeath();
        }

        Parameters param = new Parameters();
        param.PutExtra(UNIT, this);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);
    }

    public void gainHealth(float healPts, Unit partyMember)
    {

        //Debug.Log("Unit name: " + partyMember.Name);
        //Debug.Log(partyMember.effectManager);

        partyMember.effectManager.EffectAccess(partyMember); // attacker
        this.effectManager.EffectAccess(this); //target
        this.hp += (int)healPts;
        if (this.hp >= this.maxhp) this.hp = this.maxhp; //no overheal



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
        float chance = 50 + ((attacker.Speed + attacker.acc - this.Speed));
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
        float dmg = 1 + attacker.Attack * (1 - (this.def + this.Speed) / 100);
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
        //Debug.Log("TURN: " + value);
        if (this.animator != null)
        {
            this.animator.SetBool("Turn", value);
        }
    }
    public void OnMovement(bool value)
    {
        //Debug.Log("Moving" + value);

        if (this.animator != null)
        {
            this.animator.SetBool("Walk", value);
        }
    }


    ///SPRINGS <summary>
    
    public void OnSpring(bool value) {
        if (this.animator != null) {
            Debug.Log("GONE");
            this.animator.SetBool("HasSpringed", true);
        }
    }
   
    private void Launch()
    {
        if (this.animator != null)
        {
            SFXManager.Instance.Play("SpringDown");
            this.animator.SetBool("HasSpringed", false);
            Debug.Log("LANDING");
            
        }
    }
   
    ///SPRINGS
    private void HandleDeath()
    {
        
        


        DroppedVegetableManager.Instance.CreateDropVegetable(this);

        //doing effect actions
        this.AfterDeathEffects();

        this.animator.enabled = false;
        this.gameObject.SetActive(false);
        this.Tile.isWalkable = true;

        UnitActionManager.Instance.RemoveUnitFromOrder(this);
        //add if statements
        switch (this.Type)
        {
            case EUnitType.Enemy:
                EventBroadcaster.Instance.PostEvent(EventNames.Enemy_Events.ON_ENEMY_DEFEATED);
                break;
            case EUnitType.Ally:
                EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.PLAYERDEATH);
                break;
        }
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
        
        if (UnitActionManager.Instance.TurnOrder[0] == this && this.Type != EUnitType.Enemy)//its you
        {
            isYou = true;
        }
        this.hpBar.GetComponent<HpBar>().setColor(this.Type, isYou);

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

    public void ToggleBuffArrow(bool flag)
    {
        //if (this == param.GetUnitExtra("UNIT"))
        //{
        //    this.BufController.SetBool("isBuffed", false);
        //    //Debug.Log("buffGone");
        //}
        this.BufController.SetBool("isBuffed", flag);
    }

    public void ToggleDebuffArrow(bool flag)
    {
        //if (this == param.GetUnitExtra("UNIT"))
        //{
        //    this.DebufController.SetBool("isDebuffed", false);
        //    // Debug.Log("DebuffedGone");
        //}
        this.DebufController.SetBool("isDebuffed", flag);
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

        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.LAUNCH, this.Launch);


        if (this.type == EUnitType.Ally) {
            this.animator.SetBool("Ally", true);
        }
        if (this.type != EUnitType.Ally) {
            this.animator.SetBool("Ally", false);
        }

        Slider hpSlide = this.hpBar.transform.Find("Slider").GetComponent<Slider>();
        hpSlide.maxValue = this.maxhp;
        hpSlide.value = hp;
        Slider easeSlide = this.hpBar.transform.Find("EaseSlider").GetComponent<Slider>();
        easeSlide.maxValue = this.maxhp;
        easeSlide.value = hp;

        this.Speed = speed;

        UnitActionManager.Instance.AddUnit(this);

        this.Sprite = this.spriteRenderer.sprite;
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

    /*  ANIMATOR    */  
    public void SetAnimatorBool(string name, bool value)
    {
        this.animator.SetBool(name, value);
    }

    /*COLOR CHANGER*/

    public void ChangeColor(Color _color)
    {
        this.spriteRenderer.color = _color;

        //Debug.Log("Prev color: " + spriteAsset.color);
    }

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

    public void AddEffects(List<Effect> effects)
    {
        this.effects.AddRange(effects);
    }

    //Applying effects
    public void ApplyEffects()
    {
        foreach (Effect effect in this.effects)
        {
            effect.ApplyEffect(this);

            //check if finished
            if (effect.Duration <= 0)
                effect.EffectAfterAction(this);
        }

        this.effects.RemoveAll(ef => ef.Duration <= 0);
    }

    public void EndTurnEffects()
    {
        foreach (Effect effect in this.effects)
        {
            effect.AfterTurnAction(this);
        }
    }

    public void AfterDeathEffects()
    {
        foreach (Effect effect in this.effects)
        {
            effect.AfterDeathAction(this);
        }
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
            //Debug.Log(effect + " vs " + ef.EffectName + " is " + isSame);
            //Debug.Log("EF: " + ef);
            if (isSame) return ef;
        }

        //Debug.Log("Didn't find effect");
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