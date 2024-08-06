using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Range {
    public EBattleScene BattleScene;
    public List<Tile> GetTilesInMovement(Tile startingTile, int range) {
        List<Tile> inRangeTiles = new List<Tile>();

        int stepCount = 0;

        inRangeTiles.Add(startingTile);

        List<Tile> previousTiles = new List<Tile>();
        previousTiles.Add(startingTile);

        while(stepCount < range) {
            List<Tile> neighborTiles = new List<Tile>();

            foreach(Tile tile in previousTiles) {
                if (!tile.isWalkable) { // do not add as highlighted area in range
                    continue;
                }
                neighborTiles.AddRange(this.GetNeighborTiles(tile));
            }

            inRangeTiles.AddRange(neighborTiles);
            previousTiles = neighborTiles.Distinct().ToList();
            stepCount++;
        }
        return inRangeTiles.Distinct().ToList();
    }

    public List<Tile> GetTilesInAttackRange(Tile startingTile, int range) {
        List<Tile> inRangeTiles = new List<Tile>();

        int stepCount = 0;

        inRangeTiles.Add(startingTile);
        
        List<Tile> previousTiles = new List<Tile>();
        previousTiles.Add(startingTile);
        
        while (stepCount < range) {
            List<Tile> neighborTiles = new List<Tile>();

            foreach (Tile tile in previousTiles) {
                if (!tile.isWalkable) {
                    continue;
                }
                neighborTiles.AddRange(this.GetNeighborTiles(tile));
            }
            previousTiles = neighborTiles.Distinct().ToList();
            stepCount++;
        }
        // only takes the last tiles in range
        inRangeTiles = previousTiles;
        inRangeTiles.Remove(startingTile);
        return inRangeTiles.Distinct().ToList();
    }

    public List<Tile> GetTilesInAttackMelee(Tile startingTile, int range) {
        List<Tile> inRangeTiles = new List<Tile>();

        int stepCount = 0;

        inRangeTiles.Add(startingTile);

        List<Tile> previousTiles = new List<Tile>();
        previousTiles.Add(startingTile);

        while (stepCount < range) {
            List<Tile> neighborTiles = new List<Tile>();

            foreach (Tile tile in previousTiles) {
                if (!tile.isWalkable) {
                    continue;
                }
                neighborTiles.AddRange(this.GetNeighborTiles(tile));
            }
            inRangeTiles.AddRange(neighborTiles);
            previousTiles = neighborTiles.Distinct().ToList();
            stepCount++;

        }
       
        inRangeTiles.Remove(startingTile);
        return inRangeTiles.Distinct().ToList();
    }
    private List<Tile>GetNeighborTiles(Tile tile) {
        List<Tile> neighborTiles = new List<Tile>();

        neighborTiles = TileMapGenerator.Instance.GetNeighborTiles(tile, new List<Tile>());

        return neighborTiles;
    }
        
}
