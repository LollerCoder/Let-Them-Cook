using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageReleaserTile : Tile
{
    [SerializeField]
    private GameObject hostage_template;

    [SerializeField]
    private Tile tile_spawn;

    public override void ApplyEffect(Unit unit)
    {
        tile_spawn.tileType = ETileType.DEFAULT;
        UnitManager.Instance.addUnit("Garlic Hostage",
            hostage_template,
            tile_spawn.gameObject.transform.position,
            EUnitType.Ally
            );
        //unit.AddEffect(new CapturedHostage(999, unit));
    }
}
