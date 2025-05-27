using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathFinding {
      private static List<Tile> _path = new List<Tile>();
    public static List<Tile> Path { 
        get {  return _path; } 
        set { _path = value; }
    }

    public static List<Tile> AStarPathFinding(Tile start, Tile end, List<Tile> inRangeTiles) {
        List<Tile> tileQueue = new List<Tile>();
        List<Tile> tileVisited = new List<Tile>();
        Dictionary<Tile, int> costSoFar = new Dictionary<Tile, int>();

        costSoFar[start] = 0; // stores the added cost for each tile (current tile to the next tile)
        start.heuristic = GetManhattanDistance(start, end);
        tileQueue.Add(start);

        while (tileQueue.Count > 0) {
            // get the tile with the lowest cost to travel to first
            Tile currentTile = tileQueue.OrderBy(x => costSoFar[x] + x.heuristic).First();

            tileQueue.Remove(currentTile);
            tileVisited.Add(currentTile);

            if (currentTile == end) {
                return GetPath(start, end);
            }

            List<Tile> neighborTiles = GetNeighborTiles(currentTile, inRangeTiles);

            foreach (Tile neighbor in neighborTiles) {
                if (!neighbor.isWalkable || tileVisited.Contains(neighbor)) {
                    continue;
                }

                int cost = 0;

                if (currentTile.bCost == neighbor.bCost) {
                    cost = 1;
                }
                else if (Mathf.Abs(neighbor.bCost - currentTile.bCost) > 1) {
                    continue;
                }
                else if (currentTile.bCost < neighbor.bCost) {
                    cost = neighbor.bCost;
                }
                else {
                    cost = 1;
                }

                int tempCost = costSoFar[currentTile] + cost;

                if (!costSoFar.ContainsKey(neighbor) || tempCost < costSoFar[neighbor]) {
                    costSoFar[neighbor] = tempCost; // store the added cost to the neighbor tile
                    neighbor.heuristic = GetManhattanDistance(end, neighbor);
                    neighbor.previousTile = currentTile;

                    if (!tileQueue.Contains(neighbor)) {
                        tileQueue.Add(neighbor);
                    }
                }
            }
        }

        return new List<Tile>();
    }
    private static List<Tile> GetNeighborTiles(Tile currentTile, List<Tile> inRangeTiles) {
        List<Tile> neighborTiles = new List<Tile>();

        neighborTiles = TileMapManager.Instance.GetNeighborTiles(currentTile, inRangeTiles);

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