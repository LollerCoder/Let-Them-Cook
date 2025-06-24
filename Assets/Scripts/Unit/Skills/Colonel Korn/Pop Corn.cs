using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCorn : Skill
{
    public PopCorn()
    {
        this.skillName = "Pop Corn!";
        this.veggieType = EVeggie.NONE;
        this.skillType = ESkillType.AOE;

        this.skillRange = 2;

        this.defaultIcon = Resources.Load<Sprite>("Skills/skillDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/skillHighlighted");
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        foreach (Unit unit in this.GetNearbyUnits(origin))
        {
            PopUpManager.Instance.addPopUp("POP!", unit.transform);
            unit.TakeDamage(3, origin);
        }

        origin.AddEffect(new Dizzy(3, origin));
    }

    private List<Unit> GetNearbyUnits(Unit origin)
    {
        List<Unit> nearbyUnits = new List<Unit>();

        foreach (Unit unit in UnitActionManager.Instance.TurnOrder)
        {
            if (unit != origin &&
                Vector3.Distance(unit.transform.position, origin.transform.position) < 2.0f)
            {
                nearbyUnits.Add(unit);
            }
        }

        return nearbyUnits;
    }
}
