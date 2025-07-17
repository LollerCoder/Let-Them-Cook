using UnityEngine;

public class Dizzy : Effect
{
    public Dizzy(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Dizzy";
        _effectMaker.ToggleDebuffArrow(true);
    }

    public override void EffectAction(Unit unitAffected)
    {
        PopUpManager.Instance.addPopUp("Dizzy!", unitAffected.transform);
        UnitActionManager.Instance.effectSkipTurn = true;
        //UnitActionManager.Instance.OnAttack = false;
        //UnitActionManager.Instance.OnMove = false;
        //UnitActionManager.Instance.hadAttacked = true;
        //UnitActionManager.Instance.hadMoved = true;
        //EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        Debug.Log("DONE");
    }
    protected override void HideArrow(Unit unitAffected) {
        unitAffected.ToggleDebuffArrow(false);
    }
}
