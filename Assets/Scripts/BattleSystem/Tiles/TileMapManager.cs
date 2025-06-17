using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TileMapManager : MonoBehaviour {
    public static TileMapManager Instance = null;

    private Dictionary<Vector2Int, Tile> _tileMap = new Dictionary<Vector2Int, Tile>();
    public Dictionary<Vector2Int, Tile> TileMap {
        get { return _tileMap; }
    }

    public float unitLock;

    // Start is called before the first frame update
    void Start() {
        Tile[] Tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (Tile tile in Tiles) {

            Vector2Int tilePosition;
            tilePosition = new Vector2Int(Mathf.RoundToInt(tile.transform.position.x), // to ensure the position is not a float
                         Mathf.RoundToInt(tile.transform.position.z));

            if (tile.withProp) {
                tile.tileType = ETileType.UNPASSABLE;
                tile.isWalkable = false;
            }
            else {
                tile.isWalkable = true;
            }

            tile.TilePos = tilePosition;

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
                if (availableTiles.ContainsKey(tile.TilePos)) {
                    Debug.Log(tile.TilePos);
                    continue;
                }
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
            if (tileMap.Value.tileType != ETileType.UNPASSABLE) {
                tileMap.Value.isWalkable = true;

            }
        }
    }

}
