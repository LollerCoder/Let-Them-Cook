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

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.WOKE_UP, this.SpawnEnemies);
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
        Unit enemyKeyHolder = enemyObjs[Random.Range(0, 1)].GetComponent<Unit>();
        enemyKeyHolder.AddEffect(new KeyHolder(999, enemyKeyHolder));

        for (int i = 0; i < enemyObjs.Count; i++)
        {
            UnitManager.Instance.addUnit("Enemy",
                enemyObjs[i],
                spawnTile[i],
                EUnitType.Enemy,
                new List<Effect>()
                );
        }
    }
}
