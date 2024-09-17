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
                    tile.transform.position.y + 0.55f, 
                    tile.transform.position.z);

                Debug.Log("Hit");
            }
        }
    }

    private GameObject OnHitTile() {
        GameObject hitTile = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            hitTile = hit.collider.gameObject;
        }

        return hitTile;
    }
}
