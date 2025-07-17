using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Effect
{
    private float damage = 2.0f;

    public Poison(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Poison";
        _effectMaker.ToggleDebuffArrow(true);
    }

    public override void AfterTurnAction(Unit unitAffected)
    {
        //Debug.Log("Poison!");
        unitAffected.TakeDamage(this.damage, this.EffectMaker);
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        unitAffected.ChangeColor(Color.white);
    }
    protected override void HideArrow(Unit unitAffected) {
        unitAffected.ToggleDebuffArrow(false);
    }
}
