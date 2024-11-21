using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[Serializable]
public abstract class Unit : MonoBehaviour
{

    public const string UNIT = "UNIT";

    [SerializeField]
    public GameObject hpBar;

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
    public EIngredientType IngredientType
    {
        get { return this.ingredientType; }
        set { this.ingredientType = value; }
    }

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

    protected bool eatable = false;

    public bool Eatable
    {
        get { return this.eatable; }
    }

    [SerializeField]
    private Tile _tile; // tile on the grid
    public Tile Tile
    {
        get { return _tile; }
        set { _tile = value; }
    }

    public bool InRange = false;
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
    public void Heal(Unit target)
    {
        this.hp += 4;
        Debug.Log($"New HP: {this.hp}");
        target.HandleEaten();
    }
    public void Heal()
    {
        this.hp += 4;
        Debug.Log($"New HP: {this.hp}");
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

        Vector3 pos = new Vector3(this.transform.position.x, 1.1f, this.transform.position.z);
        Debug.Log(this.IngredientType.ToString());
        DroppedVegetableManager.Instance.CreateDropVegetable(this.IngredientType.ToString(), pos);

        this.eatable = true;

        this.GetComponent<Animator>().enabled = false;
        this.gameObject.SetActive(false);

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

    protected virtual void Start()
    {
        if (this.type == EUnitType.Ally)
        {
            this.animator.SetBool("Ally", true);
        }
        if (this.type != EUnitType.Ally)
        {
            this.animator.SetBool("Ally", false);
        }
   
    }


    


    public abstract void GetAttackOptions();
    public abstract void UnitAttack(Unit target);
    public abstract void Selected();
    //protected abstract void HandleDeath();
    public virtual void HandleEaten()
    {
        Destroy(this.gameObject);
    }

    // public Unit(Unit unit)
    // {
    //     this.Accuracy = unit.Accuracy;
    //     this.Speed = unit.Speed;
    //     this.Attack = unit.Attack;
    //     this.Defense = unit.Defense;
    //     this.Experience = unit.Experience;
    // }
}
