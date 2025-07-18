using UnityEngine;
using System.Collections;

/*
 * Holder for event names
 * Created By: NeilDG
 */ 
public class EventNames {
	public const string ON_UPDATE_SCORE = "ON_UPDATE_SCORE";
	public const string ON_CORRECT_MATCH = "ON_CORRECT_MATCH";
	public const string ON_WRONG_MATCH = "ON_WRONG_MATCH";
	public const string ON_INCREASE_LEVEL = "ON_INCREASE_LEVEL";

	public const string ON_PICTURE_CLICKED = "ON_PICTURE_CLICKED";


    public class UnitActionEvents {
        public const string ON_UNIT_TURN_START = "ON_UNIT_TURN_START";
        public const string ON_UNIT_TURN_END = "ON_UNIT_TURN_END";
        public const string ON_ATTACK_START = "ON_ATTACK_START";
        public const string ON_ATTACK_END = "ON_ATTACK_END";
        public const string ON_DESTINATION_REACHED = "ON_DESTINATION_REACHED";
        public const string ON_ENEMY_END_TURN = "ON_ENEMY_END_TURN";
    }
  
    public class UIEvents {
        public const string ENABLE_CLICKS = "ENABLE_CLICKS";
        public const string DISABLE_CLICKS = "DISABLE_CLICKS";
    }
    public class Dialogue_Events
    {
        public const string ON_DIALOGUE_START = "ON_DIALOGUE_START";
        public const string ON_DIALOGUE_FINISHED = "ON_DIALOGUE_FINISHED";
    }

	public class Enemy_Events
	{
		public const string ON_ENEMY_DEFEATED = "ON_ENEMY_DEFEATED";

	}

	public class BattleManager_Events {
        public const string HANDLE_GAIN_REWARDS = "HANDLE_GAIN_REWARDS";
        public const string CHECK_END_CONDITION = "CHECK_END_CONDITION";
        public const string ON_START = "ON_START";
		public const string NEXT_TURN = "NEXT_TURN";
		public const string UPDATE_INVENTORY = "UPDATE_INVENTORY";

		//CUTSCENE
		public const string CUTSCENE_PLAY = "CUTSCENE_PLAY";
        public const string CUTSCENE_END = "CUTSCENE_END";
		//AOE TOGGLE
		public const string CUTSCENE_AOE = "CUTSCENE_AOE";

		//UNIT SELECT
		public const string ADDED_UNITS_SELECTED = "ADDED_UNITS_SELECTED";
    }


	public class BattleUI_Events {
		public const string ON_AVATAR_CLICK = "ON_AVATAR_CLICK";
		public const string TOGGLE_ACTION_BOX = "TOGGLE_ACTION_BOX";
		public const string SHOW_HP = "SHOW_HP";
		public const string HIDE_HP = "HIDE_HP";
		public const string SHOW_DEFENSE = "SHOW_DEFENSE";
		public const string HIDE_DEFENSE = "HIDE_DEFENSE";
		public const string DEBUFF_SHOW = "DEBUFF_SHOW";
        public const string BUFF_SHOW = "BUFF_SHOW";
        public const string DEBUFF_HIDE = "DEBUFF_HIDE";
		public const string BUFF_HIDE = "BUFF_HIDE";
        public const string WAIT_BUTTON_SHOW = "WAIT_BUTTON_SHOW";

    }

	public class BattleCamera_Events {
		public const string ENEMY_FOCUS = "ENEMY_FOCUS";
		public const string CURRENT_FOCUS = "CURRENT_FOCUS";
	}

	public class HostageRescue_Events
	{
		public const string HOSTAGE_FREE = "HOSTAGE_FREE";
		public const string GOAL_ARROW_HIDE = "GOAL_ARROW_HIDE";
		public const string GOAL_ARROW_UNHIDE = "GOAL_ARROW_UNHIDE";
		public const string ARROW_SHOWED = "ARROW_SHOWED";
		public const string BOAT_ARRIVED = "BOAT_ARRIVED";
		public const string ON_BOARD = "ON_BOARD";
    }

	public class EnemySpawn_Events
	{
		public const string SPAWN_ENEMY = "SPAWN_ENEMY";

    }

	public class Level3_Objectives
	{
		public const string WOKE_UP = "WOKE_UP";
		public const string KEY_FOUND = "KEY_FOUND";
		public const string ESCAPED = "ESCAPED";
	}

	public class Tile_Events
    {
        public const string GOAL_ARROW_HIDE = "GOAL_ARROW_HIDE";
        public const string GOAL_ARROW_UNHIDE = "GOAL_ARROW_UNHIDE";
    }

	public class Level5_Events
    {
		public const string BREACHED = "BREACHED";
    }

}







