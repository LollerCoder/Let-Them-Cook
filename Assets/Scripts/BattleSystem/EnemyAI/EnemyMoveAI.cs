using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{

    public class EnemyMoveAI
    {

        //private PathFinding _pathFinding;
        //private Range _showRange;

        public EnemyMoveAI()
        {
            ///this._pathFinding = new PathFinding();
            //this._showRange = new Range();
        }

        public List<Tile> MoveEnemy(Unit _currentUnit, Unit targetUnit)
        {
            Debug.Log("Enemy Moving!");

            UnitActionManager.Instance.OnMove = true;


            List<Tile> path = PathFinding.AStarPathFinding(_currentUnit.Tile,
                             targetUnit.Tile,
                             Range.GetTilesInMovement(_currentUnit.Tile,
                                                             100)
                             );
            path.RemoveAt(path.Count - 1);
            //Debug.Log("Unit speed: " + _currentUnit.Speed);
            if (path.Count > 0)
            {
                UnitActionManager.Instance.hadMoved = true;
                _currentUnit.OnMovement(true);
            }
            Debug.Log($"Current Unit: {_currentUnit} Target unit: {targetUnit} = Path length: {path.Count}");

            return path;
        }
    }

}