using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Unit : MonoBehaviour {
    [SerializeField]
    private UnitInfo _info;
    public UnitInfo Info {
        get { return _info; }
    }

    [SerializeField]
    private Tile _tile;
    public Tile Tile {
        get { return _tile; }
        set { _tile = value; }
    }

    public int health = 0;

    private Attack attack;
    public void TakeDamage(int damage) {
        this.health -= damage;

        if(this.health <= 0) {
            this.Tile.isWalkable = true;
            UnitActionManager.Instance.RemoveUnitFromOrder(this);
        }
    }

    public void Heal(int heal) {
        if(heal > this.Info.jobClass.constitution) {
            int remove = heal - this.Info.jobClass.constitution;
            heal = remove;
        }
        Debug.Log(heal);
        this.health += heal;
    }
    public void Attack(Unit unit2, int roll) {
        int damage = roll - unit2.Info.jobClass.constitution;
        this.attack.UnitAttack(this, unit2, damage);
    }
    public void OnTap() {

        UnitActionManager.Instance.UnitSelect(this);
    }
    private void OnUnitTurnEnd() {
        UnitActionManager.Instance.ConfirmUnitActionDone();
    }
    public void Start() {
        this.attack = this.GetComponent<Attack>();
        UnitActionManager.Instance.StoreUnit(this);
        EventBroadcaster.Instance.AddObserver(EventNames.UnitActionEvents.ON_UNIT_TURN_END, this.OnUnitTurnEnd);

        this.Info.jobClass.GenerateStats(this);

        this.health = this.Info.jobClass.constitution;


    }
}
