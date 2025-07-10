using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitSelectorScript : MonoBehaviour
{
    [Header("Units")]
    [SerializeField]
    private List<Tile> tileSpawn = new List<Tile>();
    private List<Unit> unitSpawn = new List<Unit>();

    GameObject unitPanel;

    public GameObject mapCam;

    private bool bTPressed = false, bGPressed = false, bPPressed = false;

    public Color bTrueColor = new Color(255f, 255f, 38f, 125f);
    public Color bFalseColor  = new Color(255f, 255f, 255f, 225f);



    public Button Tbtn, Gbtn, Pbtn;

    private int lvlNumber;
    void Start()
    {
        /*Pause thegame and choose your units first*/
       // Time.timeScale = 0.0f;

        unitPanel = this.gameObject;

        unitPanel.SetActive(true);

        //what level number is the player at right now?
        lvlNumber = int.Parse(SceneManager.GetActiveScene().name.Split("-")[1]);

        if (lvlNumber >= 2) Gbtn.interactable = true;
        if (lvlNumber >= 3) Pbtn.interactable = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //button for tomato
    public void buttonTReader()
    {
        ColorBlock TbtnClr = Tbtn.colors;

        if (!bTPressed)
        {
            UnitManager.Instance.bTomato = true;
            bTPressed = true;
            Debug.Log("Tomato selected!");

            TbtnClr.normalColor = bTrueColor;
            TbtnClr.highlightedColor = bTrueColor;
            TbtnClr.pressedColor = bTrueColor;
        }

        else if (bTPressed)
        {
            UnitManager.Instance.bTomato = false;
            bTPressed = false;
            Debug.Log("Tomato deselected");

            TbtnClr.normalColor = bFalseColor;
            TbtnClr.highlightedColor = bFalseColor;
            TbtnClr.pressedColor = bFalseColor;
        }

        Tbtn.colors = TbtnClr;

    }

     public void buttonGReader()
    {

        if (!bGPressed)
        {
            UnitManager.Instance.bGarlic = true;
            bGPressed = true;
            Debug.Log("Garlic selected!");
        }

        else if (bGPressed)
        {
            UnitManager.Instance.bGarlic = false;
            bGPressed = false;
            Debug.Log("Garlic deselected");
        }

    }

     public void buttonPReader()
    {

        if (!bPPressed)
        {
            UnitManager.Instance.bPumpkin = true;
            bPPressed = true;
            Debug.Log("Pumpkin selected!");
        }

        else if (bPPressed)
        {
            UnitManager.Instance.bPumpkin = false;
            bPPressed = false;
            Debug.Log("Pumpkin deselected");
        }

    }

    public void startGame()
    {
      //  Time.timeScale = 1.0f;

        unitPanel.SetActive(false);

        mapCam.SetActive(false);

        UnitManager.Instance.manageParty(this.tileSpawn);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.ADDED_UNITS_SELECTED);
    }
}
