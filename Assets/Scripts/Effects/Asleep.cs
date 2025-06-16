using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asleep : Effect
{
    private int initHealth;

    public Asleep(int _duration, Unit _effectMaker) : base(_duration, _effectMaker)
    {
        this.EffectName = "Asleep";

        this.initHealth = _effectMaker.HP;
    }

    public override void EffectAction(Unit unitAffected)
    {
        if (unitAffected.HP < this.initHealth)
        {
            EventBroadcaster.Instance.PostEvent(EventNames.EnemySpawn_Events.SPAWN_ENEMY);
            this.Duration = 0;
            return;
        }

        PopUpManager.Instance.addPopUp(".....zzZZZ", unitAffected.transform);
        UnitActionManager.Instance.OnAttack = false;
        UnitActionManager.Instance.OnMove = false;
        UnitActionManager.Instance.hadAttacked = true;
        UnitActionManager.Instance.hadMoved = true;
    }
}
