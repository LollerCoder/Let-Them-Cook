using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillAllObjective : MonoBehaviour , IObjective
{
    [SerializeField] Toggle toggle;
    [SerializeField] private string toggleMessage;
    private Text toggleText;
    private int _EnemyNums;

    private bool cleared = false;

    public bool Cleared { get { return cleared; } }

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, this.LateStart);
        EventBroadcaster.Instance.AddObserver(EventNames.Enemy_Events.ON_ENEMY_DEFEATED, this.DecrementEnemyCount);

        this.toggle.interactable = false;
        this.toggle.isOn = false;
        this.toggleText = toggle.GetComponentInChildren<Text>();
        this.toggleText.text = toggleMessage;
    }

    public void LateStart()
    {
        this._EnemyNums = UnitActionManager.Instance.UnitList.FindAll(u => u.Type == EUnitType.Enemy).Count;
    }

    public void DecrementEnemyCount()
    {
        this._EnemyNums--;
        this.clearCondition();
        
    }
    public void clearCondition()
    {
       

        if (this._EnemyNums <= 0)
        {
            
            toggle.isOn = true;

            cleared = true;
        }

    }
    public void onConditionClear()
    {

    }


    public bool getIfCleared()
    {
        return cleared;
    }
}
