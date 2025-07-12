using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjective : Objective
{
    private new void Start()
    {
        base.Start();
        //Debug.Log("=============TUTORIAL STARTED!");
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, this.LateStart);
        EventBroadcaster.Instance.AddObserver(EventNames.Enemy_Events.ON_ENEMY_DEFEATED, this.clearCondition);
    }

    public void LateStart()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.ADDED_UNITS_SELECTED);
    }

    protected override void clearCondition()
    {
        toggle.isOn = true;

        cleared = true;
    }
}
