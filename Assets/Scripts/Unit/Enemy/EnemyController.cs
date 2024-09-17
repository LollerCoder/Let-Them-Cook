using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemyController: MonoBehaviour {
    [SerializeField]
    private int _normalEnemies;
    [SerializeField]
    private List<Vector3> _positions;
    [SerializeField]
    private Transform _scene;
    [SerializeField]
    private List<Unit> _enemies;

    public bool boss = false;

    private void SpawnEnemy(){
        Unit unit = null;
        
        if (!this.boss) {
            for(int i = 0;  i < _normalEnemies; i++) {
                int rand = Random.Range(0, this._enemies.Count - 1);
                unit = Instantiate(this._enemies[rand], this._positions[i], Quaternion.identity, this._scene.transform);
                unit.transform.rotation = Quaternion.Euler(0,-180,0);
                unit.Type = EUnitType.Enemy;
                unit.gameObject.layer = this.gameObject.layer;
            }
        }
        else {
            
            for (int i = 0; i < 3; i++) {
                int rand = Random.Range(0, this._enemies.Count - 1);
                unit = Instantiate(this._enemies[rand], this._positions[i], Quaternion.identity, this._scene.transform);
                unit.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            unit = Instantiate(this._enemies[3], this._positions[3], Quaternion.identity, this._scene.transform);
            unit.transform.rotation = Quaternion.Euler(0, -180, 0);
            unit.Type = EUnitType.Boss;
            unit.gameObject.layer = this.gameObject.layer;
        }

        
    }

    private void Start() {
        this.SpawnEnemy();
    }
}
