using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{

    public class EnemyAttackAI
    {
        //private PathFinding _pathFinding;
        //private Range _showRange; 

        public void OnAttack(Unit target, Unit origin)
        {
            Debug.Log(origin.name + " is now attacking " + target.name);
            UnitActionManager.Instance.OnAttack = true;
            UnitActionManager.Instance.numAttack = 0;

            //UnitActions.UnitSelect(target);
            //SkillDatabase.Instance.applySkill(origin.SKILLLIST[0], target, origin);
            UnitActions.ConfirmAttack(target, 0);

            //EventBroadcaster.Instance.PostEvent(EventNames.UnitActionEvents.ON_ENEMY_END_TURN);
        }
    }

}