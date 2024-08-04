using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Unit: MonoBehaviour { 
    public EUnitType type;

    public int Range;

    public int SPD;

    public int ATK;

    public int HP;

    public int DEF;

    public int MAXHP;

    [SerializeField]
    private Tile _tile;
    public Tile Tile {
        get { return _tile; }
        set { _tile = value; }
    }

    public void TakeDamage(int damage) {
        this.HP -= damage;
        this.HP = Mathf.Max(HP, 0);

        if (this.HP < 1) {
            this.Tile.isWalkable = true;
            UnitActionManager.Instance.RemoveUnitFromOrder(this);
        }
    }

    public void Heal(int heal) {

    }
    public virtual void Attack(Unit unit2) {

    }
    private void OnMouseUp() {
        UnitActionManager.Instance.UnitSelect(this);
    }
    protected void OnUnitTurnEnd() {
        UnitActionManager.Instance.ConfirmUnitActionDone();
    }
}
