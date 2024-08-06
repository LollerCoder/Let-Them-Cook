using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelector : MonoBehaviour {
    public static SceneSelector Instance = null;

    [SerializeField]
    private EnemyController enemyController;

    public EBattleScene Scene;

    private void Start() {

        TileMapGenerator.Instance.StartMap();
    }

    public void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else if(Instance != null) {
            Destroy(this.gameObject);
        }
    }
}
