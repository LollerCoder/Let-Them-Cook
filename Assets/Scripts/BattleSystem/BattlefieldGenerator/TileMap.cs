using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface TileMap {
    public void TilePrefabGenerator();
    public void TileMapGenerator();
    public void GenerateTileTypes();
    public List<Tile> GetNeighborTiles(Tile currentTile, List<Tile> inRangeTiles);
}
