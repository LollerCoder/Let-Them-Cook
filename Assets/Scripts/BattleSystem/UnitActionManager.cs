using EnemyAI;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

public class UnitActionManager : MonoBehaviour
{
    public static UnitActionManager Instance = null;

    public const string UNIT = "UNIT";

    PostProcessVolume vignette;

    public bool bEnemy = false;
    public bool bAlly = false;

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

    private List<ITurnTaker> _turnOrder = new List<ITurnTaker>();
    public List<ITurnTaker> TurnOrder
    {
        get { return _turnOrder; }
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

    public ITurnTaker GetFirstUnit() {
        return this._turnOrder[0];
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Start()
    {
        Debug.Log(" Unit action manager start!");
        vignette = Camera.main.GetComponentInChildren<PostProcessVolume>();
        vignette.weight = 0.0f;
     
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ADDED_UNITS_SELECTED, this.UpdateEnemyAIList);
        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.HOSTAGE_FREE, this.UpdateEnemyAIList);

        EventBroadcaster.Instance.AddObserver(EventNames.UnitActionEvents.ON_DESTINATION_REACHED, this.EnemyAIEndTurn);
        EventBroadcaster.Instance.AddObserver(EventNames.UnitActionEvents.ON_ENEMY_END_TURN, this.EnemyAIEndTurn);
    }
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

        if (this.GetFirstUnit() is Unit enemy) {
            PathFinding.Path = this._enemyAI.TakeTurn(enemy);
            if (PathFinding.Path == null) this.EnemyAIEndTurn();
        }

        //this.StartCoroutine(this.EnemyWait(2.0f)); // fix this instead of doing a coroutine, do the next turn whenever the enemy has done all of their actions
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

    public void EnemyAIEndTurn()
    {
        if (this.GetFirstUnit() is Unit enemy)
        {
            //Debug.Log("Type is unit");
            if (enemy.Type != EUnitType.Enemy) return;

        }
        this.OnAttack = false;
        this.OnMove = false;

        if(PathFinding.Path != null) PathFinding.Path.Clear();
        EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.ENABLE_CLICKS);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void AddUnit(Unit unit)
    {
        if (_Units.Contains(unit)) return;
        _Units.Add(unit);
    }

    public void RemoveUnitFromOrder(ITurnTaker removedUnit) {
        if (removedUnit is SpecialUnits _unit) {
            this._turnOrder.Remove(_unit);
        }
        if (removedUnit is Unit unit) {
            this._Units.Remove(unit);
            this._turnOrder.Remove(unit);
            unit.Tile.isWalkable = true;

            this._enemyAI.UpdateAllyUnits(_Units.FindAll(u => u.Type == EUnitType.Ally));

            Parameters param = new Parameters();
            param.PutExtra(UNIT, unit);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
        }
    }
    public void DecideTurnOrder() {

        //List<Unit> tempList = new List<Unit>();

        //foreach(ITurnTaker taker in this.TurnOrder) {
        //    if (taker is Unit temp) {
        //        tempList.Add(temp);
        //    }
        //}

        //_Units = _Units.Except(tempList).ToList(); //filter out duplicates

        RemoveDuplicates();

        //remove all units in turn order
        this._turnOrder.RemoveAll(taker => taker is Unit);

        this._turnOrder.AddRange(_Units);

        this._turnOrder.Sort((x, y) => y.Speed.CompareTo(x.Speed));
        BattleUI.Instance.UpdateTurnOrder(this._turnOrder);
    }

    private void RemoveDuplicates()
    {
        List<Unit> tempUnitList = new List<Unit>(_Units);

        foreach (ITurnTaker taker in this.TurnOrder)
        {
            if (taker is Unit temp)
            {
                tempUnitList.Add(temp);
            }
        }

        List<Unit> toRemove = new List<Unit>();

        //getting the duplicates
        foreach (Unit u in tempUnitList)
        {
            if (tempUnitList.FindAll(un => un == u).Count > 1)
            {
                toRemove.Add(u);
            }
        }

        //removing the duplicates
        foreach (Unit u in toRemove)
        {
            tempUnitList.RemoveAll(un => un == u);
            tempUnitList.Add(u);
        }

        _Units = tempUnitList;

    }

    private void PrintUnits()
    {
        Debug.Log("=============== Turn takers: ");
        foreach (ITurnTaker tt in _turnOrder)
        {
            Debug.Log(tt);
        }
        Debug.Log("=============== " + _Units.Count + " Units: ");
        foreach (Unit u in _Units)
        {
            Debug.Log(u);
        }
    }

    public void UnitTurn() {

        ITurnTaker firstTurn = this.GetFirstUnit();
        if (firstTurn is Unit unit) {
            unit.Tile.UnHighlightTile();

        }
        this.TurnOrder.Remove(firstTurn);
        this.TurnOrder.Add(firstTurn);

        Range.UnHighlightTiles();

        UnitAttackActions.EnemyListed = false;

        UnitActions.UpdateTile();

        this.StartCoroutine(this.SetUpTurn());
    }

