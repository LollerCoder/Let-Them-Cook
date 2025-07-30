using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asleep : Effect
{
    private int initHealth;

    public Asleep(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Asleep";

        //Debug.Log("Initializing health " + this.initHealth);
        this.initHealth = _effectMaker.HP;
    }

    public override void EffectAction(Unit unitAffected)
    {
        if (unitAffected.HP == this.initHealth)
        {
            PopUpManager.Instance.addPopUp(".....zzZZZ", unitAffected.transform);
        }
        
    }

    public override void AfterTurnAction(Unit unitAffected)
    {
        if (unitAffected.HP < this.initHealth)
        {
            EventBroadcaster.Instance.PostEvent(EventNames.Level3_Objectives.WOKE_UP);
            this.Duration = 1;
        }
    }

    public override void EffectAfterAction(Unit unitAffected)
    {
        PopUpManager.Instance.addPopUp("Huh... what?", unitAffected.transform);
        EventBroadcaster.Instance.PostEvent(EventNames.Level3_Objectives.WOKE_UP);
    }

    public override void AfterDeathAction(Unit unitAffected) {
        EventBroadcaster.Instance.PostEvent(EventNames.Level3_Objectives.WOKE_UP);
    }
}
