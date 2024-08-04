using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile : MonoBehaviour{
    private Material _mat;
    private Color _color;

    private int tileX;
    private int tileZ;
    public Vector2Int TilePos {
        get { return new Vector2Int(tileX, tileZ);}
        set { tileX = value.x; tileZ = value.y; }
    }

    public int cost;
    public int heuristic;
    public int F {
        get { return this.cost + this.heuristic; }
    }

    public Tile previousTile;
    public TileType tileType;
     

    public bool isWalkable;
    private void Start() {
        this._mat = this.gameObject.GetComponent<Renderer>().material;
        this._color = this._mat.color;    
    }
    public void UnHighlightTile() {
        this._mat.color = this._color;
    }
    public void HighlightWalkableTile() {
        if (this.isWalkable) { //just to make sure it wont be highlighted
            this._mat.color = Color.cyan;
        }
    }
    public void HighlightAttackableTile() {
        this._mat.color = Color.red;
    }
    public void OnTap() {
        UnitActionManager.Instance.TileTapped(this);
    }
}