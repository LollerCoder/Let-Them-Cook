using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Range {
    public static EBattleScene BattleScene;

    private static List<Tile> _inRangeTiles = new List<Tile>();
    public static List<Tile> InRangeTiles {
        get { return _inRangeTiles; }
    }

    public static List<Tile> GetTilesInMovement(Tile startingTile, float range) {
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
                neighborTiles.AddRange(GetNeighborTiles(tile));
            }

            inRangeTiles.AddRange(neighborTiles);
            previousTiles = neighborTiles.Distinct().ToList();
            stepCount++;
        }
        return inRangeTiles.Distinct().ToList();
    }

    public static List<Tile> GetTilesInAttackRange(Tile startingTile, int range) {
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
                neighborTiles.AddRange(GetNeighborTiles(tile));
            }
            previousTiles = neighborTiles.Distinct().ToList();
            stepCount++;
        }
        // only takes the last tiles in range
        inRangeTiles = previousTiles;
        inRangeTiles.Remove(startingTile);
        return inRangeTiles.Distinct().ToList();
    }
    public static List<Tile> GetTilesInAttackMelee(Tile startingTile, int range) {
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
                neighborTiles.AddRange(GetNeighborTiles(tile));
            }
            inRangeTiles.AddRange(neighborTiles);
            previousTiles = neighborTiles.Distinct().ToList();
            stepCount++;

        }
       
        inRangeTiles.Remove(startingTile);
        return inRangeTiles.Distinct().ToList();
    }
    private static List<Tile>GetNeighborTiles(Tile tile) {
        List<Tile> neighborTiles = new List<Tile>();

        neighborTiles = TileMapGenerator.Instance.GetNeighborTiles(tile, new List<Tile>());

        return neighborTiles;
    }


    ///////////////////////////////////////////////////////////////////////////////////////
    
    private static void GetTiles(string Type, Unit unit, float range) {
        switch (Type) {
            case "Attack":
                _inRangeTiles = GetTilesInAttackMelee(unit.Tile, (int)range);

                break;
            case "Move":
                _inRangeTiles = GetTilesInMovement(unit.Tile, range);

                foreach (Tile tile in _inRangeTiles) {
                    tile.HighlightWalkableTile();
                }
                break;
            case "Heal":
                _inRangeTiles = GetTilesInAttackRange(unit.Tile, (int)range);

                foreach (Tile tile in _inRangeTiles) {
                    tile.HighlightEatableTile();
                }
                break;
            default: break;
        }
    }
    public static void GetRange(Unit unit, float range, string Type) {
        UnHighlightTiles();

        GetTiles(Type, unit, range);
    }
    public static void UnHighlightTiles() { 
        foreach (Tile tile in _inRangeTiles) {
            tile.UnHighlightTile();
        }
    }

}
