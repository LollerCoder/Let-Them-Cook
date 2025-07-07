using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindKeyObjective : Objective
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, this.LateStart);


    }

    public void LateStart()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.KEY_FOUND, this.FoundKey);
    }

    public void FoundKey()
    {
        this.clearCondition();
    }

    protected override void clearCondition()
    {
        Debug.Log("Found key!!!");
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
}
