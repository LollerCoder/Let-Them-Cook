using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class Unit: MonoBehaviour { 
    protected EUnitType type;
    public EUnitType Type { get { return this.type; } }

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

    [SerializeField]
    private Tile _tile; // tile on the grid
    public Tile Tile {
        get { return _tile; }
        set { _tile = value; }
    }
    public void TakeDamage(int damage) {
        this.hp -= damage;
        this.hp = Mathf.Max(HP, 0); // make sure it will never go past 0

        if (this.HP == 0) {
            this.Tile.isWalkable = true;
            UnitActionManager.Instance.RemoveUnitFromOrder(this);
        }
    }

    public void Heal(Unit target) {

    }
    public void Defend(int damage) {

    }
    public abstract void UnitAttack(Unit target);
    public abstract void OnMouseUp();
    public abstract void OnMouseEnter();
    public abstract void OnMouseExit();
    protected void HandleDeath() {
        Destroy(this.gameObject);
    }
}
