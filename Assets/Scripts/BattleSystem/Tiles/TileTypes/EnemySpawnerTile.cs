using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTile : Tile
{
    [Header("Enemy Template")]
    [SerializeField]
    private GameObject enemy_template;

    // Start is called before the first frame update
    public new void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.EnemySpawn_Events.SPAWN_ENEMY, this.SpawnEnemy);
        base.Start();   
    }

    public void SpawnEnemy()
    {
        UnitManager.Instance.addUnit("Enemy",
            enemy_template,
            this,
            EUnitType.Enemy,
            new List<Effect>()
            );
    }

    public override void ApplyEffect(Unit unit)
    {

    }
}
