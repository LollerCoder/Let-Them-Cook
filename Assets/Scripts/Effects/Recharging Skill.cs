using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class RechargingSkill : Effect
{
    public string SkillName; //name of skill being recharged
    public RechargingSkill(int _duration, Unit _effectMaker, string skillName) : base(_duration, _effectMaker)
    {
        this.EffectName = "Recharging Skill";
        SkillName = skillName;
    }

    public override void EffectAction(Unit unitAffected)
    {
        PopUpManager.Instance.addPopUp("Recharging!", this.EffectMaker.gameObject.transform);
    }
}
