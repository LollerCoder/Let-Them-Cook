using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    [SerializeField]
    EnemyController enemyController;

    [SerializeField]
    private int numAllies = 0;
    [SerializeField]
    private int currAllies = 0;

    [SerializeField]
    private List<DroppedVegetable> inInventory = new List<DroppedVegetable>();

    [SerializeField]
    private int numEnemies = 0;
    [SerializeField]
    private int currEnemies = 0;

    [SerializeField]
    private bool GameEnd = false;

    private void NextTurn() {
        Unit unit = UnitActionManager.Instance.GetFirstUnit();
        if (unit != null) {
            unit.Tile.ApplyEffect(unit);
        }

        BattleUI.Instance.OnEndTurn(this.GameEnd);
    }

    private void EndCondition(Parameters param) {
        Unit unit = param.GetUnitExtra("UNIT");

        if (unit.Type == EUnitType.Enemy) { // ally win
            this.currEnemies--;
            if (this.currEnemies == 0) {
                BattleUI.Instance.EndScreen(EUnitType.Ally);
                this.GameEnd = true;
                this.CollectRemainingVeg();

                /*Set the next level as unlocked*/
                LevelManager.instance.updateMap(SceneManager.GetActiveScene().name);
                return;
            }
        }

        if (unit.Type == EUnitType.Ally) {  // enemy win
            this.currAllies--;
            if (this.currAllies == 0) {
                BattleUI.Instance.EndScreen(EUnitType.Enemy);
                this.GameEnd = true;
                this.CollectRemainingVeg();
                return;
            }
        }
    }

    private void EndLevel(Parameters param)
    {
        if (!(param.GetBoolExtra("Level_Complete", false))) return;

        BattleUI.Instance.EndScreen(param);

        this.GameEnd = true;
        this.CollectRemainingVeg();

        /*Set the next level as unlocked*/
        LevelManager.instance.updateMap(SceneManager.GetActiveScene().name);
    }

    private void SetUnitNums() {
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
        //RewardSystem.Instance.gainRewards(2, this.numEnemies, this.numAllies, this.currAllies, UnitActionManager.Instance.UnitOrder);
    }

    private void UpdateInventory(Parameters param) {
        DroppedVegetable veg = param.GetVegExtra("VEG");

        this.inInventory.Add(veg);  
    }
    private void CollectRemainingVeg() {
        foreach (DroppedVegetable veg in DroppedVegetableManager.Instance.VegInField) {
            if (!this.inInventory.Contains(veg)) {
                this.inInventory.Add(veg);
            }
        }
    }

    public void Start() {

        
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.HANDLE_GAIN_REWARDS, this.HandleUnitLevelUp);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CHECK_END_CONDITION, this.EndLevel);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, this.SetUnitNums);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.NEXT_TURN, this.NextTurn);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.UPDATE_INVENTORY, this.UpdateInventory);

        this.StartCoroutine(this.SetUpBattle());
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveAllObservers();
    }

    IEnumerator SetUpBattle() {
        //this.enemyController.SpawnEnemy();

        //yield return this.StartCoroutine(this.enemyController.SpawnEnemy()); // waits for the spawn enemy to finish spawning before starting the battle
        yield return null; // waits for the next frame, basically make every unit load first
        UnitActionManager.Instance.OnStart();
    }

}
