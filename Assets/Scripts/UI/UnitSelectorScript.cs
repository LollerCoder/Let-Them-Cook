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

    private Color btnColor = new Color(255f, 255f, 255f, 255f);

    public Button Tbtn, Gbtn, Pbtn;

    private int lvlNumber;
    void Start()
    {
        /*Pause thegame and choose your units first*/
       // Time.timeScale = 0.0f;

        unitPanel = this.gameObject;

        unitPanel.SetActive(true);

        btnColor.a = 225;

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

        if (!bTPressed)
        {
            btnColor.a = 125;
            UnitManager.Instance.bTomato = true;
            bTPressed = true;
           //Tbtn.GetComponent<Button>().
            Debug.Log("Tomato selected!");
        }

        else if (bTPressed)
        {
            btnColor.a = 255;
            UnitManager.Instance.bTomato = false;
            bTPressed = false;
          // Tbtn.GetComponent<Image>().color = btnColor;
            Debug.Log("Tomato deselected");
        }

    }

     public void buttonGReader()
    {

        if (!bGPressed)
        {
            btnColor.a = 125;
            UnitManager.Instance.bGarlic = true;
            bGPressed = true;
            Debug.Log("Garlic selected!");
        }

        else if (bGPressed)
        {
            btnColor.a = 255;
            UnitManager.Instance.bGarlic = false;
            bGPressed = false;
            Debug.Log("Garlic deselected");
        }

    }

     public void buttonPReader()
    {

        if (!bPPressed)
        {
            btnColor.a = 125;
            UnitManager.Instance.bPumpkin = true;
            bPPressed = true;
            Debug.Log("Pumpkin selected!");
        }

        else if (bPPressed)
        {
            btnColor.a = 255;
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
