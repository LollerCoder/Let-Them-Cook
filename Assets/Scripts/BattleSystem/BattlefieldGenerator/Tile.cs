using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour{
    private Material _mat;
    private Color _color;

    private int tileX;
    private int tileZ;

    public Vector2Int TilePos {
        get { return new Vector2Int(tileX, tileZ);}
        set { tileX = value.x; tileZ = value.y; }
    }

    private int Cost;
    private int Heuristic;

    public int cost {
        get { return this.Cost; }
        set { this.Cost = value; }
    }

    public int heuristic {
        get { return this.Heuristic; }
        set { this.Heuristic = value; }
    }

    public int F {
        get { return this.Cost + this.Heuristic; }
    }
    
    public Tile previousTile;

    public ETileType tileType;

    public bool isWalkable;
    protected void Start() {
        this._mat = this.gameObject.GetComponent<Renderer>().material;
        this._color = this._mat.color;
        this.tileType = ETileType.DEFAULT;
        
    }
    public void UnHighlightTile() {
        this._mat.color = this._color;
    }
    public void HighlightWalkableTile() {
        if (this.isWalkable) { //just to make sure it wont be highlighted
            this._mat.color = Color.magenta;
        }
    }
    public void HighlightAttackableTile() {
        this._mat.color = Color.red;
    }

    public void HighlightHealableTile() {
        this._mat.color = Color.green;
    }

    public void OnMouseUp() {
        if (!EventSystem.current.IsPointerOverGameObject()) { // to make sure that it wont be clickable when behind a UI element
            UnitActions.TileTapped(this);
        }
    }
    public virtual void ApplyEffect(Unit unit) {
        Debug.Log("DEFAULT");
    }
}
