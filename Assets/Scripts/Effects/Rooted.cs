
public class Rooted : Effect
{
    public Rooted(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Rooted";
    }

    public override void EffectAction(Unit unitAffected)
    {
        UnitActionManager.Instance.OnMove = false;
        UnitActionManager.Instance.hadMoved = true;
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        this.Duration += 999;
    }
}
