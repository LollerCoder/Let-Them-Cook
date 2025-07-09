using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tile where hostage needs to go to to win/get free
public class HostageGoalTile : Tile
{
    [SerializeField]
    private GameObject arrow_template;
    private GameObject arrow;

    private Unit _UnitOnBoard = null;

    private new void Start()
    {
        //Debug.Log("Hi i am here");
        EventBroadcaster.Instance.AddObserver(EventNames.Tile_Events.GOAL_ARROW_HIDE, this.HideArrow);
        EventBroadcaster.Instance.AddObserver(EventNames.Tile_Events.GOAL_ARROW_UNHIDE, this.ShowArrow);

        this.arrow = Instantiate(arrow_template, transform.position + Vector3.up * 0.25f, new Quaternion(0,0,0,0));
        this.arrow.SetActive(false);

        base.Start();
    }

    public void ShowArrow()
    {
        this.arrow.SetActive(true);
        EventBroadcaster.Instance.PostEvent(EventNames.HostageRescue_Events.ARROW_SHOWED);
    }

    public void HideArrow()
    {
        this.arrow.SetActive(false);
    }

    public override void ApplyEffect(Unit unit)
    {
        //if (unit.GetEffect("Captured_Hostage") == null) return;

        //Parameters param = new Parameters();

        //param.PutExtra("End_Text", "Hostage Freed");
        //param.PutExtra("Ally_Win", false);

        //param.PutExtra("Level_Complete", true);

        //EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);

        //NEW
        //increment on board if ally
        if (unit.Type == EUnitType.Ally)
        {
            BattleUI.Instance.ToggleActionBox();
            UnitActionManager.Instance.RemoveUnitFromOrder(unit);
            unit.gameObject.SetActive(false);
            EventBroadcaster.Instance.PostEvent(EventNames.HostageRescue_Events.ON_BOARD);
        
        }
        
        //get the number of allies on board

    }
}
