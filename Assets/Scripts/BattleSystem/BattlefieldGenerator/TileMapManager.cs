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

    // Start is called before the first frame update
    void Start() {
        Tile[] Tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);

        foreach (Tile tile in Tiles) {
            Vector2Int tilePosition = new Vector2Int(Mathf.RoundToInt(tile.transform.position.x), // to ensure the position is not a float
                                                     Mathf.RoundToInt(tile.transform.position.z));

            tile.isWalkable = true;
            tile.TilePos = tilePosition;
            tile.tileType.name = tile.tileType.tile.ToString();

            if (!this._tileMap.ContainsKey(tilePosition)) { // avoid any duplicate keys
                this._tileMap.Add(tilePosition, tile);
            }
        }
    }

    public void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != null) {
            Destroy(this.gameObject);
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
        //for (int x = 0; x < this._mapSizeX; x++) {
        //    for (int z = 0; z < this._mapSizeZ; z++) {
        //        if (this._tileMap[new Vector2Int(x, z)].tileType.tile != ETileType.TREES) {
        //            this._tileMap[new Vector2Int(x, z)].isWalkable = true;
        //        }
        //    }
        //}

        foreach(var i in this._tileMap) {
            i.Value.isWalkable = true;
        }
    }

}
