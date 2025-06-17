using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public static class UnitActions {
    //private static bool mouseOnUnit = false;
    private static Tile currentTile;
    private static Vector3 currentTilePos;
    private static Tile goalTile = new Tile();
    private static DroppedVegetable currVeg;
    
    public static bool stepFlag = false;

    //if a tile has been tapped
    public static bool bGoal = false;
    
   // private static int _affectedStatValue = 0;

    public const string UNIT = "UNIT";

    //cutscene
    public const string currUNIT = "CURRUNIT";
    public const string TARGET = "TARGET";
    public const string CAMERA = "CAMERA";
    //public const string SKILLANIM = "SKILLANIM";
    public const string SKILLNAME = "SKILLNAME";

    ///////////////////////////////////////////////////////
    public static void SetCurrentTile(Tile Tile, float y) {
        currentTile = Tile;
        currentTilePos = new Vector3(currentTile.transform.position.x, y, currentTile.transform.position.z);
    }
    public static void ResetPosition() {
        if (!UnitActionManager.Instance.Moving) {
            UnitActionManager.Instance.OnMove = false;
            Unit unit = (Unit)UnitActionManager.Instance.GetFirstUnit();
            if (UnitActionManager.Instance.hadMoved) {
                //EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
                BattleUI.Instance.ToggleActionBox();
            }
            UnitActionManager.Instance.OnAttack = false;
            UnitActionManager.Instance.OnMove = true;
            UnitActionManager.Instance.hadMoved = false;
            unit.OnMovement(true);
            unit.transform.position = currentTilePos;
            unit.Tile = currentTile;
            bGoal = false;
            EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.CURRENT_FOCUS);
            //EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);
            //if (BattleUI.Instance.ActionBoxState) {               // MIGHT CHANGE
            //    BattleUI.Instance.ToggleActionBox();
            //}

            if (BattleUI.Instance.EatPickUpButtonState){
                BattleUI.Instance.ToggleEatOrPickUpButtons();
            }
           
            stepFlag = false;

            HideInRangeHPBar(UnitActionManager.Instance.numAttack);

            // reset and updatec attackable list
            UnitAttackActions.ResetAttackables();
            UnitAttackActions.CheckSkillRange(unit);

            
        }
    }
    public static void UnitHover(Unit unit, bool toggle) { // if true, show hp bar ; if false, hide 
        Parameters param = new Parameters();
        param.PutExtra(UNIT, unit);

        if (toggle && !unit.InRange) {
            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);
        }
        else if (!toggle && !unit.InRange) {
            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.HIDE_HP, param);
        }
        
    }
    public static void UnitSelect(Unit selectedUnit) {
        // check if the unit selected is in the range of the skill
        if (UnitAttackActions.IsUnitSelectable(selectedUnit, UnitActionManager.Instance.numAttack) && 
            UnitActionManager.Instance.OnAttack &&
            !UnitActionManager.Instance.hadAttacked
            ) {
            ConfirmAttack(selectedUnit, UnitActionManager.Instance.numAttack);
        }
    }

    ///////////////////////////////////////////////////////
    public static void ConfirmAttack(Unit target, int Skill) {
        Unit currentUnit = (Unit)UnitActionManager.Instance.GetFirstUnit();
        //ESkillType skillType = ESkillType.NONE;
        string SkillName = "";

        if (currentUnit.SKILLLIST[Skill] != null)
        {
            SkillName = SkillDatabase.Instance.findSkill(currentUnit.SKILLLIST[Skill]).SkillName;
        }


        Debug.Log("Attacked Target " + target.name);
        if (Skill == 1) {
            Debug.Log("10 dmg applied");
        }

        UnitActionManager.Instance.OnAttack = false;
        UnitActionManager.Instance.hadAttacked = true;

        //JAIRO BATTLECUTSCENE

        if (GameSettingsManager.Instance.enableCutscene)
        {
            Parameters param = new Parameters();
            
            


            if (currentUnit.SKILLLIST[Skill] != null)
            {
                if(SkillDatabase.Instance.findSkill(currentUnit.SKILLLIST[Skill]).SKILLTYPE == ESkillType.AOE)
                {
                    param = SkillDatabase.Instance.findSkill(currentUnit.SKILLLIST[Skill]).GetNeighborList(currentUnit, target);
                    param.PutExtra("TARGETS", "MULTIPLE");
                    
                }
                else
                {
                    param.PutExtra("TARGETS", "SINGLE");
                    
                }
                
            }
            param.PutGameObjectExtra(CAMERA, Camera.main.gameObject);
            param.PutExtra(currUNIT, currentUnit);
            param.PutExtra(TARGET, target);
            param.PutExtra(SKILLNAME, SkillName);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CUTSCENE_PLAY, param); //cutscene call
            EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CUTSCENE_PLAY); // camera takes one with params
        }
        else
        {
            applySkill(target, Skill);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
        }





    }


    public static void applySkill(Unit target, int Skill)
    {
        Unit currentUnit = (Unit)UnitActionManager.Instance.GetFirstUnit();

        if (currentUnit.SKILLLIST[Skill] != null)
        {
            SkillDatabase.Instance.applySkill(currentUnit.SKILLLIST[Skill], target, currentUnit);


        }
    }
    public static void ShowInRangeHPBar(int i) {
        Parameters param;

        foreach (Unit unit in UnitAttackActions.Attackables[i]) {
            unit.InRange = true;
            param = new Parameters();
            param.PutExtra(UNIT, unit);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.SHOW_HP, param);
        }
    }
    public static void HideInRangeHPBar(int i) {
        Parameters param = new Parameters();

        if (i == -1) { // skip if there are no skill activated
            return;
        }

        foreach (Unit unit in UnitAttackActions.Attackables[i]) {
            param = new Parameters();
            param.PutExtra(UNIT, unit);
            unit.InRange = false;
            EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.HIDE_HP, param);
        }
    }

    ///////////////////////////////////////////////////////
    public static bool AllyOnTileGoal(Tile endTile) {
        if (endTile.TilePos == ((Unit)UnitActionManager.Instance.GetFirstUnit()).Tile.TilePos) {
            return false;
        }

        foreach (Unit unit in UnitActionManager.Instance.UnitList) {
            if (unit.Type == EUnitType.Ally) {
                if (unit.Tile.TilePos == endTile.TilePos) {
                    bGoal = false;
                    return true;
                }
            }
        }

        return false;
    }
    public static void MoveCurrentUnit() {
        Unit currentUnit = (Unit)UnitActionManager.Instance.GetFirstUnit();

        float step = UnitActionManager.Instance.Speed * Time.deltaTime;

        //float previousY = currentUnit.transform.position.y;
        Vector3 currentPos = currentUnit.transform.position;

        if (currentUnit.Name == "Sergeant Bob") {

        }

        currentUnit.transform.position = Vector3.MoveTowards(currentPos, PathFinding.Path[0].transform.position, step);
        currentUnit.transform.position = new Vector3(currentUnit.transform.position.x,
                                                            PathFinding.Path[0].transform.position.y + TileMapManager.Instance.unitLock,
                                                            currentUnit.transform.position.z);

        Vector2 unitPos = new Vector2(currentUnit.transform.position.x, currentUnit.transform.position.z);
        Vector2 tilePos = new Vector2(PathFinding.Path[0].transform.position.x, PathFinding.Path[0].transform.position.z);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.CURRENT_FOCUS);

        if (Vector2.Distance(unitPos, tilePos) < 0.1f) {
            currentUnit.transform.position = new Vector3(PathFinding.Path[0].transform.position.x,
                                                            PathFinding.Path[0].transform.position.y + TileMapManager.Instance.unitLock,
                                                            PathFinding.Path[0].transform.position.z);
            currentUnit.Tile = PathFinding.Path[0];
            goalTile = PathFinding.Path[0];

            PathFinding.Path.RemoveAt(0);



        }

        if (PathFinding.Path.Count < 1) {
            UnitActionManager.Instance.Moving = false;
            UnitActionManager.Instance.OnMove = false;
            currentUnit.OnMovement(false);
            BattleUI.Instance.ToggleWaitButton(true);
            if(currentUnit.Type == EUnitType.Ally && !CheckVegetableOnTile(currentUnit)) {
                //BattleUI.Instance.ToggleActionBox();
                //EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX);

                // reset and update attackable list
                UnitAttackActions.ResetAttackables();
                UnitAttackActions.CheckSkillRange((Unit)UnitActionManager.Instance.GetFirstUnit());
            }
            else if (CheckVegetableOnTile(currentUnit) && currentUnit.Type == EUnitType.Ally) {
                BattleUI.Instance.ToggleEatOrPickUpButtons();
            }

            //springs
            SpringTile st = currentUnit.Tile as SpringTile;
            if (st != null)
            {
                st.setUnitToLaunch(currentUnit);
                Debug.Log("UnitToLaunchSet");
            }
            //springs
        }

    }

    private static bool CheckVegetableOnTile(Unit unit) {
        foreach(DroppedVegetable veg in DroppedVegetableManager.Instance.VegInField) {
            if(veg.Tile == unit.Tile) {
                currVeg = veg;
                return true;
            }
        }

        return false;
    }

    public static void UpdateVegetable(int choice) { // 0 for eat, 1 for pickup
        if (choice == 0) {
            DroppedVegetableManager.Instance.EatVegetable(currVeg);
        }
        if(choice == 1) {
            DroppedVegetableManager.Instance.PickUpVegetable(currVeg);
        }
   
        currVeg = null;
        BattleUI.Instance.ToggleEatOrPickUpButtons();
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }
    public static void TileTapped(Tile goalTile)
    {
        Unit unit = (Unit)UnitActionManager.Instance.GetFirstUnit();
        //string bufDebufname = ""; //name
        EffectInfo terst = new EffectInfo(0, 0, EStatToEffect.NOTSET); //effectInfo

        if (!UnitActionManager.Instance.hadMoved && !AllyOnTileGoal(goalTile) && UnitActionManager.Instance.OnMove && bGoal)
        {

            PathFinding.Path = PathFinding.AStarPathFinding(unit.Tile, goalTile,
                                Range.GetTilesInMovement(unit.Tile, unit.Move));

            if (unit.Tile.TilePos == goalTile.TilePos)
            {
                //Debug.Log("helllloooo");
                stepFlag = true;
                //BattleUI.Instance.ToggleActionBox(); // MIGHT CHANGE
                ((Unit)UnitActionManager.Instance.GetFirstUnit()).OnMovement(false);
                UnitActionManager.Instance.OnMove = false;

            }

            if (PathFinding.Path.Count <= 0) {
                UnitActions.bGoal = false;
                return;
            }

            // if there is a path

            stepFlag = true;
            unit.OnMovement(true);
            UnitActionManager.Instance.Moving = true;
            BattleUI.Instance.ToggleWaitButton(false);



            SpriteRenderer cuSR = ((Unit)UnitActionManager.Instance.GetFirstUnit()).GetComponent<SpriteRenderer>();
            if (((Unit)UnitActionManager.Instance.GetFirstUnit()).transform.position.x > goalTile.transform.position.x) {
                //Debug.Log("looking left");
                cuSR.flipX = true;
            }
            else {
                //Debug.Log("looking right");
                cuSR.flipX = false;
            }
        }
        
     
    }
    public static void AssignUnitTile() {

        var map = TileMapManager.Instance.TileMap;
        //var map = TileMapGenerator.Instance.TileMap;

        Vector2Int unitPos = new Vector2Int();
        foreach (Unit unit in UnitActionManager.Instance.UnitList) {

            unitPos = new Vector2Int(
                Mathf.RoundToInt(unit.transform.position.x),
                Mathf.RoundToInt(unit.transform.position.z)
            );
            //Debug.Log(unit.Name + unitPos);

            if (map.ContainsKey(unitPos)) {
                unit.Tile = map[unitPos];
               //Debug.Log(unit.Name + unitPos + "assigned");

            }

        }
    }
    public static void UpdateTile() { // move to tileactions
        TileMapManager.Instance.UpdateTile();
        foreach (ITurnTaker temp in UnitActionManager.Instance.TurnOrder) {
            if (temp is Unit unit) {
                if (unit.Tile == null) {
                    Debug.Log(unit.Name);
                }

                if (unit.Type != EUnitType.Ally) {
                    unit.Tile.isWalkable = false;
                }
                else {
                    unit.Tile.isWalkable = true;
                }
            }
        }
    }
}
