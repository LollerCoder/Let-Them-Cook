using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHover : MonoBehaviour {
    void Update() {
        var Tile = this.OnHitTile();

        if (Tile != null) {
            Tile tile = Tile.GetComponent<Tile>();
            if (tile != null) {
                this.transform.position = new Vector3(tile.transform.position.x, 
                    tile.transform.position.y + 1.05f, // set it just above the tile
                    tile.transform.position.z);
            }
        }
    }

    private GameObject OnHitTile() {
        GameObject hitTile = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int ignoreLayer = ~LayerMask.GetMask("Units"); // ignore the unit layer

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayer)) {
            hitTile = hit.collider.gameObject;
        }

        return hitTile;
    }
}
