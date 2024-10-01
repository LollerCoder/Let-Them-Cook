using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class UnitActions {
    private static bool mouseOnUnit = false;
    private static Tile currentTile;
    private static Vector3 currentTilePos;
    private static Tile goalTile;
    public static bool selectFlag = false;

    ///////////////////////////////////////////////////////
    public static void SetCurrentTile(Tile Tile, float y) {
        currentTile = Tile;
        currentTilePos = new Vector3(currentTile.transform.position.x, y, currentTile.transform.position.z);
    }
    public static void UnitHover(Unit selectedUnit) {
        Unit currentUnit = UnitActionManager.Instance.GetUnit();
        if (currentUnit == selectedUnit
            && currentUnit.Type == EUnitType.Ally
            && !UnitActionManager.Instance.Selected
            && !UnitActionManager.Instance.OnAttack
            && !UnitActionManager.Instance.OnHeal
            ) {

            UnitActionManager.Instance.OnMove = !UnitActionManager.Instance.OnMove;
            //mouseOnUnit = !mouseOnUnit;
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
    public static void UnSelectUnit() {
        if (UnitActionManager.Instance.Selected && !UnitActionManager.Instance.Moving && !UnitActionManager.Instance.OnAttack) {
            UnitActionManager.Instance.OnMove = false;

            if (UnitActionManager.Instance.hadMoved) {
                EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
            }

            UnitActionManager.Instance.Selected = false;
            UnitActionManager.Instance.hadMoved = false;
            UnitActionManager.Instance.GetUnit().OnMove(false);
            UnitActionManager.Instance.GetUnit().transform.position = currentTilePos;
            UnitActionManager.Instance.GetUnit().Tile = currentTile;

            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.ON_AVATAR_CLICK);

            selectFlag = false;
        }
    }
    public static void UnitSelect(Unit selectedUnit) {
        if (UnitActionManager.Instance.GetUnit() == selectedUnit
            && !UnitActionManager.Instance.hadMoved
            && !UnitActionManager.Instance.OnAttack
            && !UnitActionManager.Instance.OnHeal
            && !selectFlag) {

            
            UnitActionManager.Instance.Selected = true;

            UnitActionManager.Instance.OnMove = true;
            UnitActionManager.Instance.OnAttack = false;
            UnitActionManager.Instance.OnHeal = false;

            UnitActionManager.Instance.GetUnit().OnMove(true);

            return;
        }

        if (IsUnitAttackable(selectedUnit) && UnitActionManager.Instance.OnAttack) {
            ConfirmAttack(selectedUnit, UnitActionManager.Instance.numAttack);
            //this.ConfirmAttack(selectedUnit, this.numAttack);
        }
        if (IsUnitEatable(selectedUnit) && UnitActionManager.Instance.OnHeal) {
            //this.ConfirmEat(selectedUnit);
            ConfirmEat(selectedUnit);
        }
    }

    public static void ConfirmMove() {
        if (UnitActionManager.Instance.Selected && !UnitActionManager.Instance.Moving && !UnitActionManager.Instance.OnAttack) {
            UnitActionManager.Instance.hadMoved = true;
            UnitActionManager.Instance.OnMove = false;
            UnitActionManager.Instance.OnAttack = true;
            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
            UnitActionManager.Instance.GetUnit().OnMove(false);
            Range.UnHighlightTiles();
            selectFlag = false;
            UnitActionManager.Instance.GetUnit().Tile = goalTile;
        }
    }

    ///////////////////////////////////////////////////////
    public static void ConfirmEat(Unit target) { // move to unit actions
        Unit currentUnit = UnitActionManager.Instance.GetUnit();

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
        Unit currentUnit = UnitActionManager.Instance.GetUnit();

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
        foreach (Tile tile in Range.InRangeTiles) {
            if (selectedUnit.Tile == tile && selectedUnit != UnitActionManager.Instance.GetUnit()) {
                return true;
            }
        }
        return false;
    }

    ///////////////////////////////////////////////////////
    
    public static void MoveCurrentUnit() {
        Unit currentUnit = UnitActionManager.Instance.GetUnit();

        float step = UnitActionManager.Instance.Speed * Time.deltaTime;

        float previousY = currentUnit.transform.position.y;
        Vector3 currentPos = currentUnit.transform.position;

        currentUnit.transform.position = Vector3.MoveTowards(currentPos, PathFinding.Path[0].transform.position, step);
        currentUnit.transform.position = new Vector3(currentUnit.transform.position.x,
                                                            previousY,
                                                            currentUnit.transform.position.z);

        Vector2 unitPos = new Vector2(currentUnit.transform.position.x, currentUnit.transform.position.z);
        Vector2 tilePos = new Vector2(PathFinding.Path[0].transform.position.x, PathFinding.Path[0].transform.position.z);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.ON_AVATAR_CLICK);

        if (Vector2.Distance(unitPos, tilePos) < 0.1f) {
            currentUnit.transform.position = new Vector3(PathFinding.Path[0].transform.position.x,
                                                            previousY,
                                                            PathFinding.Path[0].transform.position.z);
            currentUnit.Tile = PathFinding.Path[0];
            PathFinding.Path.RemoveAt(0);
        }

        if (PathFinding.Path.Count < 1) {
            goalTile = currentUnit.Tile;
            currentUnit.Tile = currentTile;
            UnitActionManager.Instance.Moving = false;
            //UnitActionManager.Instance.OnMove = false;
            currentUnit.OnMove(false);
            
        }
    }
}
