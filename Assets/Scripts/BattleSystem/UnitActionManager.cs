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
        vignette = Camera.main.GetComponentInChildren<PostProcessVolume>();
        vignette.weight = 0.0f;
     

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
        }

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
        this._turnOrder.Remove((ITurnTaker)removedUnit);
        removedUnit.Tile.isWalkable = true;

        Parameters param = new Parameters();
        param.PutExtra(UNIT, removedUnit);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
    }
    public void DecideTurnOrder() {

        List<Unit> tempList = new List<Unit>();

        foreach(ITurnTaker taker in this.TurnOrder) {
            if (taker is Unit temp) {
                tempList.Add(temp);
            }
        }

        _Units = _Units.Except(tempList).ToList(); //filter out duplicates
        this._turnOrder.AddRange(_Units) ;
        this._turnOrder.Sort((x, y) => y.Speed.CompareTo(x.Speed));
        BattleUI.Instance.UpdateTurnOrder(this._turnOrder);
    }

    public void UnitTurn() {

        ITurnTaker firstTurn = this.GetFirstUnit();

        this.TurnOrder.Remove(firstTurn);
        this.TurnOrder.Add(firstTurn);

        Range.UnHighlightTiles();

        UnitAttackActions.EnemyListed = false;

        UnitActions.UpdateTile();

        this.SetUpTurn();
    }

    public void ResetCurrentUnit() {
        Unit unit = (Unit)this.GetFirstUnit();

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
        else if ((PathFinding.Path.Count <= 0) && (this.GetFirstUnit() is Unit unit))
        {
            if (this.OnAttack && !this.hadAttacked)
            {
                UnitAttackActions.ShowUnitsInSkillRange(this.numAttack, unit);
            }
            else if (this.OnMove && !this.hadMoved)
            {
                Range.GetRange(unit, unit.Move, "Move");
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

    //first thing that happens when Unit's turn starts
    private void SetUpTurn()
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
                    Debug.Log("Launch");
                    
                }
            //springs
            

            UnitActions.SetCurrentTile(unit.Tile, unit.transform.position.y);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.CURRENT_FOCUS);
            unit.OnMovement(true);
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

                    bEnemy = false;
                    bAlly = true;
                    BattleUI.Instance.ToggleActionBox();
                    break;


                case EUnitType.Enemy:

                    bEnemy = true;
                    bAlly = false;
                    EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);

                    this.EnemyUnitAction();
                    break;


                case EUnitType.SpecialTile:
                    ((ISpecialTile)unit).ApplyEffect();
                    break;


            }
        }

        //if (this._unitOrder[0].Type != EUnitType.Ally) {

        //    bEnemy = true;

        //    EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);

        //    this.EnemyUnitAction();
        //}
        //else
        //{
        //    bEnemy = false;
        //    BattleUI.Instance.ToggleActionBox();


        //}
        if (this.GetFirstUnit() is SpecialUnits sUnit) {
            sUnit.Action();
        }

    }
    public void OnStart() {
        this.DecideTurnOrder();
        UnitActions.AssignUnitTile();
        UnitActions.UpdateTile();
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

