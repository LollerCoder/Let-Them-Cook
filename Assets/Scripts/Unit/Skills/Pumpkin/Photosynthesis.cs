using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class Photosynthesis : Skill
{
    public Photosynthesis()
    {
        this.skillName = "Photosynthesis";
        this.veggieType = EVeggie.CARROT; 
        this.skillType = ESkillType.HEAL;

        //for skill progressions
        this.cost = 15;
        this.skillRange = 3;
        SkillDatabase.Instance.GetSkillSprite(this);
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        target.gainHealth(4, origin);
    }

    public override void HighlightTile(Unit unit) {
        unit.Tile.HighlightHealableTile();
    }
}