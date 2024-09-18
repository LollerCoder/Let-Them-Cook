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

    [SerializeField]
    private BattleUI _battleUI;

    private List<Unit> _Units = new List<Unit>();
    private List<Unit> _unitOrder = new List<Unit>();

    private List<Tile> _path = new List<Tile>();
    private List<Tile> _inRangeTiles = new List<Tile>();

    private PathFinding _pathFinding;
    private Range _showRange;

    private Unit enemy = null;
    private EnemyMainAI _enemyAI;

    private bool OverEnemy = false;

    public bool Selected = false;

    public bool hadMoved = false;
    public bool hadAttacked = false;
    public bool hadHealed = false;
    public bool hadDefend = false;

    public bool OnAttack = false;
    public bool OnHeal = false;
    public bool OnMove = false;
    public bool OnDefend = false;

    private bool OnStart = true;

    private int _affectedStatValue = 0;

    public int numAttack = -1; // default value

    // for storing the unit
    public void StoreUnit(Unit unit) {
        this._Units.Add(unit);
    }

    public Unit GetUnit() {
        return this._unitOrder[0];
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FOR UNIT MOVEMENT
    public void TileTapped(Tile goalTile) {
        string bufDebufname = ""; //name
        EffectInfo terst = new EffectInfo(0, 0, EStatToEffect.NOTSET);//effectInfo
        if (!this.hadMoved && !this.AllyOnTileGoal(goalTile) && this.OnMove) {

            this._path = this._pathFinding.AStarPathFinding(this._unitOrder[0].Tile,
                         goalTile,
                         this._showRange.GetTilesInMovement(this._unitOrder[0].Tile,
                                                         this._unitOrder[0].Speed)
                         );
            if(this._path.Count > 0) {
                this.hadMoved = true;
                this._unitOrder[0].OnMove(true);
                
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
                }


                if (_unitOrder[0].EffectManager.EFFECTLIST.ContainsKey(bufDebufname)) //name this is just to prevent same skill effect stacking
                {
                    _unitOrder[0].EffectManager.EFFECTLIST[bufDebufname].DURATION = terst.DURATION;
                    
                }
                else // the actual way of adding a debuff/buff to a unit the rest is already handled by UnitActionManager....Somewhere
                {
                    _unitOrder[0].EffectManager.EFFECTLIST.Add(bufDebufname, terst);
                    Debug.Log("Target affected");
                }



            }
            
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

        if (this._path.Count <= 1) {
            this.OnMove = false;
            this._unitOrder[0].OnMove(false);
        }
    }
    private bool AllyOnTileGoal(Tile endTile) {
        foreach (Unit ally in this._unitOrder) {
            if (ally.Type == EUnitType.Ally) {
                if (ally.Tile.TilePos == endTile.TilePos) {
                    return true;
                }
            }
        }

        return false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // FOR UNIT ACTION

    public void UnitDefend() {
        this._unitOrder[0].OnDefend();

        this.hadAttacked = true;
        this.hadMoved = true;
        this.hadHealed = true;
        this.OnDefend = false;
        this.hadDefend = true;
    }

    private void UnitHeal() {
        this.UnHighlightTiles();

        Unit currentUnit = this._unitOrder[0];

        Debug.Log("UnitHeal " + currentUnit.Name + " Healed");

        foreach(Tile tile in this._inRangeTiles) {
            tile.HighlightEatableTile();
        }
        
    }

    private void ConfirmEat(Unit target) {
        if (!this.hadHealed) {
            if (this._unitOrder[0].HP == this._unitOrder[0].MAXHP) {
                this.OnHeal = false;
                Debug.Log("no heal");
                return;
            }

            this._unitOrder[0].Heal(target);
            this.hadHealed = true;
            this.OnHeal = false;
        }
    }
    private void ConfirmAttack(Unit target, int Skill) {

        switch (Skill)
        {
            case 0:
                if (this._unitOrder[0].SKILLLIST[Skill] != null)
                {
                    Debug.Log(this._unitOrder[0].SKILLLIST[Skill]);
                    Debug.Log(target.Name);
                    SkillDatabase.Instance.applySkill(this._unitOrder[0].SKILLLIST[Skill], target, this._unitOrder[0]);
                    
                }
                break;
            case 1:
                if (this._unitOrder[0].SKILLLIST[Skill] != null)
                {
                    SkillDatabase.Instance.applySkill(this._unitOrder[0].SKILLLIST[Skill], target, this._unitOrder[0]);

                }
                break;
            case 2:
                if (this._unitOrder[0].SKILLLIST[Skill] != null)
                {
                    SkillDatabase.Instance.applySkill(this._unitOrder[0].SKILLLIST[Skill], target, this._unitOrder[0]);

                }
                break;
            case 3:
                if (this._unitOrder[0].SKILLLIST[Skill] != null)
                {
                    SkillDatabase.Instance.applySkill(this._unitOrder[0].SKILLLIST[Skill], target, this._unitOrder[0]);

                }
                break;
            case 4:
                if (this._unitOrder[0].SKILLLIST[Skill] != null)
                {
                    SkillDatabase.Instance.applySkill(this._unitOrder[0].SKILLLIST[Skill], target, this._unitOrder[0]);

                }
                break;

        }

        
        Debug.Log("Attacked Target " + target.name);
        if(Skill == 1)
        {
            Debug.Log("10 dmg applied");
        }
        this.OnAttack = false;
        this.hadAttacked = true;

        if (this._unitOrder[0].Type == EUnitType.Ally) {
            this._battleUI.ToggleSkillBox();
        }
        
    }
    public void UnitHover(Unit selectedUnit) {
        if (this._unitOrder[0] == selectedUnit
            && this._unitOrder[0].Type == EUnitType.Ally
            && !this.Selected
            && !this.OnAttack
            && !this.OnHeal
            && !this.OnDefend) {

            this.OnMove = !this.OnMove;

        }
       
        //else if (this._unitOrder[0] != selectedUnit && 
        //        selectedUnit.Type != EUnitType.Ally) {
        //    this.OverEnemy = !this.OverEnemy;
        //    this.enemy = selectedUnit;
        //}

        //if (!this.OverEnemy) {
        //    this.enemy = null;
        //}
    }
    public void UnSelectUnit() {
        if(this.OnMove && this.Selected) {
            this.OnMove = false;
            this.Selected = false;
            this._unitOrder[0].OnMove(false);
        }
    }
    public void UnitSelect(Unit selectedUnit) {
        if (this._unitOrder[0] == selectedUnit
            && !this.hadMoved
            && !this.OnAttack
            && !this.OnHeal
            && !this.OnDefend) {

            this.OnMove = true;
            this.Selected = true;
            this.OnAttack = false;
            this.OnHeal = false;
            this.OnDefend = false;

            this._unitOrder[0].OnMove(true);
            return;
        }

        if (this.IsUnitAttackable(selectedUnit) && this.OnAttack) {
            this.ConfirmAttack(selectedUnit, this.numAttack);
        }
        if (this.IsUnitEatable(selectedUnit) && this.OnHeal) {
            this.ConfirmEat(selectedUnit);
        }
    }

    private bool IsUnitEatable(Unit selectedUnit) {
        if (selectedUnit.Eatable) {
            foreach (Tile tile in this._inRangeTiles) {
                if (selectedUnit.Tile == tile) {
                    return true;
                }
            }
        }

        return false;
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
        //this.OnAttack = true;

        //this._inRangeTiles = this._showRange.GetTilesInAttackMelee(this._unitOrder[0].Tile, this._unitOrder[0].BasicRange);
        //this.numAttack = 0;

        //foreach (Unit unit in this._Units)
        //{
        //    if (unit.Type == EUnitType.Ally)
        //    {
        //        this.UnitSelect(unit);
        //    }
        //}

        this._path = this._enemyAI.TakeTurn(this._unitOrder[0]);

        this.StartCoroutine(this.EnemyWait(1.0f));
    }    
    private IEnumerator EnemyWait(float seconds) {
        this.OnAttack = false;
        this.OnMove = false;
        yield return new WaitForSeconds(seconds);
        this._path.Clear();
        EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.ENABLE_CLICKS);
        this.NextUnitTurn();
    }
    private void OnAttackSelection() { // OVER HERE IS WHERE YOU'LL DO HIGHLIGHT?
        if(this.numAttack == 0) {
            this.GetMeleeAttackTiles();
        }
        if (this.numAttack == 1) {
            this.GetMeleeAttackTiles();
        }
        if (this.numAttack == 2) {
            this.GetMeleeAttackTiles();
        }
        if (this.numAttack == 3) {
            this.GetMeleeAttackTiles();
        }
        if (this.numAttack == 4) {
            this.GetMeleeAttackTiles();
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
    private void UpdateTile() {
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
        this._battleUI.NextCharacterAvatar(this._unitOrder[0]);
        this._unitOrder[0].OnTurn(!this._unitOrder[0].Turn);

        this.hadMoved = false;
        this.hadAttacked = false;
        this.hadHealed = false;
        this.hadDefend = false;
        this._unitOrder[0].Tile.isWalkable = true;
        this.Selected = false;
        this.numAttack = -1;

        // remove defend buff on their turn
        this._unitOrder[0].Defend = false;

        this.UnHighlightTiles();

        if (this._unitOrder[0].Type != EUnitType.Ally) {

            EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);
            this.EnemyUnitAction();
        }
        else {
            this._battleUI.ToggleActionBox();
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

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void GetEnemyMoveRange(Unit enemy) {
        this.UnHighlightTiles();

        this._inRangeTiles = this._showRange.GetTilesInMovement(enemy.Tile, enemy.Speed);

        foreach (Tile tile in this._inRangeTiles) {
            tile.HighlightWalkableTile();
        }
    }
    private void GetRangeTiles() {
        this.UnHighlightTiles();

        Unit currentUnit = this._unitOrder[0];

        this._inRangeTiles = this._showRange.GetTilesInMovement(currentUnit.Tile, currentUnit.Speed);

        foreach (Tile tile in this._inRangeTiles) {
            tile.HighlightWalkableTile();
        }
    }
    private void GetMeleeAttackTiles() {
        this.UnHighlightTiles();

        Unit currentUnit = this._unitOrder[0];

        this._inRangeTiles = this._showRange.GetTilesInAttackMelee(currentUnit.Tile, currentUnit.BasicRange);

        foreach (Tile tile in this._inRangeTiles) {
            tile.HighlightAttackableTile();
        }
    }
    private void GetRangeAttackTiles() {
        //this.UnHighlightTiles();

        //Unit currentUnit = this._unitOrder[0];

        //this._inRangeTiles = this._showRange.GetTilesInAttackRange(currentUnit.Tile, currentUnit.Range);

        //foreach (Tile tile in this._inRangeTiles)
        //{
        //    tile.HighlightAttackableTile();
        //}
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
            this._battleUI.NextCharacterAvatar(this._unitOrder[0]);

            this._enemyAI = new EnemyMainAI(this._Units);

            this._unitOrder[0].OnTurn(!this._unitOrder[0].Turn);

            if (this._unitOrder[0].Type != EUnitType.Ally) {
                EventBroadcaster.Instance.PostEvent(EventNames.UIEvents.DISABLE_CLICKS);
            }
            else {
                this._battleUI.ToggleActionBox();
            }

            OnStart = false;
        }

        if (Input.GetMouseButtonUp(1))
        { // right button
            this.UnSelectUnit();
        }

        if (this._path == null) return;

        if (this._path.Count > 0) {
            this.MoveCurrentUnit();
        }
        else {
            if (this.OnMove && !this.hadMoved) {
                this.GetRangeTiles();
            }
            else if (this.OnHeal && !this.hadHealed) {
                this.UnitHeal();
            }
            else if (this.OnAttack && !this.hadAttacked) {
                this.OnAttackSelection();
            }
            else if (this.OverEnemy && this.enemy != null) {
                this.GetEnemyMoveRange(this.enemy);
            }
            else {
                this.UnHighlightTiles();
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

