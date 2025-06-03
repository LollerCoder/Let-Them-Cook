using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageReleaserTile : Tile
{
    [SerializeField]
    private GameObject caged_hostage;

    [Header("Hostage")]
    [SerializeField]
    private GameObject hostage_template;

    [SerializeField]
    private Tile tile_spawn;

    private bool isReleased = false;

    private new void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.HOSTAGE_FREE, this.FreeHostage);
        
        base.Start();
    }

    public void FreeHostage()
    {
        isReleased = true;
    }

    public override void ApplyEffect(Unit unit)
    {
        if (isReleased) return;

        EventBroadcaster.Instance.PostEvent(EventNames.HostageRescue_Events.HOSTAGE_FREE);
        EventBroadcaster.Instance.PostEvent(EventNames.EnemySpawn_Events.SPAWN_ENEMY);

        caged_hostage.SetActive(false);

        tile_spawn.tileType = ETileType.DEFAULT;

        List<Effect> effects = new List<Effect>();

        effects.Add(new CapturedHostage(999, unit));

        UnitManager.Instance.addUnit("Garlic Hostage",
            hostage_template,
            tile_spawn,
            EUnitType.Ally,
            effects
            );
        //unit.AddEffect(new CapturedHostage(999, unit));
    }
}
