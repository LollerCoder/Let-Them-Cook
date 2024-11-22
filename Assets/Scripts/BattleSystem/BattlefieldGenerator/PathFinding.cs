using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathFinding {
    public static EBattleScene BattleScene;

    private static List<Tile> _path = new List<Tile>();
    public static List<Tile> Path { 
        get {  return _path; } 
        set { _path = value; }
    }
    public static List<Tile> AStarPathFinding(Tile start, Tile end, List<Tile> inRangeTiles) {
        List<Tile> tileQueue = new List<Tile>();
        List<Tile> tileVisited = new List<Tile>();

        tileQueue.Add(start);

        while (tileQueue.Count > 0) {
            Tile currentTile = tileQueue.OrderBy(x => x.F).First();

            tileQueue.Remove(currentTile);
            tileVisited.Add(currentTile);

            if (currentTile == end) {
                return GetPath(start, end);
            }

            List<Tile> neighborTiles = GetNeighborTiles(currentTile, inRangeTiles);

            foreach(Tile neighbor in neighborTiles) {
                if(!neighbor.isWalkable || tileVisited.Contains(neighbor)){
                    continue;
                }

                neighbor.cost = GetManhattanDistance(start, neighbor);
                neighbor.heuristic = GetManhattanDistance(end, neighbor);

                neighbor.previousTile = currentTile;

                if(!tileQueue.Contains(neighbor)) {
                    tileQueue.Add(neighbor);
                }
            }
        }

        return new List<Tile>();
    }
    private static List<Tile> GetNeighborTiles(Tile currentTile, List<Tile> inRangeTiles) {
        List<Tile> neighborTiles = new List<Tile>();

        neighborTiles = TileMapManager.Instance.GetNeighborTiles(currentTile, inRangeTiles);
        //neighborTiles = TileMapGenerator.Instance.GetNeighborTiles(currentTile, inRangeTiles);

        return neighborTiles;
    }
    private static List<Tile> GetPath(Tile start, Tile end) {
        List<Tile> finalPath = new List<Tile>();

        Tile currentTile = end;

        while(currentTile != start) {
            finalPath.Add(currentTile);
            currentTile = currentTile.previousTile;
        }

        finalPath.Reverse();

        return finalPath;
    }
    private static int GetManhattanDistance(Tile tile, Tile neighbor) {
        return Mathf.Abs(tile.TilePos.x - neighbor.TilePos.x) + Mathf.Abs(tile.TilePos.y - neighbor.TilePos.y);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

}