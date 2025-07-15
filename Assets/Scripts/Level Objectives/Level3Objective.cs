using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Objective : MonoBehaviour
{
    [Header("Initial Enemy")]
    [SerializeField]
    private Unit guard;

    [Header("Spawn Enemy Data")]
    [SerializeField]
    private List<GameObject> enemyObjs = new List<GameObject>();
    [SerializeField]
    private List<Tile> spawnTile = new List<Tile>();

    [Header("Wave Spawn Enemy Data")]
    [SerializeField]
    private List<GameObject> enemyObjs_wave2 = new List<GameObject>();
    [SerializeField]
    private List<Tile> spawnTile_wave2 = new List<Tile>();

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.WOKE_UP, this.SpawnEnemies);
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.KEY_FOUND, this.GiveAllyKey);
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.KEY_FOUND, this.SpawnWave2Enemies);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, this.LateStart);
        
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveAllObservers();
    }

    public void LateStart()
    {
        guard.AddEffect(new Asleep(7, guard));
    }

    public void SpawnEnemies()
    {
        //getting the keyholder
        int keyHolderIndex = Random.Range(0, 1);

        List<Effect> effectsAdded = new List<Effect>();

        for (int i = 0; i < enemyObjs.Count; i++)
        {
            effectsAdded.Clear();

            if (i == keyHolderIndex) effectsAdded.Add(new KeyHolder(99,guard));

            UnitManager.Instance.addUnit("Enemy",
                enemyObjs[i],
                spawnTile[i],
                EUnitType.Enemy,
                effectsAdded
                );
        }

        EventBroadcaster.Instance.PostEvent(EventNames.EnemySpawn_Events.SPAWN_ENEMY);
    }

    public void SpawnWave2Enemies(Parameters param)
    {
        for (int i = 0; i < enemyObjs_wave2.Count; i++)
        {
            UnitManager.Instance.addUnit("Enemy",
                enemyObjs_wave2[i],
                spawnTile_wave2[i],
                EUnitType.Enemy,
                new List<Effect>()
                );
        }
    }

    public void GiveAllyKey(Parameters param)
    {
        Unit enemyHolder = param.GetUnitExtra("Enemy Keyholder");
        Unit unit2Give = UnitActionManager.Instance.GetClosestUnit(enemyHolder, EUnitType.Enemy);
        unit2Give.AddEffect(new KeyHolder(999, enemyHolder));
    }
}
