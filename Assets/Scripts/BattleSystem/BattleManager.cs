using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    private int numAllies = 0;
    private int numEnemies = 0;

    public bool GameEnd = false;

    public void EndCondition(Unit unit) {
        if (unit.Type == EUnitType.Enemy) {
            this.numEnemies--;
            if (this.numEnemies == 0) {
                BattleUI.Instance.EndScreen(EUnitType.Ally);
                this.GameEnd = true;
                return;
            }
        }

        if (unit.Type == EUnitType.Ally) {
            this.numAllies--;
            if (this.numAllies == 0) {
                BattleUI.Instance.EndScreen(EUnitType.Enemy);
                this.GameEnd = true;
                return;
            }
        }
    }

    public void SetNums() {
        foreach(Unit unit in UnitActionManager.Instance.UnitList) {
            if(unit.Type == EUnitType.Ally) {
                this.numAllies++;
            }          
            
            if(unit.Type == EUnitType.Enemy) {
                this.numEnemies++;
            }
        }
    }

    public void HandleUnitLevelUp() {

    }

    public void UpdateInventory() {

    }

}
