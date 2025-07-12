using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageReleaserTile : Tile
{

    [Header("Hostage")]
    [SerializeField]
    private GameObject caged_hostage_prop;
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
        if (unit.Type != EUnitType.Ally) return;


        caged_hostage_prop.SetActive(false);

        tile_spawn.tileType = ETileType.DEFAULT;
        tile_spawn.withProp = false;
        tile_spawn.isWalkable = false;

        UnitManager.Instance.addUnit("Garlic Hostage",
            hostage_template,
            tile_spawn,
            EUnitType.Ally,
            new List<Effect>()
            );

        this.GiveAlliesEffect(unit);

        EventBroadcaster.Instance.PostEvent(EventNames.HostageRescue_Events.HOSTAGE_FREE);
        EventBroadcaster.Instance.PostEvent(EventNames.EnemySpawn_Events.SPAWN_ENEMY);

        //unit.AddEffect(new CapturedHostage(999, unit));
    }

    private void GiveAlliesEffect(Unit orig)
    {
        foreach (Unit unit in UnitActionManager.Instance.UnitList.FindAll(u => u.Type == EUnitType.Ally))
        {
            unit.AddEffect(new MissionEscapeEffect(999, orig));
        }
    }
}
