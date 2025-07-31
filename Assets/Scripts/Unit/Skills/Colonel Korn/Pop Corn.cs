using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCorn : Skill
{
    public PopCorn()
    {
        this.skillName = "Pop Corn!";
        this.veggieType = EVeggie.NONE;
        this.skillType = ESkillType.GROUND_SLAM;

        this.skillRange = 2;
        SkillDatabase.Instance.GetSkillSprite(this);
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        foreach (Unit unit in this.GetNearbyUnits(origin))
        {
            if (unit == target) continue; 
            PopUpManager.Instance.addPopUp("POP!", unit.transform);
            unit.TakeDamage(4, origin);
        }

        target.TakeDamage(5, origin);
        target.AddEffect(new Dizzy(3, target));
    }

    private List<Unit> GetNearbyUnits(Unit origin)
    {
        List<Unit> nearbyUnits = new List<Unit>();

        foreach (ITurnTaker turnTaker in UnitActionManager.Instance.TurnOrder)
        {
            if (turnTaker is not Unit) continue;

            Unit unit = turnTaker as Unit;
            if (unit != origin &&
                Vector3.Distance(unit.transform.position, origin.transform.position) < 2.0f)
            {
                nearbyUnits.Add(unit);
            }
        }

        return nearbyUnits;
    }
}
