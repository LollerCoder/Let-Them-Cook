using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Range {
    private static List<Tile> _inRangeTiles = new List<Tile>();
    public static List<Tile> InRangeTiles {
        get { return _inRangeTiles; }
    }

    public static List<Tile> GetTilesInMovement(Tile startingTile, float range) {
        List<Tile> inRangeTiles = new List<Tile>();
        Dictionary<Tile, float> costSoFar = new Dictionary<Tile, float>();
        List<Tile> tileQueue = new List<Tile>();

        costSoFar[startingTile] = 0f;
        tileQueue.Add(startingTile);

        while (tileQueue.Count > 0) {
            // get the tile with the lowest cost to travel to first
            Tile current = tileQueue.OrderBy(x => costSoFar[x]).First();
            tileQueue.Remove(current);

            inRangeTiles.Add(current);

            foreach (Tile neighbor in GetNeighborTiles(current)) {
                if (!neighbor.isWalkable)
                    continue;

                int cost = 0;

                if (current.bCost == neighbor.bCost) {
                    cost = 1;
                }
                else if (Mathf.Abs(neighbor.bCost - current.bCost) > 1) {
                    continue;
                }
                else if (current.bCost < neighbor.bCost) {
                    cost = 1 + (neighbor.bCost - current.bCost); // ensures you can travel to a neighbor tile 1 cost higher than the current tile
                }
                else {
                    cost = 1;
                }

                float tempCost = costSoFar[current] + cost;

                if (tempCost <= range && (!costSoFar.ContainsKey(neighbor) || tempCost < costSoFar[neighbor])) {
                    costSoFar[neighbor] = tempCost; // store the added cost to the neighbor tile

                    if (!tileQueue.Contains(neighbor))
                        tileQueue.Add(neighbor);
                }
            }
        }

        return inRangeTiles;
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

        neighborTiles = TileMapManager.Instance.GetNeighborTiles(tile, new List<Tile>());
        //neighborTiles = TileMapGenerator.Instance.GetNeighborTiles(tile, new List<Tile>());

        return neighborTiles;
    }


    ///////////////////////////////////////////////////////////////////////////////////////
    
    private static void GetTiles(RangeType Type, Unit unit, float range) {
        switch (Type) {
            case RangeType.ATTACK: // temporary
                _inRangeTiles = GetTilesInAttackMelee(unit.Tile, (int)range);

                break;
            case RangeType.WALK:
                _inRangeTiles = GetTilesInMovement(unit.Tile, range);

                //foreach (Tile tile in _inRangeTiles) {
                //    tile.HighlightWalkableTile();
                //}

                InRangeTiles[0].HighlightCurrentTile();
                for (int i = 1; i < InRangeTiles.Count; i++) {
                    InRangeTiles[i].HighlightWalkableTile();
                }

                break;
            case RangeType.HEAL:
                _inRangeTiles = GetTilesInAttackMelee(unit.Tile, (int)range);
                break;
            default: break;
        }
    }

    public static void GetRange(Unit unit, float range, RangeType Type) {
        UnHighlightTiles();
   
        GetTiles(Type, unit, range);

    }       
    public static void UnHighlightTiles() {

        foreach (Tile tile in _inRangeTiles) {
            tile.UnHighlightTile();
        }
    }

}
