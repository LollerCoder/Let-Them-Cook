using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour {

    public static TileMapGenerator Instance = null;

    [SerializeField]
    private GameObject[] _tilePrefabs;

    [SerializeField]
    private Transform _TileField;

    [SerializeField]
    private PrefabGenerator _PrefabsGenerator;

    private ETileType[,] _tiles;

    private Dictionary<ETileType, TileType> _tileType = new Dictionary<ETileType, TileType>();

    private Dictionary<Vector2Int, Tile> _tileMap = new Dictionary<Vector2Int, Tile>();
    public Dictionary<Vector2Int, Tile> TileMap {
        get { return _tileMap; }
    }

    private int _mapSizeX = 10;
    private int _mapSizeZ = 10;

    public void TilePrefabGenerator() {
        for (int x = 0; x < this._mapSizeX; x++) {
            for (int z = 0; z < this._mapSizeZ; z++) {  
                //        list of tile types [ 2d array of tile types with their position on the grid ] 
                TileType tiletype = this._tileType[this._tiles[x, z]];
                GameObject tempTile = (GameObject)Instantiate(tiletype.tilePrefab, new Vector3(x, 0, z), Quaternion.identity, this._TileField);

                Tile tile = tempTile.GetComponent<Tile>();

                tile.TilePos = new Vector2Int(x, z);

                tile.tileType = tiletype;
                if (tile.tileType.tile != ETileType.TREES) {
                    tile.isWalkable = true;
                }
                else {
                    // make the tile with obstacle not walkable
                    this._PrefabsGenerator.GenerateTreePrefab(x, z);
                    tile.isWalkable = false;
                }

                this._tileMap.Add(tile.TilePos, tile);
            }
        }
    }
    public void TileGenerator() {
        this._tiles = new ETileType[this._mapSizeX, this._mapSizeZ];

        this.GenerateGrass();
        this.GeneratePath();
    }
    public void GenerateTileTypes() {
        // assign each tile with their unique name, prefab, and type
        // then add it to the list of tile types
        TileType tiletype = new TileType();
        tiletype.name = "GRASS";
        tiletype.tilePrefab = this._tilePrefabs[0];
        tiletype.tile = ETileType.GRASS;
        this._tileType.Add(ETileType.GRASS, tiletype);

        tiletype = new TileType();
        tiletype.name = "DIRT";
        tiletype.tilePrefab = this._tilePrefabs[1];
        tiletype.tile = ETileType.DIRT;
        this._tileType.Add(ETileType.DIRT, tiletype);

        tiletype = new TileType();
        tiletype.name = "TREES";
        tiletype.tilePrefab = this._tilePrefabs[0];
        tiletype.tile = ETileType.TREES;
        this._tileType.Add(ETileType.TREES, tiletype);
    }
    public List<Tile> GetNeighborTiles(Tile currentTile, List<Tile> inRangeTiles) {
        Dictionary<Vector2Int, Tile> availableTiles = new Dictionary<Vector2Int, Tile>();

        if(inRangeTiles.Count > 0) {
            foreach(Tile tile in inRangeTiles) {         
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
    
    private void Start() {
        this.GenerateTileTypes();
        this.TileGenerator();
        this.TilePrefabGenerator();
    }
    public void StartMap() {

    }
    private void GenerateGrass() {
        for (int x = 0; x < this._mapSizeX; x++) {
            for (int z = 0; z < this._mapSizeZ; z++) {
                this._tiles[x, z] = ETileType.GRASS;
            }
        }

        for (int z = 0; z < 10; z++) {
            this._tiles[0, z] = ETileType.TREES;
        }

        this._tiles[2, 9] = ETileType.TREES;
        this._tiles[2, 8] = ETileType.TREES;
        this._tiles[3, 9] = ETileType.TREES;

        this._tiles[2, 5] = ETileType.TREES;
        this._tiles[2, 4] = ETileType.TREES;
        this._tiles[2, 3] = ETileType.TREES;

        this._tiles[7, 2] = ETileType.TREES;
        this._tiles[8, 2] = ETileType.TREES;
        this._tiles[8, 3] = ETileType.TREES;

        this._tiles[8, 9] = ETileType.TREES;
    }

    private void GeneratePath() {
        for(int z = 0; z < 10; z++) {
            this._tiles[4, z] = ETileType.DIRT;
            this._tiles[5, z] = ETileType.DIRT;
        }

        for(int x = 6; x < 10; x++) {
            this._tiles[x, 4] = ETileType.DIRT;
            this._tiles[x, 5] = ETileType.DIRT;
        }
    }
    public void UpdateTile() {
        for(int x = 0; x < this._mapSizeX; x++) {
            for (int z = 0; z < this._mapSizeZ; z++) {
                if (this._tileMap[new Vector2Int(x, z)].tileType.tile != ETileType.TREES) {
                    this._tileMap[new Vector2Int(x, z)].isWalkable = true;
                }
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
}
