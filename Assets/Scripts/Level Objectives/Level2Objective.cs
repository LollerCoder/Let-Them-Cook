using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Objective : MonoBehaviour
{
    //private int _EnemyNums;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, this.LateStart);
    //    EventBroadcaster.Instance.AddObserver(EventNames.Enemy_Events.ON_ENEMY_DEFEATED, this.DecrementEnemyCount);
    //}

    //public void LateStart()
    //{
    //    this._EnemyNums = UnitActionManager.Instance.UnitList.FindAll(u => u.Type == EUnitType.Enemy).Count;
    //}

    //public void DecrementEnemyCount()
    //{
    //    this._EnemyNums--;

    //    if (this._EnemyNums <= 0)
    //    {
    //        Parameters param = new Parameters();
    //        param.PutExtra("Level_Complete", true);
    //        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
    //    }
    //}
}
