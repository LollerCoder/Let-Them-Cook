using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding {
    public EBattleScene BattleScene;
    public List<Tile> AStarPathFinding(Tile start, Tile end, List<Tile> inRangeTiles) {
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

                neighbor.cost = this.GetManhattanDistance(start, neighbor);
                neighbor.heuristic = this.GetManhattanDistance(end, neighbor);

                neighbor.previousTile = currentTile;

                if(!tileQueue.Contains(neighbor)) {
                    tileQueue.Add(neighbor);
                }
            }
        }

        return new List<Tile>();
    }
    private List<Tile> GetNeighborTiles(Tile currentTile, List<Tile> inRangeTiles) {
        List<Tile> neighborTiles = new List<Tile>();

        neighborTiles = TileMapGenerator.Instance.GetNeighborTiles(currentTile, inRangeTiles);

        return neighborTiles;
    }
    private List<Tile> GetPath(Tile start, Tile end) {
        List<Tile> finalPath = new List<Tile>();

        Tile currentTile = end;

        while(currentTile != start) {
            finalPath.Add(currentTile);
            currentTile = currentTile.previousTile;
        }

        finalPath.Reverse();

        return finalPath;
    }
    private int GetManhattanDistance(Tile tile, Tile neighbor) {
        return Mathf.Abs(tile.TilePos.x - neighbor.TilePos.x) + Mathf.Abs(tile.TilePos.y - neighbor.TilePos.y);
    }
}