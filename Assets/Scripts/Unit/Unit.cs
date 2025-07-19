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

    public void TakeDamage(float damage, Unit attacker)
    {
        attacker.effectManager.EffectAccess(attacker); // attacker
        this.effectManager.EffectAccess(this); //target

        //Doing the damage
        int dmg = CalculateDamage(attacker);
        this.hp -= (int)damage + dmg;
        this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0


        //unit is dead
        if (this.hp == 0)
        {
            Debug.Log("Its Dead");
            this.Tile.isWalkable = true;

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

    public int CalculateDamage(Unit attacker)
    {
        float dmg = 1 + attacker.Attack * (1 - (this.def + this.Speed) / 100);
        dmg = (float)Math.Floor(dmg);
        return (int)dmg;
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
                

                Parameters param = new Parameters();
                param.PutExtra("EnemyKilled", this);
                EventBroadcaster.Instance.PostEvent(EventNames.Enemy_Events.ON_ENEMY_DEFEATED);
                EventBroadcaster.Instance.PostEvent(EventNames.Enemy_Events.ON_ENEMY_DEFEATED,param);
                
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

        if (UnitActionManager.Instance.GetFirstUnit() is Unit _unit && _unit == this && this.Type != EUnitType.Enemy)//its you
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
        this.BufController.SetBool("isBuffed", flag);
    }

    public void ToggleDebuffArrow(bool flag)
    {
        this.DebufController.SetBool("isDebuffed", flag);
    }

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

    public void SetAnimatorBool(string name, bool value)
    {
        this.animator.SetBool(name, value);
    }

    public void ChangeColor(Color _color)
    {
        this.spriteRenderer.color = _color;
    }

    public Color poisonUnit(SpriteRenderer spriteAsset)
    {

        if (spriteAsset != null)Debug.Log("POISON");

        Color poison = Color.green;

        spriteAsset.color = poison;

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
        bool isSame;

        foreach (Effect ef in this.effects)
        {
            isSame = String.Equals(effect, ef.EffectName);
            if (isSame) return ef;
        }

        return null;
    }

    public void PrintEffects()
    {
        Debug.Log("Effects of " + this.Name);

        foreach (Effect ef in this.effects)
            Debug.Log(ef.EffectName);
    }
}