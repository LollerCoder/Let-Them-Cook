using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class Unit: MonoBehaviour {

    protected List<Skill> skillList = new List<Skill>();
    [SerializeField]

    

    public List<Skill> SKILLLIST
    {
        get { return skillList; }
    }

    protected EIngredientType ingredientType;
    public EIngredientType IngredientType{
        get { return this.ingredientType; }
        set { this.ingredientType = value; }
    }

    protected EUnitType type;
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
    public float Accuracy { get { return this.acc; } }

    protected float accMult = 1; // hit
    public float AccuracyMultiplier { get { return this.accMult; } }

    /// <summary>
    /// SPEED
    /// </summary>

    protected float spd; // movement range
    public float Speed { get { return this.spd; } }

    protected float spdMult = 1; // hit
    public float SpeedMultiplier { get { return this.spdMult; } }

    /// <summary>
    /// Attack
    /// </summary>

    protected float atk; // dmg
    public float Attack { get { return this.atk; } }

    protected float atkMult = 1; // hit
    public float AttackMultiplier { get { return this.atkMult; } }

    /// <summary>
    /// HP
    /// </summary>

    protected int hp; // health
    public int HP { get { return this.hp; } }


    /// <summary>
    /// DEFENSE
    /// </summary>
    protected float def; // defense
    public float DEF { get { return this.def; } }

    protected float defMult = 1; // defense
    public float DefenseMultiplier { get { return this.defMult; } }

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
    public void TakeDamage(float damage, Unit attacker) {
        attacker.EffectAccess(attacker);
        if(damage == 0) {
            if (this.isDodged(attacker))
            {
                Debug.Log("HP before :" + this.hp);
                int dmg = CalculateDamage(attacker);
                if (this.defend) {
                    
                    dmg = dmg - (int)Mathf.Round(dmg * 0.2f);
                }
                this.hp -= dmg;
                this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0
                Debug.Log("Dealt Damage: " + dmg);
                Debug.Log("HP after :" + this.hp);

                this.defend = false;


            }
            else
            {
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


        attacker.EffectReset(attacker);

       
    }

    public void EffectAccess(Unit applyTo)
    {
        ;
        foreach (string key in applyTo.effectList.Keys)
        {
        
            float augment = applyTo.effectList[key].MOD;
            EStatToEffect stat = applyTo.effectList[key].STAT;
            Debug.Log(applyTo.effectList[key].STAT);
            switch (stat)
            {
                case EStatToEffect.ACCURACY:
                    applyTo.accMult += augment / 100;
                    //apply
                    applyTo.acc *= applyTo.accMult;
                    Debug.Log(applyTo.accMult);
                    break;
                case EStatToEffect.SPEED:
                    applyTo.spdMult += augment / 100;
                    //apply
                    applyTo.spd *= applyTo.spdMult;
                    break;
                case EStatToEffect.DEFENSE:
                    applyTo.defMult += augment / 100;
                    //apply
                    applyTo.def *= applyTo.defMult;
                    break;
                case EStatToEffect.ATTACK:
                    applyTo.atkMult += augment / 100;
                    //apply
                    applyTo.atk *= applyTo.atkMult;
                    break;
                default:
                    Debug.Log("Invalid");

                    break;

            }
        }
    }

    public void EffectTimer()
    {
        List<string> toDelete = new List<string>();
        if(effectList.Count != 0)
        {
            foreach (string key in effectList.Keys)
            {


                effectList[key].DURATION -= 1;
                Debug.Log("Effect " + key + " has " + effectList[key].DURATION + " left");

                if (effectList[key].DURATION == 0)
                {
                    toDelete.Add(key);

                }
            }
        }
        foreach(string keyDelete in toDelete)
        {
            effectList.Remove(keyDelete);
        }
       
        
    }
    private void EffectReset(Unit applyTo)
    {
        applyTo.accMult = 1;
        applyTo.defMult = 1;
        applyTo.atkMult = 1;
        applyTo.spdMult = 1;
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
    public void OnDefend() {
        this.Defend = true;
    }

    public void OnMove(bool value) {
        if(this.animator != null) {
            this.animator.SetBool("Walk", value);
        }
    }
    protected void OnMouseEnter() {
        UnitActionManager.Instance.UnitHover(this);
    }

    protected void OnMouseExit() {
        UnitActionManager.Instance.UnitHover(this);
    }

    protected void OnMouseUp() {
        UnitActionManager.Instance.UnitSelect(this);
    }                                                                                             

    public abstract void GetAttackOptions();
    public abstract void UnitAttack(Unit target);
    public abstract void Selected();
    protected abstract void HandleDeath();
    public virtual void HandleEaten() {
        Destroy(this.gameObject);
    }

}
