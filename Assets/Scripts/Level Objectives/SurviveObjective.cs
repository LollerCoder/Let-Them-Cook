using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// SurviveObjective
public class SurviveObjective : Objective
{

    [SerializeField]private int _allyNums;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //this.instaFail = true;
        toggle.isOn = true;
        cleared = true;
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.PLAYERDEATH, this.updateAlliesCount);
        this._allyNums = UnitActionManager.Instance.UnitList.FindAll(u => u.Type == EUnitType.Ally).Count;
    }

    public void updateAlliesCount()
    {
      
        this._allyNums = UnitActionManager.Instance.UnitList.FindAll(u => u.Type == EUnitType.Ally).Count;
        //this._allyNums--;
        this.clearCondition();

    }



    protected override void clearCondition()
    {


        if (this._allyNums <= 0)
        {

            toggle.isOn = false;
            cleared = false;
            this.unclearable = true;
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
