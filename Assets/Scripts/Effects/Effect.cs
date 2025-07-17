using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    [SerializeField]
    public string EffectName = "Effect";

    [SerializeField]
    public int Duration = 1;

    public Unit EffectMaker;

    public EnumEffects effectType;

    public Effect(int _duration, Unit _effectMaker)
    {
        EffectMaker = _effectMaker;
        Duration = _duration;
    }

    public void ApplyEffect(Unit unitAffected)
    {
        if (Duration <= 0) {
            this.HideArrow(unitAffected);
            return;
        }

        EffectAction(unitAffected);

        Duration--;
    }

    public virtual void EffectAction(Unit unitAffected)
    {
        //Debug.Log("Doing " + this.EffectName);
    }

    //function that will be called once the duration is finished
    public virtual void EffectAfterAction(Unit unitAffected)
    {
        Debug.Log(this.EffectName + " has finished!");
    }

    //function called after the end turn of the unit affected
    public virtual void AfterTurnAction(Unit unitAffected)
    {

    }

    public virtual void AfterDeathAction(Unit unitAffected)
    {

    }
    protected virtual void HideArrow(Unit unitAffected) {

    }
}
