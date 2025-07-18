using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillColonelKornObjective : Objective
{

   
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //this.instaFail = true;
        toggle.isOn = false;
        cleared = false;
        EventBroadcaster.Instance.AddObserver(EventNames.Enemy_Events.ON_ENEMY_DEFEATED,checkForCorn);

    }

    public void checkForCorn(Parameters param)
    {
        Unit enemy = param.GetUnitExtra("EnemyKilled");
       
        if(enemy.IngredientType == EIngredientType.CORN)
        {
            this.clearCondition();
        }
        

    }



    protected override void clearCondition()
    {



            toggle.isOn = true;
            cleared = true;
            //this.unclearable = true;
        

    }
    protected override void onConditionClear()
    {

    }


    public override bool getIfCleared()
    {
        return cleared;
    }
}

