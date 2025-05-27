using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageReleaserTile : Tile
{
    void Start()
    {
        base.Start();
        this.tileType = ETileType.OBJECTIVE;
    }

    public override void ApplyEffect(Unit unit)
    {
        unit.AddEffect(new CapturedHostage(999, unit));
    }
}
