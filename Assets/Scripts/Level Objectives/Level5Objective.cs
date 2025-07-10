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

    private bool _IsAlerted = false;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Level5_Events.BREACHED, this.SpawnEnemies);

        foreach (Unit u in _InitEnemies)
        {
            u.AddEffect(new Rooted(999, u));
        }
    }

    public void SpawnEnemies()
    {
        if (_IsAlerted) return;

        this._IsAlerted = true;

        DialogueManager.Instance.StartDialogue(this._AlertDialogue);
        EventBroadcaster.Instance.PostEvent(EventNames.EnemySpawn_Events.SPAWN_ENEMY);
    }
}
