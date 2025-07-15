using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTile : Tile {
    [SerializeField]
    private Cannon cannon;
    public new void Start() {
        base.Start();
        this.tileType = ETileType.CANNON;

        if (this.rangeIndicator != null) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        }

        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.CANNON);
    }
    public override void UnHighlightTile() {
        this.inWalkRange = false;
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.CANNON);
    }
    public override void UnHighlightTargetTile() {
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.CANNON);
    }
}
