using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class Unit: MonoBehaviour {

    protected List<Skill> skillList = new List<Skill>();

    public List<Skill> SKILLLIST
    {
        get { return skillList; }
    }

    protected EUnitType type;
    public EUnitType Type { 
        get { return this.type; }
        set { this.type = value;  }
    }

    protected string charName; // unit name
    public string Name { get { return this.charName; } }

    protected int acc; // hit
    public int Accuracy { get { return this.acc; } }

    protected int spd; // movement range
    public int Speed { get { return this.spd; } }

    protected int atk; // dmg
    public int Attack { get { return this.atk; } }

    protected int hp; // health
    public int HP { get { return this.hp; } }

    protected int def; // defense
    public int DEF { get { return this.def; } }

    protected int maxhp; // max health
    public int MAXHP { get { return this.maxhp; } }

    protected int basicrange = 2; // max health
    public int BasicRange { get { return this.basicrange; } }

    protected Animator animator;

    [SerializeField]
    private Tile _tile; // tile on the grid
    public Tile Tile {
        get { return _tile; }
        set { _tile = value; }
    }
    public void TakeDamage(int damage, Unit attacker) {
        if (damage == 1000)
        {
            attacker.atk += 1000;
        }

        if (this.isDodged(attacker))
        {
            Debug.Log("HP before :" + this.hp);
            int dmg = CalculateDamage(attacker);
            this.hp -= dmg;
            this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0
            Debug.Log("Dealt Damage: " + dmg);
            Debug.Log("HP after :" + this.hp);
        }
        else
        {
            Debug.Log("DODGE");
        }

        if (this.HP == 0) {
            this.Tile.isWalkable = true;
            UnitActionManager.Instance.RemoveUnitFromOrder(this);
            Debug.Log("Its Dead");
        }

        if (damage == 1000)
        {
            attacker.atk -= 1000;
        }
    }

    public int CalculateDamage(Unit attacker)
    {
        float dmg = 1 + attacker.Attack * (1 - (this.def + this.spd) / 100);
        dmg = (float)Math.Floor(dmg);
        return (int)dmg;
    }
    public bool isDodged(Unit attacker)
    {
            float chance = (100 - (attacker.Speed + attacker.Accuracy - this.spd) / 100);
            float x = UnityEngine.Random.Range(1,100);
            if(x < chance)
            {
            Debug.Log("Ods were: " + x + " to " + chance);
                return true;
            }
        return false;
    }

    public void Heal(Unit target) {

    }
    public void Defend(int damage) {

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

    protected void HandleDeath() {
        Destroy(this.gameObject);
    }

}
