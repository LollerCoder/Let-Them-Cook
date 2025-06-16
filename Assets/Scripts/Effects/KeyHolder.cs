using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventNames;

public class KeyHolder : Effect
{
    public KeyHolder(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Key Holder";
    }

    public override void EffectAction(Unit unitAffected)
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Tile_Events.GOAL_ARROW_UNHIDE);
    }

    public override void AfterTurnAction(Unit unitAffected)
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Tile_Events.GOAL_ARROW_HIDE);
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        this.Duration += 99;
    }

    public override void AfterDeathAction(Unit unitAffected)
    {
        if (unitAffected.Type == EUnitType.Enemy)
            EventBroadcaster.Instance.PostEvent(EventNames.Level3_Objectives.KEY_FOUND);
    }
}
