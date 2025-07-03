using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UnitSelectorScript : MonoBehaviour
{
    [Header("Units")]
    [SerializeField]
    private List<Tile> tileSpawn = new List<Tile>();
    private List<Unit> unitSpawn = new List<Unit>();

    GameObject unitPanel;

    private bool bPressed = false;

    public Button Tbtn;
    void Start()
    {
        /*Pause thegame and choose your units first*/
        Time.timeScale = 0.0f;

        unitPanel = this.gameObject;

        unitPanel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //button for tomato
    public void buttonReader()
    {
        UnitManager.Instance.bTomato = true;
        //this.unitSpawn.Add(new Tomato);
        Debug.Log("Tomato selected!");

        // if (!bPressed)
        // {
        //     bPressed = true;


        // } else if (bPressed) UnitManager.Instance.UnitCounter -= 1;

    }

    public void startGame()
    {
        Time.timeScale = 1.0f;

        unitPanel.SetActive(false);

        UnitManager.Instance.manageParty(this.tileSpawn);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.ADDED_UNITS_SELECTED);
    }
}
