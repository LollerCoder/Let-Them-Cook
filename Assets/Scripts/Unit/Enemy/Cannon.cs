using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : SpecialUnits {

    public List<Tile> targetTiles = new List<Tile>();
    public LayerMask unitLayer;
    public void Start() {
        this.Sprite = holder;
        this.Speed = 10;
        UnitActionManager.Instance.TurnOrder.Add(this);
    }

    public override void Action() {
        foreach (Tile tile in this.targetTiles) {
            Ray ray = new Ray(tile.transform.position, Vector3.up);
            Debug.DrawRay(tile.transform.position, Vector3.up, Color.red, 5f);
            if (Physics.Raycast(ray, out RaycastHit hit, 50.0f, this.unitLayer)) {
                if (hit.collider.gameObject.GetComponent<Unit>() is Unit unit) {
                    unit.TakeDamageFromTile(10);
                }
            }
        }

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }
}