    public void ResetCurrentUnit() {
        Unit unit = (Unit)this.GetFirstUnit();
        UnitActions.CheckVegetableOnTile(unit);
        unit.GetComponent<BoxCollider>().enabled = true;
        unit.OnMovement(false);
        unit.OnTurn(false);
        Parameters param = new Parameters();
        param.PutExtra(UNIT, unit);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.HIDE_HP, param);

        unit.EffectManager.EffectTimer();
        unit.EffectManager.ArrowHider(unit);
    }


    void Update()
    {
        if (bEnemy)
        {
            if (vignette.weight < 0.90)  vignette.weight  +=  3.0f * Time.deltaTime;
            else vignette.weight = 1.0f;
        }

        else if (!bEnemy)
        {
            if (vignette.weight > 0.1) {vignette.weight  -=  2.0f  * Time.deltaTime;}
            else vignette.weight = 0.0f;
        }
            

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void LateUpdate() {
        if (this.TurnOrder.Count <= 0 || this.GetFirstUnit() is SpecialUnits) {
            return;
        }

        if (Input.GetKeyUp(KeyCode.R) && UnitActions.stepFlag)
        {
            BattleUI.Instance.ResetButtonState(this.numAttack);
            UnitActions.ResetPosition();
        }

        if (PathFinding.Path == null) return;

        if (PathFinding.Path.Count > 0) {
            UnitActions.MoveCurrentUnit();
        }
        else if ((PathFinding.Path.Count <= 0))
        {
            if (this.OverEnemy && this.enemy != null)
            {
                Range.GetRange(this.enemy, this.enemy.Speed, RangeType.WALK);
            }
        }
    }

    //first thing that happens when Unit's turn starts
    private IEnumerator SetUpTurn()
    {
        BattleUI.Instance.UpdateTurnOrder(this.TurnOrder);

        if (this.GetFirstUnit() is Unit unit) { 
            Parameters param = new Parameters();
            param.PutExtra(UNIT, unit);

            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);

            if (unit.Type == EUnitType.Ally) BattleUI.Instance.NextUnitSkills(unit);

            //springs
                foreach(Tile tile in TileMapManager.Instance.TileMap.Values)
                {
                    SpringTile st = tile.gameObject.GetComponent<SpringTile>();

                    if (st != null) { st.ApplyOnUnitStart(); }
                    //Debug.Log("Launch");
                    
                }
            //springs
            

            UnitActions.SetCurrentTile(unit.Tile, unit.transform.position.y);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.CURRENT_FOCUS);
            unit.OnTurn(true);
            unit.GetComponent<BoxCollider>().enabled = false;

            this.OnMove = true;
            this.hadMoved = false;
            this.hadAttacked = false;
            unit.Tile.isWalkable = true;
            this.numAttack = -1;

            //apply effects
            unit.ApplyEffects();

            // reset and update attackable list
            UnitAttackActions.SetAttackableList();

            switch (unit.Type) {
                case EUnitType.Ally:
                    if (unit.OnWeapon) {

                    }
                    bEnemy = false;
                    bAlly = true;
                    BattleUI.Instance.ToggleActionBox();
                    Range.GetRange(unit, unit.Move, RangeType.WALK);
                    break;


                case EUnitType.Enemy:

                    bEnemy = true;
                    bAlly = false;
                    EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);
                    yield return new WaitForSeconds(0.8f);
                    this.EnemyUnitAction();
                    break;


                case EUnitType.SpecialTile:
                    ((ISpecialTile)unit).ApplyEffect();
                    break;

                default:
                    break;

            }
        }

        if (this.GetFirstUnit() is SpecialUnits sUnit) {
            this.bEnemy = true;
            this.StartCoroutine(sUnit.Turn());
        }

    }

    //getting closest unit to
    public Unit GetClosestUnit(Unit origin, EUnitType type)
    {
        Unit closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 origPos = origin.transform.position;

        foreach (Unit u in this._turnOrder)
        {
            Debug.Log("CURRENT UNIT TO CHECK " + u);
            if (origin == u || u.Type == type) continue;

            float curDistance = Vector3.Distance(origPos, u.transform.position);
            Debug.Log("Distance: " + curDistance);
            if (curDistance < minDistance)
            {
                Debug.Log("UPDATING CLOSEST: " + u + " distance: " + curDistance);
                minDistance = curDistance;
                closest = u;
            }
        }

        Debug.Log("CLOSEST UNIT: " + closest);
        return closest;
    }

    public void OnStart() {
        CameraMovement.inCutscene = false;
        this.DecideTurnOrder();
        UnitActions.AssignUnitTile();
        UnitActions.UpdateTile();
        this._enemyAI = new EnemyMainAI(this._Units);

        this.StartCoroutine(this.SetUpTurn());

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.ON_START);  
        
    }

    public void UpdateEnemyAIList()
    {
        //Debug.Log("Updated ally units on enemy AI");
        this._enemyAI.UpdateAllyUnits(
                _Units.FindAll(u => u.Type == EUnitType.Ally)
            );
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this);
        }
    }
}

