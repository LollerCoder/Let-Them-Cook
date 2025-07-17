using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoost : Effect
{
    private float baseAttack;
    private float attackBoost = 5.0f;

    public AttackBoost(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Attack Boost";

        this.baseAttack = _effectMaker.Attack;
        _effectMaker.ToggleBuffArrow(true);
    }

    public override void EffectAction(Unit unitAffected)
    {
        if (this.baseAttack != unitAffected.Attack) return;

        Debug.Log("Attack BOOST!");
        unitAffected.Attack += attackBoost;
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        unitAffected.Attack = this.baseAttack;
    }
    protected override void HideArrow(Unit unitAffected) {
        unitAffected.ToggleBuffArrow(false);
    }
}
