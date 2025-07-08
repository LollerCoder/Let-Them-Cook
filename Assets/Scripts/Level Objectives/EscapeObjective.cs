using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeObjective : Objective
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.ESCAPED, this.Escaped);
    }

    public void Escaped(Parameters param)
    {
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
}
