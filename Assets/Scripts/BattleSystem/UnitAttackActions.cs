using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAttackActions : MonoBehaviour {

    public static List<List<Unit>> Attackables = new List<List<Unit>>();
    public static bool EnemyListed = false;
    private static int attackablesIndex = 0;

    public static void ShowUnitsInSkillRange(int i, Unit unit) {
        Skill skill = SkillDatabase.Instance.findSkill(unit.SKILLLIST[i]);

        if (Attackables[i].Count > 0) {
            UnitActions.ShowInRangeHPBar(i);
        }

        foreach (Unit _unit in Attackables[i]) { // highlights the tiles of the units 
            skill.HighlightTile(_unit);
        }


    }

    public static void CheckSkillRange(Unit unit) { // OVER HERE IS WHERE YOU'LL DO HIGHLIGHT?
        Skill skill;
        int i = 0;
        foreach (string uSkill in unit.SKILLLIST) {
            skill = SkillDatabase.Instance.findSkill(uSkill);
           
            if (skill != null){
                GetUnitsInRange(skill, unit, i);
            }
            i++;
        }
        for (int j = 0; j < Attackables.Count; j++) { // will change depending to the range of the unit/skill
            if (Attackables[j].Count == 0){
                
                BattleUI.Instance.UpdateButtonState(j, false, unit);
            }
            else {
                BattleUI.Instance.UpdateButtonState(j, true, unit);
                //Debug.Log(j);
            }
        }
    }
    public static void UpdateAttackableUnits(int i) {
        foreach (ITurnTaker temp in UnitActionManager.Instance.TurnOrder) {
            foreach (Tile tile in Range.InRangeTiles) {
                if (temp is Unit unit) {
                    if (unit.Tile == tile && unit.Type != EUnitType.Ally) {
                        Attackables[i].Add(unit);
                        //unit.InRange = true;
                    }
                }
            }
        }
    }

    public static void UpdateSelectableAllies(int i) {
        foreach (ITurnTaker temp in UnitActionManager.Instance.TurnOrder) {
            foreach (Tile tile in Range.InRangeTiles) {
                if (temp is Unit unit) {
                    if (unit.Tile == tile && unit.Type == EUnitType.Ally) {
                        Attackables[i].Add(unit);
                    }
                }
            }
        }
    }
    public static bool IsUnitSelectable(Unit selectedUnit, int skillNum) {
        if (UnitActionManager.Instance.Moving) {
            return false;
        }

        if (skillNum == -1) {
            return false;
        }

        if (Attackables[skillNum].Find(u => u == selectedUnit)) {
            return true;
        }
        return false;

        /*If within range and if the type is Enemy*/
        //if (selectedUnit.InRange && (selectedUnit.Type == EUnitType.Enemy|| selectedUnit.Type == EUnitType.Boss))
        //{
        //    return true;
        //}
        //return false;
    }

    public static void SetAttackableList() {
        ResetAttackables();
        Attackables.Clear(); // clear the entire list first, match the number of unit skills
        for (int i = 0; i < ((Unit)UnitActionManager.Instance.GetFirstUnit()).SKILLLIST.Count; i++) {
            Attackables.Add(new List<Unit>());
        }
        CheckSkillRange((Unit)UnitActionManager.Instance.GetFirstUnit());
    }

    public static void ResetAttackables() {
        foreach (List<Unit> attackables in Attackables) {
            UnHighlightUnitTiles(attackables);
            attackables.Clear();
            attackablesIndex = 0;
        }
    }

    public static void UnHighlightUnitTiles(List<Unit> attackables) {
        foreach (Unit unit in attackables) {
            unit.InRange = false;
            unit.Tile.UnHighlightTargetTile();

        }
    }

    public static void CycleEnemy(int i, int j) {
        if (Attackables[i].Count == 0) {
            return;
        }

        switch (j) {
            case 0: attackablesIndex = (attackablesIndex - 1 + Attackables[i].Count) % Attackables[i].Count;
                break;
            case 1: attackablesIndex = (attackablesIndex + 1) % Attackables[i].Count;
                break;
            default: break;
        }

        Parameters param = new Parameters();
        param.PutExtra("UNIT", Attackables[i][attackablesIndex]);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.ENEMY_FOCUS, param);
    }
    private static void GetUnitsInRange(Skill skill, Unit unit, int index) { // Function that determines the skill type to pick the appropriate range and target selection
               
        switch (skill.SKILLTYPE) { 
            case ESkillType.BASIC:
            case ESkillType.RANGE:
                Range.GetRange(unit, skill.SkillRange, RangeType.ATTACK);
                UpdateAttackableUnits(index);
                break;
            case ESkillType.AOE:
                Range.GetRange(unit, skill.SkillRange, RangeType.ATTACK);
                UpdateAttackableUnits(index);
                break;
            case ESkillType.HEAL:
                Range.GetRange(unit, skill.SkillRange, RangeType.HEAL);
                UpdateSelectableAllies(index);
                break;
            default: break;
        }
    }
}


