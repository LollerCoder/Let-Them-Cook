using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeHostageObjective : Objective
{

    new void Start()
    {
        base.Start();
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.KEY_FOUND, this.FreeHostage);


    }

    public void FreeHostage(Parameters param)
    {
        this.clearCondition();
    }

    protected override void clearCondition()
    {
        toggle.isOn = true;

        cleared = true;
    }
}
