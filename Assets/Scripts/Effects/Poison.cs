using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Effect
{
    private float damage = 5.0f;

    public Poison(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Poison";
    }

    public override void EffectAction(Unit unitAffected)
    {
        //Debug.Log("Poison!");
        unitAffected.TakeDamage(this.damage, this.EffectMaker);
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        unitAffected.ChangeColor(Color.white);
    }
}
