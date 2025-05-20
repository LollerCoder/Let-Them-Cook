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

            if (path.Count <= 0) return null;

            //get actual path relative to unit's speed
            List<Tile> actual_path = new List<Tile>();

            for (int i = 0; i < _currentUnit.Speed; i++)
            {
                actual_path.Add(path[i]);
            }

            //Debug.Log("Unit speed: " + _currentUnit.Speed);
            if (actual_path.Count > 0)
            {
                UnitActionManager.Instance.hadMoved = true;
                _currentUnit.OnMovement(true);
            }

            //making the sprite face the correct direction
            SpriteRenderer cuSR =  _currentUnit.GetComponent<SpriteRenderer>();

            if (_currentUnit.transform.position.x >= targetUnit.transform.position.x)
            {
                Debug.Log("looking left");
                cuSR.flipX = true;
            }
            else
            {
                Debug.Log("looking right");
                cuSR.flipX = false;
            }

            //Debug.Log($"Current Unit: {_currentUnit} Target unit: {targetUnit} = Path length: {actual_path.Count}");

            return actual_path;
        }
    }

}