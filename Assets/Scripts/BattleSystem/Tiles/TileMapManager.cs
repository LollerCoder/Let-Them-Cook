using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileMapManager : MonoBehaviour {
    public static TileMapManager Instance = null;

    private Dictionary<Vector2Int, Tile> _tileMap = new Dictionary<Vector2Int, Tile>();
    public Dictionary<Vector2Int, Tile> TileMap {
        get { return _tileMap; }
    }

    [SerializeField]
    private List<TileMapProps> Props = new List<TileMapProps>();

    [SerializeField]
    private Transform PropField;

    // Start is called before the first frame update
    void Start() {
        Tile[] Tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (Tile tile in Tiles) {
            Vector2Int tilePosition = new Vector2Int(Mathf.RoundToInt(tile.transform.position.x), // to ensure the position is not a float
                                                     Mathf.RoundToInt(tile.transform.position.z));

            tile.isWalkable = true;
            tile.TilePos = tilePosition;

            if (!this._tileMap.ContainsKey(tilePosition)) { // avoid any duplicate keys
                this._tileMap.Add(tilePosition, tile);
            }
        }
        this.PropGenerator();
        this.UpdateTileWithProps();
    }

    public void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != null) {
            Destroy(this.gameObject);
        }
    }

    private void PropGenerator() {
        foreach(TileMapProps props in this.Props) {
            Instantiate(props.value, props.key, props.value.transform.rotation, this.PropField);
        }
    }

    private void UpdateTileWithProps() {
        Vector2Int key;
        foreach (TileMapProps props in this.Props) {
            key = new Vector2Int((int)props.key.x, (int)props.key.z);

            if (this.TileMap.ContainsKey(key)) {
                this.TileMap[key].tileType = ETileType.UNPASSABLE;
                this.TileMap[key].isWalkable = false;
                Debug.Log(this.TileMap[key].name);
                Debug.Log(key);
            }
        }
    }

    public List<Tile> GetNeighborTiles(Tile currentTile, List<Tile> inRangeTiles) {
        Dictionary<Vector2Int, Tile> availableTiles = new Dictionary<Vector2Int, Tile>();

        if (inRangeTiles.Count > 0) {
            foreach (Tile tile in inRangeTiles) {
                availableTiles.Add(tile.TilePos, tile);
            }
        }
        else {
            availableTiles = this._tileMap;
        }

        List<Tile> neighbors = new List<Tile>();

        //front
        Vector2Int tempNeighbor = new Vector2Int(currentTile.TilePos.x, currentTile.TilePos.y + 1);
        if (availableTiles.ContainsKey(tempNeighbor)) {
            neighbors.Add(availableTiles[tempNeighbor]);
        }

        //back
        tempNeighbor = new Vector2Int(currentTile.TilePos.x, currentTile.TilePos.y - 1);
        if (availableTiles.ContainsKey(tempNeighbor)) {
            neighbors.Add(availableTiles[tempNeighbor]);
        }

        //left
        tempNeighbor = new Vector2Int(currentTile.TilePos.x - 1, currentTile.TilePos.y);
        if (availableTiles.ContainsKey(tempNeighbor)) {
            neighbors.Add(availableTiles[tempNeighbor]);
        }

        //right
        tempNeighbor = new Vector2Int(currentTile.TilePos.x + 1, currentTile.TilePos.y);
        if (availableTiles.ContainsKey(tempNeighbor)) {
            neighbors.Add(availableTiles[tempNeighbor]);
        }

        return neighbors;
    }
    public void UpdateTile() {
        foreach(var tileMap in this._tileMap) {
            if(tileMap.Value.tileType != ETileType.UNPASSABLE) {
                tileMap.Value.isWalkable = true;
            }
        }
    }

}

[Serializable]
public class TileMapProps {
    public Vector3 key;
    public GameObject value;
    public TileMapProps(Vector3 key, GameObject value) {
        this.key = key;
        this.value = value;
    }
}
