using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGenerator: MonoBehaviour {
    public bool enable = false;

    [SerializeField]
    private List<GameObject> _parkPrefabs;

    [SerializeField]
    private Transform _parent;
    public void GenerateTreePrefab(int x, int z) {
        GameObject tree = Instantiate(this._parkPrefabs[0], new Vector3(x, 0.5f, z), Quaternion.identity, this._parent.transform);
        tree.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
}
