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

        if (lvlNumber >= 5) Gbtn.interactable = true;
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
            TbtnClr.selectedColor = bTrueColor;
            Tbtn.colors = TbtnClr;
        }

        else if (bTPressed)
        {
            UnitManager.Instance.bTomato = false;
            bTPressed = false;
            Debug.Log("Tomato deselected");

            TbtnClr.normalColor = bFalseColor;
            TbtnClr.highlightedColor = bFalseColor;
            TbtnClr.pressedColor = bFalseColor;
            TbtnClr.selectedColor = bFalseColor;
            Tbtn.colors = TbtnClr;
        }

        

    }

     public void buttonGReader()
    {
        ColorBlock GbtnClr = Gbtn.colors;
        if (!bGPressed)
        {
            UnitManager.Instance.bGarlic = true;
            bGPressed = true;
            Debug.Log("Garlic selected!");

            GbtnClr.normalColor = bTrueColor;
            GbtnClr.highlightedColor = bTrueColor;
            GbtnClr.pressedColor = bTrueColor;
            GbtnClr.selectedColor = bTrueColor;
            Gbtn.colors = GbtnClr;
        }

        else if (bGPressed)
        {
            UnitManager.Instance.bGarlic = false;
            bGPressed = false;
            Debug.Log("Garlic deselected");
            
            GbtnClr.normalColor = bFalseColor;
            GbtnClr.highlightedColor = bFalseColor;
            GbtnClr.pressedColor = bFalseColor;
            GbtnClr.selectedColor = bFalseColor;
            Gbtn.colors = GbtnClr;
        }

    }

     public void buttonPReader()
    {
        ColorBlock PbtnClr = Pbtn.colors;

        if (!bPPressed)
        {
            UnitManager.Instance.bPumpkin = true;
            bPPressed = true;
            Debug.Log("Pumpkin selected!");

            PbtnClr.normalColor = bTrueColor;
            PbtnClr.highlightedColor = bTrueColor;
            PbtnClr.pressedColor = bTrueColor;
            PbtnClr.selectedColor = bTrueColor;
            Pbtn.colors = PbtnClr;
        }

        else if (bPPressed)
        {
            UnitManager.Instance.bPumpkin = false;
            bPPressed = false;
            Debug.Log("Pumpkin deselected");

            PbtnClr.normalColor = bFalseColor;
            PbtnClr.highlightedColor = bFalseColor;
            PbtnClr.pressedColor = bFalseColor;
            PbtnClr.selectedColor = bFalseColor;
            Pbtn.colors = PbtnClr;
            
            
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
