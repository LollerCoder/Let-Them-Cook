using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Objective : MonoBehaviour
{
    [Header("Initial Enemies")]
    [SerializeField]
    private List<Unit> _InitEnemies = new List<Unit>();

    [Header("Dialogue")]
    [SerializeField]
    private Dialogue _AlertDialogue;

    [Header("Gate Objects")]
    [SerializeField]
    private List<GameObject> _GateObjs = new List<GameObject>();
    [SerializeField]
    private List<Tile> _GateTiles = new List<Tile>();
    [SerializeField]
    private List<Unit> _EnemiesOnCannon = new List<Unit>();

    private bool _IsAlerted = false;

    int breachedCount = 0;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Level5_Events.BREACHED, this.SpawnEnemies);

        //foreach (Unit u in _InitEnemies)
        //{
        //    u.AddEffect(new Rooted(999, u));
        //}
    }

    public void SpawnEnemies()
    {
        if (this.breachedCount == 2) {
            this.EnemyCannonStartMoving();
        }
        else if (this.breachedCount < 2) {
            this.breachedCount++;
        }

        if (_IsAlerted) return;

        this._IsAlerted = true;

        //removing the gates
        foreach (GameObject gate in this._GateObjs)
        {
            gate.SetActive(false);
        }
        foreach (Tile tile in this._GateTiles)
        {
            tile.withProp = false;
            tile.isWalkable = true;
        }

        //Spawning the enemies
        DialogueManager.Instance.StartDialogue(this._AlertDialogue);
        EventBroadcaster.Instance.PostEvent(EventNames.EnemySpawn_Events.SPAWN_ENEMY);
    }

    private void EnemyCannonStartMoving() {
        this.breachedCount++;
        foreach (Unit enemy in this._EnemiesOnCannon) {
            enemy.OnWeapon = false;
        }
    }
}
