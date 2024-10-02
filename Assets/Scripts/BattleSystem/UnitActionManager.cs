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

    private EBattleScene _battleScene;

    private Skill _skill;

    private EagleEye _eagleEye;

    [SerializeField]
    private float speed;
    public float Speed {  
        get { return this.speed; } 
    }

    [SerializeField]
    private BattleUI _battleUI;
    public BattleUI BattleUI {
        get { return _battleUI; }
    }

    private List<Unit> _Units = new List<Unit>();
    private List<Unit> _unitOrder = new List<Unit>();

    private Unit enemy = null;
    private EnemyMainAI _enemyAI;

    private bool OverEnemy = false;

    public bool Selected = false;

    private bool OnStart = true;

    private int _affectedStatValue = 0;

    public int numAttack = -1; // default value

    public bool hadMoved = false;
    public bool hadAttacked = false;
    public bool hadHealed = false;

    public bool OnAttack = false;
    public bool OnHeal = false;
    public bool OnMove = false;

    public bool Moving = false;

    // for storing the unit
    public void StoreUnit(Unit unit) {
        this._Units.Add(unit);
    }

    public Unit GetUnit() {
        return this._unitOrder[0];
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FOR UNIT MOVEMENT

    public bool AllyOnTileGoal(Tile endTile) {
        foreach (Unit ally in this._unitOrder) {
            if (ally.Type == EUnitType.Ally) {
                if (ally.Tile.TilePos == endTile.TilePos) {
                    return true;
                }
            }
        }

        return false;
    }
    public void TileTapped(Tile goalTile) {
        string bufDebufname = ""; //name
        EffectInfo terst = new EffectInfo(0, 0, EStatToEffect.NOTSET); //effectInfo
        if (!this.hadMoved && !this.AllyOnTileGoal(goalTile) && this.OnMove && !UnitActions.selectFlag) {

            PathFinding.Path = PathFinding.AStarPathFinding(this._unitOrder[0].Tile,
                         goalTile,
                         Range.GetTilesInMovement(this._unitOrder[0].Tile,
                                                         this._unitOrder[0].Speed)
                         );
            if(PathFinding.Path.Count > 0) {
                UnitActions.selectFlag = true;
                this._unitOrder[0].OnMove(true);
                this.Moving = true;
                /*Special Tile Detection*/
                if(goalTile.gameObject.tag == "SpecialTile")
                {
                    Debug.Log("Special Tile detected!" + goalTile.gameObject.name);

                    switch(goalTile.gameObject.name)
                    {
                        case "BuffTile(Clone)":
                        terst = new EffectInfo(3,2,EStatToEffect.ACCURACY); //effectInfo
                        bufDebufname = "BuffTile";
                        
                        break;

                        case "DebuffTile(Clone)":
                        terst = new EffectInfo(3,-2,EStatToEffect.SPEED);
                        // this._eagleEye.ApplyEffect(this._unitOrder[0],this._unitOrder[0],_skill.skillData); 
                        bufDebufname = "DebuffTile";
                        break;

                        case "RandomTile(Clone)":
                        this._affectedStatValue = UnityEngine.Random.Range(-6,6);
                        terst = new EffectInfo(3,this._affectedStatValue,EStatToEffect.ATTACK);
                        bufDebufname = "RandomTile";
                        // this._eagleEye.ApplyEffect(this._unitOrder[0],this._unitOrder[0],_skill.skillData);
                        break;

                        case "HazardTile(Clone)":
                        this._unitOrder[0].HP -= 1;
                        break;

                        default:
                        Debug.Log("B");
                        break;
                    }
                    // TODO put APPLYEFFECT into effectmanager, its reused in many codes throughtout...NUKE IEFFECTABLE
                    if(bufDebufname != "")
                    {
                        SkillDatabase.Instance.addSkill(terst, bufDebufname);
                        _unitOrder[0].EffectManager.ApplyTileEffect(_unitOrder[0], bufDebufname, terst.DURATION);
                    }
                    
                }
                



            }
            
        }
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
        this.NextUnitTurn();
    }
    private void OnAttackSelection() { // OVER HERE IS WHERE YOU'LL DO HIGHLIGHT?
        if(this.numAttack == 0) {
            Range.GetRange(this._unitOrder[0], this._unitOrder[0].BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
        if (this.numAttack == 1) {
            Range.GetRange(this._unitOrder[0], this._unitOrder[0].BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
        if (this.numAttack == 2) {
            Range.GetRange(this._unitOrder[0], this._unitOrder[0].BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
        if (this.numAttack == 3) {
            Range.GetRange(this._unitOrder[0], this._unitOrder[0].BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
        if (this.numAttack == 4) {
            Range.GetRange(this._unitOrder[0], this._unitOrder[0].BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void RemoveUnitFromOrder(Unit removedUnit) {

        foreach (Unit remove in this._unitOrder) {
            if (remove == removedUnit) {
                this._unitOrder.Remove(remove);
                remove.Tile.isWalkable = true;
                break;
            }
        }
        this._battleUI.UpdateTurnOrder(this._unitOrder);
        this.CheckEndCondition();

    }
    private void DecideTurnOrder() {
        this._unitOrder.AddRange(_Units);
        this._unitOrder.Sort((x, y) => y.Speed.CompareTo(x.Speed));
        this._battleUI.UpdateTurnOrder(this._unitOrder);
    }
    private void UpdateTile() { /////move to tileactions
       TileMapGenerator.Instance.UpdateTile();

        foreach (Unit unit in this._unitOrder) {
            if (unit.Type != EUnitType.Ally) {
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

        unit.OnTurn(!unit.Turn);

        this._unitOrder.RemoveAt(0);
        this._unitOrder[0].EffectManager.EffectTimer();
        
        this._unitOrder.Add(unit);
        this._battleUI.UpdateTurnOrder(this._unitOrder);

        this._unitOrder[0].OnTurn(!this._unitOrder[0].Turn);
                    
        UnitActions.SetCurrentTile(this._unitOrder[0].Tile, this._unitOrder[0].transform.position.y);

        this.hadMoved = false;
        this.hadAttacked = false;
        this.hadHealed = false;
        this._unitOrder[0].Tile.isWalkable = true;
        this.Selected = false;
        this.numAttack = -1;

        Range.UnHighlightTiles();

        if (this._unitOrder[0].Type != EUnitType.Ally) {

            EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);
            this.EnemyUnitAction();
        }
        //else {
        //    this._battleUI.ToggleActionBox();
        //}
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

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start() {

    }
    private void LateUpdate() {
        if (OnStart) {
            this.DecideTurnOrder();
            this.AssignUnitTile();
            this.UpdateTile();
            PathFinding.BattleScene = this._battleScene;
            Range.BattleScene = this._battleScene;
            this._battleUI.NextCharacterAvatar(this._unitOrder[0]);
            UnitActions.SetCurrentTile(this._unitOrder[0].Tile, this._unitOrder[0].transform.position.y);
            this._enemyAI = new EnemyMainAI(this._Units);

            this._unitOrder[0].OnTurn(!this._unitOrder[0].Turn);

            if (this._unitOrder[0].Type != EUnitType.Ally) {
                EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);
            }

            OnStart = false;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        { // right button
            UnitActions.UnSelectUnit();
        }

        if(Input.GetKeyUp(KeyCode.E)) {
            UnitActions.ConfirmMove();
        }

        if (PathFinding.Path == null) return;

        if (PathFinding.Path.Count > 0) {
            UnitActions.MoveCurrentUnit();
            //this.MoveCurrentUnit();
        }
        else {
            if (this.OnMove && !this.hadMoved) {
                Range.GetRange(this._unitOrder[0], this._unitOrder[0].Speed, "Move");
            }
            else if (this.OnHeal && !this.hadHealed) {
                Range.GetRange(this._unitOrder[0], this._unitOrder[0].BasicRange, "Heal");
            }
            else if (this.OnAttack && !this.hadAttacked) {
                this.OnAttackSelection();
            }
            else if (this.OverEnemy && this.enemy != null) {
                Range.GetRange(this.enemy, this.enemy.Speed, "Move");
            }
            else {
                Range.UnHighlightTiles();
            }
        }
    }

    private void CheckEndCondition() {
        int enemies = 0;
        int allies = 0;
        foreach(Unit unit in this._unitOrder) {
            if(unit.Type == EUnitType.Ally) {
                allies++;
            }
            if (unit.Type != EUnitType.Ally) {
                enemies++;
            }
        }

        if (allies == 0) {
            Debug.Log("Defeated!");
            this._battleUI.EndScreen(1);
        }

        if (enemies == 0) {
            Debug.Log("Level Cleared!");
            EventBroadcaster.Instance.PostEvent(EventNames.Enemy_Events.ON_ENEMY_DEFEATED);
            this._battleUI.EndScreen(2);
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

