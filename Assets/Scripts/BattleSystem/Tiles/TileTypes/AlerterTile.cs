using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when this tile is stepped on, alert the enemy and send reinforcements
public class AlerterTile : Tile
{
    public new void Start()
    {
        base.Start();
        this.tileType = ETileType.HEAL;
    }
    public override void ApplyEffect(Unit _unit)
    {
        EventBroadcaster.Instance.PostEvent(EventNames.Level5_Events.BREACHED);
    }
}
