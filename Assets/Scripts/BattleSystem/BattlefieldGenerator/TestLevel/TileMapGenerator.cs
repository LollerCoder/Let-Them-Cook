using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;

//using Unity.VisualScripting;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour {

    public static TileMapGenerator Instance = null;

    [SerializeField]
    private GameObject[] _tilePrefabs;

    [SerializeField]
    private Transform _TileField;

    private ETileType[,] _tiles;

    private Dictionary<ETileType, TileType> _tileType = new Dictionary<ETileType, TileType>();

    private Dictionary<Vector2Int, Tile> _tileMap = new Dictionary<Vector2Int, Tile>();
    public Dictionary<Vector2Int, Tile> TileMap {
        get { return _tileMap; }
    }

    private int _mapSizeX = 20;
    private int _mapSizeZ = 20;

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
                    tile.isWalkable = false;
                }

                this._tileMap.Add(tile.TilePos, tile);
            }
        }
    }
    public void TileGenerator() {
        this._tiles = new ETileType[this._mapSizeX, this._mapSizeZ];

        this.GenerateTiles();
    }
    public void GenerateTileTypes() {
        // assign each tile with their unique name, prefab, and type
        // then add it to the list of tile types
        TileType tiletype = new TileType();
        tiletype.name = "BLOCK";
        tiletype.tilePrefab = this._tilePrefabs[0];
        tiletype.tile = ETileType.BLOCK;
        this._tileType.Add(ETileType.BLOCK, tiletype);

        tiletype = new TileType();
        tiletype.name = "BLACK";
        tiletype.tilePrefab = this._tilePrefabs[1];
        tiletype.tile = ETileType.BLACK;
        this._tileType.Add(ETileType.BLACK, tiletype);

        tiletype = new TileType();
        tiletype.name = "WHITE";
        tiletype.tilePrefab = this._tilePrefabs[2];
        tiletype.tile = ETileType.WHITE;
        this._tileType.Add(ETileType.WHITE, tiletype);

        /*Special TIle Types*/
        tiletype = new TileType();
        tiletype.name = "BUFF";
        tiletype.tilePrefab = this._tilePrefabs[3];
        tiletype.tile = ETileType.BUFF;
        this._tileType.Add(ETileType.BUFF, tiletype);


        tiletype = new TileType();
        tiletype.name = "DEBUFF";
        tiletype.tilePrefab = this._tilePrefabs[4];
        tiletype.tile = ETileType.DEBUFF;
        this._tileType.Add(ETileType.DEBUFF, tiletype);


        tiletype = new TileType();
        tiletype.name = "RANDOM";
        tiletype.tilePrefab = this._tilePrefabs[5];
        tiletype.tile = ETileType.RANDOM;
        this._tileType.Add(ETileType.RANDOM, tiletype);


        tiletype = new TileType();
        tiletype.name = "HAZARD";
        tiletype.tilePrefab = this._tilePrefabs[6];
        tiletype.tile = ETileType.HAZARD;
        this._tileType.Add(ETileType.HAZARD, tiletype);
        
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
    private void GenerateTiles() {
        for (int z = 0; z < this._mapSizeZ; z++) {
            for (int x = 0; x < this._mapSizeX; x++) {

                int currTile = UnityEngine.Random.Range(1,41);

                switch(currTile)
                {
                    case 10:  this._tiles[x, z] = ETileType.BUFF;
                    break;

                    case 20:  this._tiles[x, z] = ETileType.DEBUFF;
                    break;

                    case 30:  this._tiles[x, z] = ETileType.RANDOM;
                    break;

                    case 40:  this._tiles[x, z] = ETileType.HAZARD;
                    break;

                    default:
                        if ((x + z) % 2 == 0) {
                            //this._tiles[x, z] = ETileType.BLACK;
                            this._tiles[x, z] = ETileType.BLOCK;
                        }
                        else {
                            //this._tiles[x, z] = ETileType.WHITE;
                            this._tiles[x, z] = ETileType.BLOCK;
                        }
                        //this._tiles[x, z] = ETileType.BLOCK;
                        break;
                }
            }
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
