using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindKeyObjective : Objective
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.KEY_FOUND, this.FoundKey);


    }


    public void FoundKey(Parameters param)
    {
        this.clearCondition();
    }

    protected override void clearCondition()
    {
        //Debug.Log("Found key!!!");
        toggle.isOn = true;

        cleared = true;
    }
}
