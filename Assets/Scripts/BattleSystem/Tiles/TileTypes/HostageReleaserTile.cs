using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageReleaserTile : Tile
{


    public override void ApplyEffect(Unit unit)
    {
        unit.AddEffect(new CapturedHostage(999, unit));
    }
}
