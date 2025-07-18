using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyAI
{
    public class EnemyMainAI
    {
        private List<Unit> _Units = new List<Unit>();
        private Unit _CurrentEnemyUnit;

        //private List<Unit> _EnemyUnits = new List<Unit>();
        private List<Unit> _AllyUnits = new List<Unit>();

        //private PathFinding _pathFinding;
        //private Range _showRange;

        //AI components
        private EnemyMoveAI _moveAI;
        private EnemyAttackAI _attackAI;

        public EnemyMainAI(List<Unit> units)
        {
            //this._pathFinding = new PathFinding();
            //this._showRange = new Range();

            this._moveAI = new EnemyMoveAI();
            this._attackAI = new EnemyAttackAI();

            this._Units = units;

        }

        public void UpdateAllyUnits(List<Unit> units)
        {
            this._AllyUnits = units;
        }

        private Unit GetClosestAllyUnit()
        {
            Unit targetUnit = null;
            float prevDist = 9999.99f;

            foreach (Unit ally in this._AllyUnits)
            {
                /*
                 * Check if ally is still alive
                 */

                float currDist = PathFinding.AStarPathFinding(this._CurrentEnemyUnit.Tile,
                             ally.Tile,
                             Range.GetTilesInMovement(this._CurrentEnemyUnit.Tile,
                                                             100)
                             ).Count;
                if (currDist < prevDist &&
                    ally.HP > 0)
                {
                    //Debug.Log("Distance: " + currDist);
                    targetUnit = ally;
                    prevDist = currDist;
                }
            }
            
            return targetUnit;
        }

        public void TakeAction()
        {

            //Debug.Log("Enemy Agent Attacking!");
            this._attackAI.OnAttack(this.GetClosestAllyUnit(), this._CurrentEnemyUnit);

            //int action = UnityEngine.Random.Range(1, 3);
            //int action = 1;
            //switch (action)
            //{
            //    case 1:
            //        {
            //            Debug.Log("Enemy Agent Attacking!");
            //            this._attackAI.OnAttack(this.GetClosestAllyUnit(), this._CurrentEnemyUnit);
            //            break;
            //        }
            //    case 2:
            //        {
            //            Debug.Log("Enemy Agent Healing!");
            //            this._CurrentEnemyUnit.Heal();
            //            break;
            //        }
            //    default: break;
            //}
        }

        public List<Tile> TakeTurn(Unit currentEnemyAgent)
        {
            List<Tile> path = new List<Tile>();
            this._CurrentEnemyUnit = currentEnemyAgent;

            Debug.Log("enter enemyunityaction");

            List<Tile> inRangeTiles = Range.GetTilesInAttackMelee(
                this._CurrentEnemyUnit.Tile, 
                this._CurrentEnemyUnit.BasicRange
                );

            Unit target = this.GetClosestAllyUnit();
            if (target == null)
            {
                Debug.LogError("TARGET NOT FOUND!");
                return path;
            }

            //if dazed or stunned
            if (this._CurrentEnemyUnit.GetEffect("Dizzy") != null ||
                this._CurrentEnemyUnit.GetEffect("Asleep") != null)
            {
                Debug.Log("Skipping turn!");
                EventBroadcaster.Instance.PostEvent(EventNames.UnitActionEvents.ON_ENEMY_END_TURN);
                return path;
            }

            if (inRangeTiles.Contains(target.Tile))
            {
                //Take action
                Debug.Log("Taking action!");
                this.TakeAction();
            }

            if (this._CurrentEnemyUnit.GetEffect("Rooted") != null)
            {
                EventBroadcaster.Instance.PostEvent(EventNames.UnitActionEvents.ON_ENEMY_END_TURN);
                return path;
            }

            else
            {
                //Move closer
                Debug.Log("Moving!");
                path = this._moveAI.MoveEnemy(this._CurrentEnemyUnit, target);
            }

            return path;
        }
    }

}