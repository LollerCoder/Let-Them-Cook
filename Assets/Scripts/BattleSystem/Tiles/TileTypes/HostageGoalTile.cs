using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tile where hostage needs to go to to win/get free
public class HostageGoalTile : Tile
{
        // Start is called before the first frame update
    void Start()
    {
        base.Start();
        this.tileType = ETileType.OBJECTIVE;
    }

    public override void ApplyEffect(Unit unit)
    {
        if (unit.GetEffect("Captured_Hostage").EffectName == null) return;

        Parameters param = new Parameters();

        param.PutExtra("End_Text", "Hostage Freed");
        param.PutExtra("Ally_Win", false);

        param.PutExtra("Level_Complete", true);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
    }
}
