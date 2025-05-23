
public class Dizzy : Effect
{
    public Dizzy(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Dizzy";
    }

    public override void EffectAction(Unit unitAffected)
    {
        PopUpManager.Instance.addPopUp("Dizzy!", unitAffected.transform);
        UnitActionManager.Instance.OnAttack = false;
        UnitActionManager.Instance.OnMove = false;
        UnitActionManager.Instance.hadAttacked = true;
        UnitActionManager.Instance.hadMoved = true;
    }

    public override void EffectAfterAction(Unit unitAffected)
    {

    }
}
