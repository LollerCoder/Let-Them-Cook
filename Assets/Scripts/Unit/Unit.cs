using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
    
public abstract class Unit: MonoBehaviour {



    public const string UNIT = "UNIT";

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

   

    protected EIngredientType ingredientType;
    public EIngredientType IngredientType{
        get { return this.ingredientType; }
        set { this.ingredientType = value; }
    }

    protected EUnitType type = EUnitType.Ally;
    public EUnitType Type { 
        get { return this.type; }
        set { this.type = value;  }
    }

    protected string charName; // unit name
    public string Name { get { return this.charName; } }

    /// <summary>
    /// Accuracy
    /// </summary>

    protected float acc; // hit
    public float Accuracy {

        get { return this.acc; }
        set { this.acc = value; }
    
    }

    protected float accMult = 1; // hit
    public float AccuracyMultiplier { 

        get { return this.accMult; }
        set { this.accMult = value; }

    }

    /// <summary>
    /// SPEED
    /// </summary>

    protected float spd; // movement range
    public float Speed {
        
        get { return this.spd; }
        set { this.spd = value; }

    }

    protected float spdMult = 1; // hit
    public float SpeedMultiplier {
        
        get { return this.spdMult; }
        set { this.spdMult = value; }

    }

    /// <summary>
    /// Attack
    /// </summary>

    protected float atk; // dmg
    public float Attack {
        
        get { return this.atk; }
        set { this.atk = value; }

    }

    protected float atkMult = 1; // hit
    public float AttackMultiplier { 
        
        get { return this.atkMult; }
        set { this.atkMult = value; }

    }

    /// <summary>
    /// HP
    /// </summary>

    protected int hp; // health
    public int HP { 
        
        get { return this.hp; }
        set { this.hp = value; }
         
    }


    /// <summary>
    /// DEFENSE
    /// </summary>
    protected float def; // defense
    public float Defense {
        
        get { return this.def; }
        set { this.def = value; }

    }

    protected float defMult = 1; // defense
    public float DefenseMultiplier { 
        
        get { return this.defMult; }
        set { this.defMult = value; }
    }

    /// <summary>
    /// MaxHP
    /// </summary>

    protected int maxhp; // max health
    public int MAXHP { get { return this.maxhp; } }

    protected int basicrange = 2; // range
    public int BasicRange { get { return this.basicrange; } }

    protected Animator animator;

    protected bool defend = false;

    public bool Defend {
        get { return this.defend; }
        set { this.defend = value; }
    }

    protected bool eatable = false;

    public bool Eatable {
        get { return this.eatable; }
    }

    [SerializeField]
    private Tile _tile; // tile on the grid
    public Tile Tile {
        get { return _tile; }
        set { _tile = value; }
    }

    private bool turn = false;

    public bool Turn {
        get { return this.turn; }
    }

    public void TakeDamage(float damage, Unit attacker) {

        Debug.Log("Unit name: " + attacker.Name);
        Debug.Log(attacker.effectManager);

        attacker.effectManager.EffectAccess(attacker); // attacker
        this.effectManager.EffectAccess(this); //target
        
        //check if its true damage
        if(damage == 0) {
            if (this.isDodged(attacker) || GameSettingsManager.Instance.turnOffDodge)
            {
                Debug.Log("HP before :" + this.hp);
                int dmg = CalculateDamage(attacker);
                if (this.defend) {
                    
                    dmg = dmg - (int)Mathf.Round(dmg * 0.2f);
                }
                this.hp -= dmg;
                this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0
                Debug.Log("Dealt Damage: " + dmg);
                
                PopUpManager.Instance.addPopUp(dmg.ToString(), this.transform);
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
            this.hp -= (int)damage;
            this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0

        }


        if (this.hp == 0) {
            Debug.Log("Its Dead");
            this.Tile.isWalkable = true;

            if (attacker.type == EUnitType.Ally)
            {
                Debug.Log(this.type);
                GameManager.Instance.OnDiedCallback.Invoke(this.ingredientType);
            }

            UnitActionManager.Instance.RemoveUnitFromOrder(this);
            this.HandleDeath();
            
        }


        attacker.effectManager.EffectReset(attacker);
        this.effectManager.EffectReset(this);


    }

    


    public bool isDodged(Unit attacker)
    {
        float chance = 50 + ((attacker.spd + attacker.acc - this.spd));
            float x = UnityEngine.Random.Range(1,100);
        
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

    public void Heal(Unit target) {
        this.hp += 4;
        Debug.Log($"New HP: {this.hp}");
        target.HandleEaten();
    }
        
    public void Heal()
    {
        this.hp += 4;
        Debug.Log($"New HP: {this.hp}");
    }

    public void OnTurn(bool value) {
        if(this.animator != null) {
            this.animator.SetBool("Turn", value);
            this.turn = value;
        }
    }

    public void OnMovement(bool value) {
        if(this.animator != null) {
            this.animator.SetBool("Walk", value);
        }
    }
    private void OnMouseEnter() {
        UnitActions.UnitHover(this);
    }

    private void OnMouseExit() {
        
    }
    protected void OnMouseUp() {
        UnitActions.UnitSelect(this);
    }

    protected virtual void Start() {
        if (this.type == EUnitType.Ally) {
            this.animator.SetBool("Ally", true);
        }
        if (this.type != EUnitType.Ally) {
            this.animator.SetBool("Ally", false);
        }
    }

    //protected virtual void SetHighlight(bool value) {
    //    this.animator.SetBool("Ally", value);
    //} 
    public abstract void GetAttackOptions();
    public abstract void UnitAttack(Unit target);
    public abstract void Selected();
    protected abstract void HandleDeath();
    public virtual void HandleEaten() {
        Destroy(this.gameObject);
    }

}
