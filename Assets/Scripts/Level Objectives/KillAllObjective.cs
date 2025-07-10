using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillAllObjective : Objective
{
 
    private int _EnemyNums;

  

   

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, this.SetEnemyCount);
        EventBroadcaster.Instance.AddObserver(EventNames.Enemy_Events.ON_ENEMY_DEFEATED, this.DecrementEnemyCount);

        EventBroadcaster.Instance.AddObserver(EventNames.EnemySpawn_Events.SPAWN_ENEMY, this.SetEnemyCount);

      
    }

    public void SetEnemyCount()
    {
        this._EnemyNums = UnitActionManager.Instance.UnitList.FindAll(u => u.Type == EUnitType.Enemy).Count;
    }

    public void DecrementEnemyCount()
    {
        this._EnemyNums--;
        this.clearCondition();
        
    }
    protected override void  clearCondition()
    {
       

        if (this._EnemyNums <= 0)
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
