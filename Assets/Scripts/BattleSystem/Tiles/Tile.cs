using NUnit.Framework;
using NUnit.Framework.Constraints;
using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
public class Tile : MonoBehaviour{
    private int tileX;
    private int tileZ;

    public bool withProp = false;

    [SerializeField]
    GameObject moveRange;

    private Material red;
    private Material green;
    private Material blue;

    private MeshRenderer meshRen;

    private Vector3 rangePos;

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

    private bool inWalkRange;

    public bool isWalkable;
    public void Start() {
        this.rangePos = this.transform.position;
        this.rangePos.y += 0.04f;

        //this.red = TileHelper.Attack;
        //this.blue = TileHelper.Walk;
        //this.green = TileHelper.Heal;

        //this.moveRange = GameObject.Instantiate(this.moveRange);

        //this.meshRen = this.moveRange.GetComponent<MeshRenderer>();

        //this.moveRange.transform.position = pos;
        //this.meshRen.material = this.blue;
        //this.moveRange.SetActive(false);
    }
    public void UnHighlightTile() {
        //Debug.Log("ADW");
        this.inWalkRange = false;
        //this.moveRange.SetActive(false);
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
    }
    public void UnHighlightTargetTile() {
        if (this.inWalkRange) {
            //this.moveRange.GetComponent<MeshRenderer>().material.color = this.blue.color;
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
            this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(this.rangePos, RangeType.WALK);
            return;
        }
        //this.moveRange.SetActive(false);
        TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
    }

    public void HighlightWalkableTile() {
        if (this.isWalkable) { //just to make sure it wont be highlighted
            this.inWalkRange = true;
            //this.meshRen.material = this.blue;
            //this.moveRange.SetActive(true);
            if (this.rangeIndicator != null) {
                TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
            }

            this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.WALK);
        }
    }
    public void HighlightAttackableTile() {
        //this.meshRen.material = this.red;
        //this.moveRange.SetActive(true);
        if (this.rangeIndicator != null) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        }
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.ATTACK);
    }

    public void HighlightHealableTile() {
        //this.meshRen.material = this.green;
        //this.moveRange.SetActive(true);
        if (this.rangeIndicator != null) {
            TileHelper.Instance.DeactivateRangeIndicator(this.rangeIndicator, this);
        }
        this.rangeIndicator = TileHelper.Instance.SpawnRangeIndicator(rangePos, RangeType.HEAL);
    }

    public virtual void ApplyEffect(Unit unit) {
        Debug.Log("DEFAULT");
    }

    public virtual void ApplyOnUnitStart()
    {
        Debug.Log("yehey");
    }
}
