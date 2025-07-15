using UnityEngine;
public class Tile : MonoBehaviour{
    private int tileX;
    private int tileZ;

    public bool withProp = false;

    [SerializeField]
    protected Vector3 rangePos;

    public GameObject rangeIndicator = null;

    public Vector2Int TilePos {
        get { return new Vector2Int(tileX, tileZ);}
        set { tileX = value.x; tileZ = value.y; }
    }

    [SerializeField]
    private int baseCost = 1;
    private int Heuristic;
    
    public int bCost {
        get { return this.baseCost; }
        set { this.baseCost = value; }
    }
    public int heuristic {
        get { return this.Heuristic; }
        set { this.Heuristic = value; }
    }
    
    public Tile previousTile;

    [SerializeField]
    protected ETileType TileType;

    public ETileType tileType {
        get { return this.TileType; }
        set {  TileType = value; }
    }

    protected bool inWalkRange;

    public bool isWalkable;
    public void Start() {
        this.rangePos = this.transform.position;
        this.rangePos.y += 0.04f;
    }
    public virtual void UnHighlightTile() {
        this.inWalkRange = false;
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
    }

    public virtual void UnHighlightTargetTile() {
        if (this.inWalkRange) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
            this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.WALK);
            return;
        }
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
    }

    public virtual void HighlightWalkableTile() {
        if (this.isWalkable) { //just to make sure it wont be highlighted
            this.inWalkRange = true;
            if (this.rangeIndicator != null) {
                TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
            }
            this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.WALK);
        }
    }
    public void HighlightAttackableTile() {
        if (this.rangeIndicator != null) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        }
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.ATTACK);
    }

    public void HighlightHealableTile() {
        if (this.rangeIndicator != null) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        }
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.HEAL);
    }

    public void HighlightCurrentTile(EUnitType type) {
        if (this.rangeIndicator != null) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        }

        if (type == EUnitType.Ally) {
            this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.CURRENT);
        }
        else {
            this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.ATTACK);
        }
    }

    public virtual void ApplyEffect(Unit unit) {
        //Debug.Log("DEFAULT");
    }

    public virtual void ApplyOnUnitStart()
    {
        Debug.Log("yehey");
    }
}
