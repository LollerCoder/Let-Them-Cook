using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//will be used as a flag to indicate Unit was a hostage
public class CapturedHostage : Effect
{
    public CapturedHostage(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Captured_Hostage";
    }

    public override void EffectAction(Unit unitAffected)
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Tile_Events.GOAL_ARROW_UNHIDE);
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        this.Duration += 999;
    }

    public override void AfterTurnAction(Unit unitAffected)
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Tile_Events.GOAL_ARROW_HIDE);
    }
}
