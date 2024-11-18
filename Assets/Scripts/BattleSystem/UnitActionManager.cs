using EnemyAI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

public class UnitActionManager : MonoBehaviour{
    public static UnitActionManager Instance = null;

    private BattleManager battleManager = new BattleManager();

    public const string UNIT = "UNIT";

    private EBattleScene _battleScene;

    private Skill _skill;

    private EagleEye _eagleEye;

    [SerializeField]
    private float speed;
    public float Speed {  
        get { return this.speed; } 
    }

    private List<Unit> _Units = new List<Unit>();
    public List<Unit> UnitList {
        get { return _Units; }
    }
    private List<Unit> _unitOrder = new List<Unit>();
    public List<Unit> UnitOrder {
        get { return _unitOrder; }
    }

    private Unit enemy = null;
    private EnemyMainAI _enemyAI;

    private bool OverEnemy = false;

    private bool Start = true;

    public int numAttack = -1; // default value

    public bool hadMoved = false;
    public bool hadAttacked = false;

    public bool OnAttack = false;
    public bool OnMove = false;

    public bool Stayed = false;
    public bool Moving = false;
    
    // for storing the unit
    public void StoreUnit(Unit unit) {
        this._Units.Add(unit);
    }

    public Unit GetFirstUnit() {
        return this._unitOrder[0];
    }

    public bool IsGameEnd() {
        if (this.battleManager.GameEnd) {
            return true;
        }

        return false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public void EnemyUnitAction() {
        //UnitActions.OnAttack = true;

        //this._inRangeTiles = this._showRange.GetTilesInAttackMelee(this._unitOrder[0].Tile, this._unitOrder[0].BasicRange);
        //this.numAttack = 0;

        //foreach (Unit unit in this._Units)
        //{
        //    if (unit.Type == EUnitType.Ally)
        //    {
        //        this.UnitSelect(unit);
        //    }
        //}

        PathFinding.Path = this._enemyAI.TakeTurn(this._unitOrder[0]);

        this.StartCoroutine(this.EnemyWait(1.0f));
    }    
    private IEnumerator EnemyWait(float seconds) {
        this.OnAttack = false;
        this.OnMove = false;
        yield return new WaitForSeconds(seconds);
        PathFinding.Path.Clear();
        EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.ENABLE_CLICKS);
        this.NextTurn();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void RemoveUnitFromOrder(Unit removedUnit) {
        this._unitOrder.Remove(removedUnit);
        removedUnit.Tile.isWalkable = true;

        this.battleManager.EndCondition(removedUnit);
    }
    private void DecideTurnOrder() {
        this._unitOrder.AddRange(_Units);
        this._unitOrder.Sort((x, y) => y.Speed.CompareTo(x.Speed));
        BattleUI.Instance.UpdateTurnOrder(this._unitOrder);
    }

    public void NextTurn() {
        BattleUI.Instance.OnEndTurn();

    }
    public void UnitTurn() {
   
        Range.UnHighlightTiles();

        UnitAttackActions.EnemyListed = false;

        UnitActions.UpdateTile();

        this.SetUpTurn();
    }

    public void ResetCurrentUnit() {
        Unit unit = this.GetFirstUnit();
        unit.GetComponent<BoxCollider>().enabled = true;
        unit.OnMovement(false);
        unit.OnTurn(false);

        Parameters param = new Parameters();
        param.PutExtra(UNIT, unit);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.HIDE_HP, param);

        this._unitOrder.Remove(unit);
        this._unitOrder.Add(unit);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void LateUpdate() {
        this.OnStart();

        if (Input.GetKeyUp(KeyCode.R) && UnitActions.stepFlag) {
            BattleUI.Instance.ResetButtonState(this.numAttack);
            UnitActions.ResetPosition();
        }

        if (PathFinding.Path == null) return;

        if (this.Stayed) {
            this.GetFirstUnit().OnMovement(false);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
            this.Stayed = false;
            this.OnMove = false;
        }

        if (PathFinding.Path.Count > 0) {
            UnitActions.MoveCurrentUnit();
        }
        else {
            if (this.OnMove && !this.hadMoved) {
                Range.GetRange(this._unitOrder[0], this._unitOrder[0].Move, "Move");
            }
            else if (this.OnAttack && !this.hadAttacked) {
                UnitAttackActions.ShowUnitsInSkillRange(this.numAttack);
            }
            else if (this.OverEnemy && this.enemy != null) {
                Range.GetRange(this.enemy, this.enemy.Speed, "Move");
            }
            else {
                Range.UnHighlightTiles();
            }
        }
    }
    private void SetUpTurn() {
        Parameters param = new Parameters();
        param.PutExtra(UNIT, this.GetFirstUnit());

        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);

        BattleUI.Instance.UpdateTurnOrder(this._unitOrder);
        BattleUI.Instance.NextUnitSkills(this.GetFirstUnit());
        UnitActions.SetCurrentTile(this.GetFirstUnit().Tile, this.GetFirstUnit().transform.position.y);
        this.GetFirstUnit().EffectManager.EffectTimer();
        this.GetFirstUnit().OnMovement(true);
        this.GetFirstUnit().OnTurn(true);
        this.GetFirstUnit().GetComponent<BoxCollider>().enabled = false;

        this.Stayed = false;
        this.OnMove = true;
        this.hadMoved = false;
        this.hadAttacked = false;
        this.GetFirstUnit().Tile.isWalkable = true;
        this.numAttack = -1;

        // reset and update attackable list
        UnitAttackActions.SetAttackableList();

        if (this._unitOrder[0].Type != EUnitType.Ally) {

            EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);
            this.EnemyUnitAction();
        }


    }
    private void OnStart() {
        if (Start) {
            this.DecideTurnOrder();
            UnitActions.AssignUnitTile();
            UnitActions.UpdateTile();
            PathFinding.BattleScene = this._battleScene;
            Range.BattleScene = this._battleScene;
            this._enemyAI = new EnemyMainAI(this._Units);

            this.SetUpTurn();

            Start = false;

            this.battleManager.SetNums();
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

