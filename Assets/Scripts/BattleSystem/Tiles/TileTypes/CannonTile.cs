using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTile : Tile {
    [SerializeField]
    private Cannon cannon;
    public new void Start() {
        base.Start();
        this.tileType = ETileType.OBJECTIVE;

        if (this.rangeIndicator != null) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        }

        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.OBJECTIVE);
    }
    public override void UnHighlightTile() {
        this.inWalkRange = false;
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.OBJECTIVE);
    }
    public override void UnHighlightTargetTile() {
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.OBJECTIVE);
    }
        public override void HighlightWalkableTile() {
        if (this.isWalkable) { //just to make sure it wont be highlighted
            this.inWalkRange = true;
            if (this.rangeIndicator != null) {
                TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
            }
            this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.OBJECTIVE);
        }
    }
}
