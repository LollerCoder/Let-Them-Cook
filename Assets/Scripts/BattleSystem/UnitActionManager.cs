//using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Unity.VisualScripting;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitActionManager : MonoBehaviour {
    public static UnitActionManager Instance = null;

    public EBattleScene _battleScene;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Pointer arrow;

    [SerializeField]
    private BattleUIController _battleUIController;

    private List<Unit> _Units = new List<Unit>();
    private List<Unit> _unitOrder = new List<Unit>();

    private List<Tile> _path = new List<Tile>();
    private List<Tile> _inRangeTiles = new List<Tile>();

    private PathFinding _pathFinding;
    private Range _showRange;

    public bool hadMoved = false;
    public bool hadAttacked = false;
    public bool hadHealed = false;

    public bool Attack = false;
    public bool Heal = false;
    public bool Move = false;

    private bool OnStart = true;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FOR UNIT MOVEMENT
    public void TileTapped(Tile goalTile) {
        if (!this.hadMoved && !this.AllyOnTileGoal(goalTile) && this.Move) {

            this._path = this._pathFinding.AStarPathFinding(this._unitOrder[0].Tile,
                         goalTile,
                         this._showRange.GetTilesInMovement(this._unitOrder[0].Tile,
                                                         this._unitOrder[0].Info.movementRange)
                         );
        }
    }
    private void MoveCurrentUnit() {
        float step = this.speed * Time.deltaTime;

        float previousY = this._unitOrder[0].transform.position.y;
        Vector3 currentPos = this._unitOrder[0].transform.position;

        this._unitOrder[0].transform.position = Vector3.MoveTowards(currentPos, this._path[0].transform.position, step);
        this._unitOrder[0].transform.position = new Vector3(this._unitOrder[0].transform.position.x,
                                                            previousY,
                                                            this._unitOrder[0].transform.position.z);

        Vector2 unitPos = new Vector2(this._unitOrder[0].transform.position.x, this._unitOrder[0].transform.position.z);
        Vector2 tilePos = new Vector2(this._path[0].transform.position.x, this._path[0].transform.position.z);
        if (Vector2.Distance(unitPos, tilePos) < 0.1f) {
            this._unitOrder[0].transform.position = new Vector3(this._path[0].transform.position.x,
                                                            previousY,
                                                            this._path[0].transform.position.z);
            this._unitOrder[0].Tile = this._path[0];
            this._path.RemoveAt(0);
        }

        if (this._path.Count < 1) {
            this.hadMoved = true;
            this.Move = false;
        }
    }
    private bool AllyOnTileGoal(Tile endTile) {
        foreach (Unit ally in this._unitOrder) {
            if (ally.Info.type == EUnitType.Ally) {
                if (ally.Tile.TilePos == endTile.TilePos) {
                    return true;
                }
            }
        }

        return false;
    }
    private void GetRangeTiles() {
        foreach (Tile tile in this._inRangeTiles) {
            tile.UnHighlightTile();
        }

        Unit currentUnit = this._unitOrder[0];

        this._inRangeTiles = this._showRange.GetTilesInMovement(currentUnit.Tile, currentUnit.Info.movementRange);

        foreach (Tile tile in this._inRangeTiles) {
            tile.HighlightWalkableTile();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // FOR UNIT ACTION

    public void UnitHeal() {
        if (!this.hadHealed) {
            if (this._unitOrder[0].health == this._unitOrder[0].Info.jobClass.constitution) {
                this.Heal = false;
                Debug.Log("no heal");
                return;
            }

            this.hadHealed = true;
            this.Heal = false;
            int roll = this.Roll();
            this._unitOrder[0].Heal(roll);
        }
        
    }

    private void ConfirmAttack(Unit target) {
        int roll;

        if (this._unitOrder[0].Info.type != EUnitType.Ally) {
            roll = this.Roll();
        }
        else {
            // roll ui here
            roll = this.Roll();
        }

        if (roll > target.Info.jobClass.constitution) {
            this._unitOrder[0].Attack(target, roll);
        }
        else {
            Debug.Log("MISS");
        }
        
        this.Attack = false;
        this.hadAttacked = true;
    }
    public void ConfirmUnitActionDone() {
        this.NextUnitTurn();
    }
    public void UnitSelect(Unit selectedUnit) {

        if (this.IsUnitAttackable(selectedUnit) && this.Attack) {
            this.ConfirmAttack(selectedUnit);
        }
    }
    private void GetMeleeAttackTiles() {
        this.UnHighlightTiles();

        Unit currentUnit = this._unitOrder[0];

        this._inRangeTiles = this._showRange.GetTilesInAttackMelee(currentUnit.Tile, currentUnit.Info.attackRange);

        foreach (Tile tile in this._inRangeTiles) {
            tile.HighlightAttackableTile();
        }
    }
    private void GetRangeAttackTiles() {
        this.UnHighlightTiles();

        Unit currentUnit = this._unitOrder[0];

        this._inRangeTiles = this._showRange.GetTilesInAttackRange(currentUnit.Tile, currentUnit.Info.attackRange);

        foreach (Tile tile in this._inRangeTiles) {
            tile.HighlightAttackableTile();
        }
    }
    private bool IsUnitAttackable(Unit selectedUnit) {
        foreach (Tile tile in this._inRangeTiles) {
            if (selectedUnit.Tile == tile) {
                return true;
            }
        }
        return false;
    }
    public void EnemyUnitAction() {
        this.Attack = true;

        if (this._unitOrder[0].Info.unitType == EUnitAttackType.Melee) {
            this._inRangeTiles = this._showRange.GetTilesInAttackMelee(this._unitOrder[0].Tile, this._unitOrder[0].Info.attackRange);
        }
        if (this._unitOrder[0].Info.unitType == EUnitAttackType.Range) {
            this._inRangeTiles = this._showRange.GetTilesInAttackRange(this._unitOrder[0].Tile, this._unitOrder[0].Info.attackRange);
        }

        foreach (Unit unit in this._Units) {
            if (unit.Info.type == EUnitType.Ally) {
               this.UnitSelect(unit);
            }
        }

        this.StartCoroutine(this.EnemyWait(1.0f));
    }    
    private IEnumerator EnemyWait(float seconds) {
        this.Attack = false;
        yield return new WaitForSeconds(seconds);
        EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.ENABLE_CLICKS);
        this.NextUnitTurn();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void RemoveUnitFromOrder(Unit removedUnit) {

        foreach (Unit remove in this._unitOrder) {
            if (remove == removedUnit) {
                this._unitOrder.Remove(remove);
                remove.Tile.isWalkable = true;
                remove.gameObject.SetActive(false);
                break;
            }
        }

        this.CheckEndCondition();

    }
    public void StoreUnit(Unit unit) {
        this._Units.Add(unit);
    }
    private void DecideTurnOrder() {
        this._unitOrder.AddRange(_Units);
        this._unitOrder.Sort((x, y) => y.Info.jobClass.dexterity.CompareTo(x.Info.jobClass.dexterity));
    }
    private void UpdateTile() {
       TileMapGenerator.Instance.UpdateTile();

        foreach (Unit unit in this._unitOrder) {
            if (unit.Info.type != EUnitType.Ally) {
                unit.Tile.isWalkable = false;
            }
            else {
                unit.Tile.isWalkable = true;
            }
        }
    }
    public void NextUnitTurn() {
        this.UpdateTile();
        Unit unit = this._unitOrder[0];
        this._unitOrder.RemoveAt(0);
        this._unitOrder.Add(unit);
        this.hadMoved = false;
        this.hadAttacked = false;
        this.hadHealed = false;
        this._unitOrder[0].Tile.isWalkable = true;

        this.UnHighlightTiles();
        if (this._unitOrder[0].Info.type != EUnitType.Ally) {

            EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);
            this.EnemyUnitAction();
        }
    }
    private void AssignUnitTile() {

        var map = TileMapGenerator.Instance.TileMap;

        map = TileMapGenerator.Instance.TileMap;

        Vector2Int unitPos = new Vector2Int();
        foreach (Unit unit in this._Units) {
            unitPos = new Vector2Int(
                (int)unit.transform.position.x,
                (int)unit.transform.position.z
            );

            if (map.ContainsKey(unitPos)) {
                unit.Tile = map[unitPos];
            }

        }
    }
    private void UnHighlightTiles() {
        foreach (Tile tile in this._inRangeTiles) {
            tile.UnHighlightTile();
        }
    }
    private int Roll() {
        int roll = UnityEngine.Random.Range(1,20);

        return roll;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Start() {

    }
    private void LateUpdate() {
        if (OnStart) {
            this.DecideTurnOrder();
            this.AssignUnitTile();
            this.UpdateTile();
            this._pathFinding = new PathFinding();
            this._showRange = new Range();
            this._pathFinding.BattleScene = this._battleScene;
            this._showRange.BattleScene = this._battleScene;

            if (this._unitOrder[0].Info.type != EUnitType.Ally) {
                EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);
            }

            OnStart = false;
        }

        arrow.UpdatePointer(this._unitOrder[0]);
        if (this._path.Count > 0) {
            this.MoveCurrentUnit();
        }
        else {
            if (this.Move && !this.hadMoved) {
                this.GetRangeTiles();
            }
            else if(this.Attack && !this.hadAttacked) {
                if(this._unitOrder[0].Info.unitType == EUnitAttackType.Melee) {
                    this.GetMeleeAttackTiles();
                }
                if(this._unitOrder[0].Info.unitType == EUnitAttackType.Range) {
                    this.GetRangeAttackTiles();
                }
            }
            else {
                this.UnHighlightTiles();
            }
        }
    }

    private void CheckEndCondition() {
        int enemies = 0;
        bool alive = false;
        foreach(Unit unit in this._unitOrder) {
            if(unit.Info.jobClass.className == EJobClass.Sage) {
                alive = true;
            }
            if (unit.Info.type != EUnitType.Ally) {
                enemies++;
            }
        }

        if (alive == false) {
            Debug.Log("Defeated! DED MC");
            this._battleUIController.EndScreen(1);
        }

        if (enemies == 0) {
            Debug.Log("Victory");
            EventBroadcaster.Instance.PostEvent(EventNames.Enemy_Events.ON_ENEMY_DEFEATED);
            this._battleUIController.EndScreen(2);
        }

    }

    public void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else if(Instance != null) {
            Destroy(this.gameObject);
        }
    }
}
