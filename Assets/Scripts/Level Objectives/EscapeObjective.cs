using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeObjective : Objective
{
    public int AllyOnBoard = 0;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //for level 3
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.ESCAPED, this.Escaped);

        //for hostage rescue
        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.ON_BOARD, this.IncrementOnBoard);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CHECK_END_CONDITION, this.CheckAllOnBoard);
    }

    //for level 3
    public void Escaped()
    {
        Debug.Log("Escaping");
        this.clearCondition();
    }

    protected override void clearCondition()
    {
        toggle.isOn = true;

        cleared = true;
    }
    protected override void onConditionClear()
    {

    }


    public override bool getIfCleared()
    {
        return cleared;
    }

    //FOR HOSTAGE RESCUE
    public void IncrementOnBoard()
    {
        this.AllyOnBoard++;
    }

    public void CheckAllOnBoard(Parameters param)
    {
        if (UnitActionManager.Instance.UnitList.FindAll(u => u.Type == EUnitType.Ally).Count <= 0)
        {
            this.clearCondition() ;
        }
    }
}
