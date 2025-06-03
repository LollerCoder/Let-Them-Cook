using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileHover : MonoBehaviour {
    [SerializeField]
    float yPos;
    void Update() {
        var Tile = this.OnHitTile();

        if (Tile != null) {
            Tile tile = Tile.GetComponent<Tile>();
            if (tile != null) {
                this.transform.position = new Vector3(tile.transform.position.x, 
                    tile.transform.position.y + this.yPos, // set it just above the tile
                    tile.transform.position.z);
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            Tile hitTile = null;

            RaycastHit hit;
            Vector3 origin = transform.position;
            Vector3 direction = Vector3.down;

            float distance = 1f;
            int ignoreLayer = ~LayerMask.GetMask("Units"); // ignore the unit layer

            if (Physics.Raycast(origin, direction, out hit, distance, ignoreLayer)) {
                hitTile = hit.collider.gameObject.GetComponent<Tile>();
            }

            if (hitTile && (!EventSystem.current.IsPointerOverGameObject() && !UnitActions.bGoal)) { // to make sure that it wont be clickable
                                                                                                     // when behind a UI element
                UnitActions.bGoal = true;
                hitTile.TileClick();
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
