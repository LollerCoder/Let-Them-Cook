using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTile : Tile {
    public new void Start() {
        base.Start();
        this.tileType = ETileType.HEAL;

        if (this.rangeIndicator != null) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        }

        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.HEALTILE);
    }
    public override void ApplyEffect(Unit _unit) {
        _unit.Heal();
        Debug.Log("ONHEAL");
    }
    public override void UnHighlightTile() {
        this.inWalkRange = false;
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.HEALTILE);
    }
    public override void UnHighlightTargetTile() {
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.HEAL);
    }
    public override void HighlightWalkableTile() {
        if (this.isWalkable) { //just to make sure it wont be highlighted
            this.inWalkRange = true;
            if (this.rangeIndicator != null) {
                TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
            }
            this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.HEALTILE);
        }
    }
}
