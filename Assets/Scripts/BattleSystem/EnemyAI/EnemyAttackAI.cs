using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{

    public class EnemyAttackAI
    {
        private PathFinding _pathFinding;
        private Range _showRange;

        public void OnAttack(Unit target)
        {
            UnitActionManager.Instance.OnAttack = true;
            UnitActionManager.Instance.numAttack = 0;

            UnitActionManager.Instance.UnitSelect(target);
        }
    }

}