using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeHostageObjective : Objective
{

    new void Start()
    {
        base.Start();
        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.HOSTAGE_FREE, this.FreeHostage);


    }

    public void FreeHostage()
    {
        this.clearCondition();
    }

    protected override void clearCondition()
    {
        toggle.isOn = true;

        cleared = true;
    }
}
