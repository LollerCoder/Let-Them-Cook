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

public class UnitActionManager : MonoBehaviour
{
    public static UnitActionManager Instance = null;

    public const string UNIT = "UNIT";

    private EBattleScene _battleScene;

    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return this.speed; }
    }

    [SerializeField]
    private List<Unit> _Units = new List<Unit>();
    public List<Unit> UnitList
    {
        get { return _Units; }
    }
    private List<Unit> _unitOrder = new List<Unit>();
    public List<Unit> UnitOrder
    {
        get { return _unitOrder; }
    }

    private Unit enemy = null;
    private EnemyMainAI _enemyAI;

    private bool OverEnemy = false;

    public int numAttack = -1; // default value

    public bool hadMoved = false;
    public bool hadAttacked = false;

    public bool OnAttack = false;
    public bool OnMove = false;

    public bool Moving = false;

    // for storing the unit
    public void StoreUnit(Unit unit) {
        this._Units.Add(unit);
    }

    public Unit GetFirstUnit() {
        return this._unitOrder[0];
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void EnemyUnitAction()
    {
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

        this.StartCoroutine(this.EnemyWait(2.0f)); // fix this instead of doing a coroutine, do the next turn whenever the enemy has done all of their actions
    }
    private IEnumerator EnemyWait(float seconds)
    {
        this.OnAttack = false;
        this.OnMove = false;
        yield return new WaitForSeconds(seconds);
        PathFinding.Path.Clear();
        EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.ENABLE_CLICKS);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void RemoveUnitFromOrder(Unit removedUnit) {
        this._unitOrder.Remove(removedUnit);
        removedUnit.Tile.isWalkable = true;

        Parameters param = new Parameters();
        param.PutExtra(UNIT, removedUnit);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
    }
    private void DecideTurnOrder() {
        this._unitOrder.AddRange(_Units);
        this._unitOrder.Sort((x, y) => y.Speed.CompareTo(x.Speed));
        BattleUI.Instance.UpdateTurnOrder(this._unitOrder);
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

        unit.EffectManager.EffectTimer();
        unit.EffectManager.ArrowHider(unit);
        this._unitOrder.Remove(unit);
        this._unitOrder.Add(unit);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void LateUpdate() {
        if (Input.GetKeyUp(KeyCode.R) && UnitActions.stepFlag)
        {
            BattleUI.Instance.ResetButtonState(this.numAttack);
            UnitActions.ResetPosition();
        }

        if (PathFinding.Path == null) return;

        if (PathFinding.Path.Count > 0) {
            UnitActions.MoveCurrentUnit();
        }
        else
        {
            if (this.OnAttack && !this.hadAttacked)
            {
                UnitAttackActions.ShowUnitsInSkillRange(this.numAttack);
            }
            else if (this.OnMove && !this.hadMoved)
            {
                Range.GetRange(this._unitOrder[0], this._unitOrder[0].Move, "Move");
            }
            else if (this.OverEnemy && this.enemy != null)
            {
                Range.GetRange(this.enemy, this.enemy.Speed, "Move");
            }
            else
            {
                Range.UnHighlightTiles();
            }
        }
    }
    private void SetUpTurn()
    {
        Parameters param = new Parameters();
        param.PutExtra(UNIT, this.GetFirstUnit());

        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);

        BattleUI.Instance.UpdateTurnOrder(this._unitOrder);
        BattleUI.Instance.NextUnitSkills(this.GetFirstUnit());
        UnitActions.SetCurrentTile(this.GetFirstUnit().Tile, this.GetFirstUnit().transform.position.y);
        
        
        this.GetFirstUnit().OnMovement(true);
        this.GetFirstUnit().OnTurn(true);
        this.GetFirstUnit().GetComponent<BoxCollider>().enabled = false;

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
        else
        {
            BattleUI.Instance.ToggleActionBox();
        }


    }
    public void OnStart() {
        this.DecideTurnOrder();
        UnitActions.AssignUnitTile();
        UnitActions.UpdateTile();
        PathFinding.BattleScene = this._battleScene;
        Range.BattleScene = this._battleScene;
        this._enemyAI = new EnemyMainAI(this._Units);

        this.SetUpTurn();

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.ON_START);  
        
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }
}

