using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosObjective : Objective
{
    
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        this.clearCondition();
    }



    protected override void clearCondition()
    {
        Unit someBS = UnitActionManager.Instance.GetFirstUnit() as Unit;
        GameObject toCheck = someBS.gameObject;
        if (toCheck.transform.position.x == 3 && toCheck.transform.position.z == 4)
        {
            toggle.isOn = true;
            cleared = true;
        }

    }
    protected override void onConditionClear()
    {

    }

    public override bool getIfCleared()
    {
        return cleared;
    }
}


