
public class MissionEscapeEffect : Effect
{
    public MissionEscapeEffect(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Mission Escape";
    }

    public override void EffectAction(Unit unitAffected)
    {
        EventBroadcaster.Instance.PostEvent(EventNames.HostageRescue_Events.GOAL_ARROW_UNHIDE);
    }

    public override void AfterTurnAction(Unit unitAffected)
    {
        EventBroadcaster.Instance.PostEvent(EventNames.HostageRescue_Events.GOAL_ARROW_HIDE);
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        this.Duration += 999;
    }
}
