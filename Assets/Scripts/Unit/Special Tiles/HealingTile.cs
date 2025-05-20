using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTile : SpecialTile, ISpecialTile
{
    protected override void Start()
    {
        this.type = EUnitType.SpecialTile;
    }

    public void ApplyEffect()
    {
        Unit affectedUnit = this.GetUnitOnTop(gameObject);

        affectedUnit.gainHealth(8, affectedUnit);
    }
}
