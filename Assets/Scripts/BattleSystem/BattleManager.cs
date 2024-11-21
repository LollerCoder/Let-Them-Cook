using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    [SerializeField]
    EnemyController enemyController;

    [SerializeField]
    private int numAllies = 0;
    [SerializeField]
    private int currAllies = 0;


    [SerializeField]
    private int numEnemies = 0;
    [SerializeField]
    private int currEnemies = 0;

    [SerializeField]
    private bool GameEnd = false;

    private void NextTurn() {
        BattleUI.Instance.OnEndTurn(this.GameEnd);
    }

    private void EndCondition(Parameters param) {
        Unit unit = param.GetUnitExtra("UNIT");

        if (unit.Type == EUnitType.Enemy) { // ally win
            this.currEnemies--;
            if (this.currEnemies == 0) {
                BattleUI.Instance.EndScreen(EUnitType.Ally);
                this.GameEnd = true;
                return;
            }
        }

        if (unit.Type == EUnitType.Ally) {  // enemy win
            this.currAllies--;
            if (this.currAllies == 0) {
                BattleUI.Instance.EndScreen(EUnitType.Enemy);
                this.GameEnd = true;
                return;
            }
        }
    }

    private void SetNums() {
        foreach(Unit unit in UnitActionManager.Instance.UnitList) {
            if(unit.Type == EUnitType.Ally) {
                this.numAllies++;
            }          
            
            if(unit.Type == EUnitType.Enemy) {
                this.numEnemies++;
            }
        }

        this.currAllies = this.numAllies;
        this.currEnemies = this.numEnemies;
    }

    private void HandleUnitLevelUp() {
        RewardSystem.Instance.gainRewards(2, this.numEnemies, this.numAllies, this.currAllies, UnitActionManager.Instance.UnitOrder);
    }

    private void UpdateInventory() {

    }

    public void Start() {
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.HANDLE_GAIN_REWARDS, this.HandleUnitLevelUp);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CHECK_END_CONDITION, this.EndCondition);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, this.SetNums);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.NEXT_TURN, this.NextTurn);

        this.StartCoroutine(this.SetUpBattle());
    }

    private IEnumerator SetUpBattle() {
        this.enemyController.SpawnEnemy();

        yield return this.StartCoroutine(this.enemyController.SpawnEnemy()); // waits for the spawn enemy to finish spawning before starting the battle

        UnitActionManager.Instance.OnStart();
    }
}
