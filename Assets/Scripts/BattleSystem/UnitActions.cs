using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class UnitActions {
    private static bool mouseOnUnit = false;
    private static Tile currentTile;
    private static Vector3 currentTilePos;
    private static Tile goalTile = new Tile();
    private static List<Unit> Attackables = new List<Unit>();
    
    public static bool stepFlag = false;
    
    private static int _affectedStatValue = 0;

    public static bool EnemyListed = false;

    public const string UNIT = "UNIT";

    ///////////////////////////////////////////////////////
    public static void SetCurrentTile(Tile Tile, float y) {
        currentTile = Tile;
        currentTilePos = new Vector3(currentTile.transform.position.x, y, currentTile.transform.position.z);
    }
    public static void ResetPosition() {
        if (!UnitActionManager.Instance.Moving) {
            UnitActionManager.Instance.OnMove = false;

            if (UnitActionManager.Instance.hadMoved) {
                EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
            }
            UnitActionManager.Instance.OnAttack = false;
            UnitActionManager.Instance.Stayed = false;
            UnitActionManager.Instance.OnMove = true;
            UnitActionManager.Instance.hadMoved = false;
            UnitActionManager.Instance.GetFirstUnit().OnMovement(true);
            UnitActionManager.Instance.GetFirstUnit().transform.position = currentTilePos;
            UnitActionManager.Instance.GetFirstUnit().Tile = currentTile;

            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.CAMERA_FOLLOW);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
            stepFlag = false;
        }
    }
    public static void UnitHover(Unit unit) {
        Parameters param = new Parameters();

        param.PutExtra("UNIT", unit);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);
    }
    public static void UnitSelect(Unit selectedUnit) {
        if (IsUnitAttackable(selectedUnit) && UnitActionManager.Instance.OnAttack) {
            ConfirmAttack(selectedUnit, UnitActionManager.Instance.numAttack);
            //this.ConfirmAttack(selectedUnit, this.numAttack);
        }
        if (IsUnitEatable(selectedUnit) && UnitActionManager.Instance.OnHeal) {
            //this.ConfirmEat(selectedUnit);
            ConfirmEat(selectedUnit);
        }
    }

    //public static void ConfirmMove() {
    //    if (!UnitActionManager.Instance.Moving && !UnitActionManager.Instance.OnAttack) {
    //        UnitActionManager.Instance.hadMoved = true;
    //        UnitActionManager.Instance.OnMove = false;
    //        UnitActionManager.Instance.OnAttack = true;
    //        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
    //        UnitActionManager.Instance.GetUnit().OnMovement(false);
    //        Range.UnHighlightTiles();
    //        stepFlag = false;

    //        if(!UnitActionManager.Instance.Stayed) {
    //            UnitActionManager.Instance.GetUnit().Tile = goalTile;
    //        }
    //    }
    //}

    ///////////////////////////////////////////////////////
    public static void ConfirmEat(Unit target) { // move to unit actions
        Unit currentUnit = UnitActionManager.Instance.GetFirstUnit();

        if (!UnitActionManager.Instance.hadHealed) {
            if (currentUnit.HP == currentUnit.MAXHP) {
                UnitActionManager.Instance.OnHeal = false;
                Debug.Log("no heal");
                return;
            }

            currentUnit.Heal(target);
            UnitActionManager.Instance.hadHealed = true;
            UnitActionManager.Instance.OnHeal = false;
            Debug.Log("UnitHeal " + currentUnit.Name + " Healed");
        }
    }
    public static void ConfirmAttack(Unit target, int Skill) {   // move to unit actions
        Unit currentUnit = UnitActionManager.Instance.GetFirstUnit();

        switch (Skill) {
            case 0:
                if (currentUnit.SKILLLIST[Skill] != null) {
                    Debug.Log(currentUnit.SKILLLIST[Skill]);
                    Debug.Log(target.Name);
                    SkillDatabase.Instance.applySkill(currentUnit.SKILLLIST[Skill], target, currentUnit);

                }
                break;
            case 1:
                if (currentUnit.SKILLLIST[Skill] != null) {
                    SkillDatabase.Instance.applySkill(currentUnit.SKILLLIST[Skill], target, currentUnit);

                }
                break;
            case 2:
                if (currentUnit.SKILLLIST[Skill] != null) {
                    SkillDatabase.Instance.applySkill(currentUnit.SKILLLIST[Skill], target, currentUnit);

                }
                break;
            case 3:
                if (currentUnit.SKILLLIST[Skill] != null) {
                    SkillDatabase.Instance.applySkill(currentUnit.SKILLLIST[Skill], target, currentUnit);

                }
                break;
            case 4:
                if (currentUnit.SKILLLIST[Skill] != null) {
                    SkillDatabase.Instance.applySkill(currentUnit.SKILLLIST[Skill], target, currentUnit);

                }
                break;

        }


        Debug.Log("Attacked Target " + target.name);
        if (Skill == 1) {
            Debug.Log("10 dmg applied");
        }
        UnitActionManager.Instance.OnAttack = false;
        UnitActionManager.Instance.hadAttacked = true;
        UnitActionManager.Instance.NextUnitTurn();
        if (UnitActionManager.Instance.GetFirstUnit().Type == EUnitType.Ally) {
            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
        }
    }
    public static bool IsUnitEatable(Unit selectedUnit) { ///// move to unit actions
        if (selectedUnit.Eatable) {
            foreach (Tile tile in Range.InRangeTiles) {
                if (selectedUnit.Tile == tile) {
                    return true;
                }
            }
        }

        return false;
    }
    public static bool IsUnitAttackable(Unit selectedUnit) { //// move to unit actions

        if (Attackables.Find(u => u == selectedUnit)) {
            return true;
        }

        return false;
    }
    public static void GetAttackableUnits() {
        foreach(Unit unit in UnitActionManager.Instance.UnitOrder) {
            foreach(Tile tile in Range.InRangeTiles) {
                if(unit.Tile == tile && unit.Type != EUnitType.Ally) {
                    Debug.Log(unit.Name);
                    Attackables.Add(unit);
                }
            }
        }
    }
    private static void InRangeHPBar() {
        //Parameters param = new Parameters();
        //foreach (Unit unit in Attackables) {
        //    param.PutExtra("UNIT", unit);
        //    EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.UNIT_ATTACK, param);
        //}

    }

    ///////////////////////////////////////////////////////
    public static void OnAttackSelection() { // OVER HERE IS WHERE YOU'LL DO HIGHLIGHT?
        int Attack = UnitActionManager.Instance.numAttack;
        Unit unit = UnitActionManager.Instance.GetFirstUnit();

        if (Attack == 0) {
            Range.GetRange(unit, unit.BasicRange, "Attack"); // temporary for all
            //this.GetMeleeAttackTiles();
        }
        if (Attack == 1) {
            Range.GetRange(unit, unit.BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
        if (Attack == 2) {
            Range.GetRange(unit, unit.BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
        if (Attack == 3) {
            Range.GetRange(unit, unit.BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
        if (Attack == 4) {
            Range.GetRange(unit, unit.BasicRange, "Attack");
            //this.GetMeleeAttackTiles();
        }
        if (!EnemyListed) {
            GetAttackableUnits();
            EnemyListed = true;
        }

        InRangeHPBar();
    }   
    public static bool AllyOnTileGoal(Tile endTile) {
        if (endTile.TilePos == UnitActionManager.Instance.GetFirstUnit().Tile.TilePos) {
            return false;
        }

        foreach (Unit unit in UnitActionManager.Instance.UnitList) {
            if (unit.Type == EUnitType.Ally) {
                if (unit.Tile.TilePos == endTile.TilePos) {
                    return true;
                }
            }
        }

        return false;
    }
    public static void MoveCurrentUnit() {
        Unit currentUnit = UnitActionManager.Instance.GetFirstUnit();

        float step = UnitActionManager.Instance.Speed * Time.deltaTime;

        float previousY = currentUnit.transform.position.y;
        Vector3 currentPos = currentUnit.transform.position;

        currentUnit.transform.position = Vector3.MoveTowards(currentPos, PathFinding.Path[0].transform.position, step);
        currentUnit.transform.position = new Vector3(currentUnit.transform.position.x,
                                                            previousY,
                                                            currentUnit.transform.position.z);

        Vector2 unitPos = new Vector2(currentUnit.transform.position.x, currentUnit.transform.position.z);
        Vector2 tilePos = new Vector2(PathFinding.Path[0].transform.position.x, PathFinding.Path[0].transform.position.z);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.CAMERA_FOLLOW);

        if (Vector2.Distance(unitPos, tilePos) < 0.1f) {
            currentUnit.transform.position = new Vector3(PathFinding.Path[0].transform.position.x,
                                                            previousY,
                                                            PathFinding.Path[0].transform.position.z);
            currentUnit.Tile = PathFinding.Path[0];
            goalTile = PathFinding.Path[0];
            PathFinding.Path.RemoveAt(0);
        }

        if (PathFinding.Path.Count < 1) {

            //currentUnit.Tile = currentTile;
            UnitActionManager.Instance.Moving = false;
            UnitActionManager.Instance.OnMove = false;
            currentUnit.OnMovement(false);
            if(UnitActionManager.Instance.GetFirstUnit().Type == EUnitType.Ally) {
                EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
            }           
        }
    }
    public static void TileTapped(Tile goalTile) {
        Unit unit = UnitActionManager.Instance.GetFirstUnit();
        string bufDebufname = ""; //name

        EffectInfo terst = new EffectInfo(0, 0, EStatToEffect.NOTSET); //effectInfo
        if (!UnitActionManager.Instance.hadMoved &&
            !AllyOnTileGoal(goalTile) &&
            UnitActionManager.Instance.OnMove) {

            PathFinding.Path = PathFinding.AStarPathFinding(unit.Tile,
                         goalTile,
                         Range.GetTilesInMovement(unit.Tile,
                                                         unit.Speed)
                         );

            if (unit.Tile.TilePos == goalTile.TilePos) {
                stepFlag = true;
                UnitActionManager.Instance.Stayed = true;
            }

            if (PathFinding.Path.Count > 0) {
                UnitActionManager.Instance.Stayed = false;
                stepFlag = true;
                unit.OnMovement(true);
                UnitActionManager.Instance.Moving = true;
                /*Special Tile Detection*/
                if (goalTile.gameObject.tag == "SpecialTile") {
                    Debug.Log("Special Tile detected!" + goalTile.gameObject.name);

                    switch (goalTile.gameObject.name) {
                        case "BuffTile(Clone)":
                            terst = new EffectInfo(3, 2, EStatToEffect.ACCURACY); //effectInfo
                            bufDebufname = "BuffTile";

                            break;

                        case "DebuffTile(Clone)":
                            terst = new EffectInfo(3, -2, EStatToEffect.SPEED);
                            // this._eagleEye.ApplyEffect(this._unitOrder[0],this._unitOrder[0],_skill.skillData); 
                            bufDebufname = "DebuffTile";
                            break;

                        case "RandomTile(Clone)":
                            _affectedStatValue = UnityEngine.Random.Range(-6, 6);
                            terst = new EffectInfo(3, _affectedStatValue, EStatToEffect.ATTACK);
                            bufDebufname = "RandomTile";
                            // this._eagleEye.ApplyEffect(this._unitOrder[0],this._unitOrder[0],_skill.skillData);
                            break;

                        case "HazardTile(Clone)":
                            unit.HP -= 1;
                            break;

                        default:
                            Debug.Log("B");
                            break;
                    }
                    // TODO put APPLYEFFECT into effectmanager, its reused in many codes throughtout...NUKE IEFFECTABLE
                    if (bufDebufname != "") {
                        SkillDatabase.Instance.addSkill(terst, bufDebufname);
                        unit.EffectManager.ApplyTileEffect(unit, bufDebufname, terst.DURATION);
                    }
                }
            }
        }
    }
    public static void AssignUnitTile() {

        var map = TileMapGenerator.Instance.TileMap;

        map = TileMapGenerator.Instance.TileMap;

        Vector2Int unitPos = new Vector2Int();
        foreach (Unit unit in UnitActionManager.Instance.UnitList) {
            unitPos = new Vector2Int(
                (int)unit.transform.position.x,
                (int)unit.transform.position.z
            );

            if (map.ContainsKey(unitPos)) {
                unit.Tile = map[unitPos];
            }

        }
    }
    public static  void UpdateTile() { /////move to tileactions
        TileMapGenerator.Instance.UpdateTile();

        foreach (Unit unit in UnitActionManager.Instance.UnitOrder) {
            if (unit.Type != EUnitType.Ally) {
                unit.Tile.isWalkable = false;
            }
            else {
                unit.Tile.isWalkable = true;
            }
        }
    }
}
