using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UnitDeploymentPopUp : MonoBehaviour
{
    //FOR UI
    private RosterInfoManager rosterInfo;
    private Roster_Scriptable roserUnit;
    //Panels
    public GameObject bufferUI;
    public GameObject unitDeploymentUI;
    //Buttons
    public Button chooseUnits;
    public Button startLevel;
    public Button deselect;
    //Images
    public Image unit1Image;
    public Image unit2Image;
    public Image unit3Image;
    //Names
    public TMP_Text unit1Name;
    public TMP_Text unit2Name;
    public TMP_Text unit3Name;

    /*
     * So far doesn't account for the size of the list of deployed units
    */
    //Units
    private DraggableItem unit1;
    private DraggableItem unit2;
    private DraggableItem unit3;

    public void Awake()
    {
        this.disableUnitDepUI();
    }

    //Click Detection
    //Hide and Show Buffer and Unit Deployment
    public void enableUnitDepUI()
    {
        this.bufferUI.SetActive(true);
        this.unitDeploymentUI.SetActive(true);
        this.rosterUnits();
        this.displayUnits();
    }
    public void disableUnitDepUI()
    {
        this.bufferUI.SetActive(false);
        this.unitDeploymentUI.SetActive(false);
        this.clearSelection();
    }

    //List of Units
    private void rosterUnits()
    {
        this.unit1 = this.gameObject.transform.Find("Base/CharacterSelection/Roster/RosterSlot").GetChild(0).gameObject.GetComponent<DraggableItem>();
        this.unit2 = this.gameObject.transform.Find("Base/CharacterSelection/Roster/RosterSlot (1)").GetChild(0).gameObject.GetComponent<DraggableItem>();
        this.unit3 = this.gameObject.transform.Find("Base/CharacterSelection/Roster/RosterSlot (2)").GetChild(0).gameObject.GetComponent<DraggableItem>();
    }
    private void displayUnits()
    {
        //Images
        this.unit1Image.sprite = unit1.rosterUnit.sprite;
        this.unit2Image.sprite = unit2.rosterUnit.sprite;
        this.unit3Image.sprite = unit3.rosterUnit.sprite;
        //Text
        this.unit1Name.text = unit1.rosterUnit.name;
        this.unit2Name.text = unit2.rosterUnit.name;
        this.unit3Name.text = unit3.rosterUnit.name;

    }

    //Clear Selection
    private void clearSelection()
    {
        this.unit1 = null;
        this.unit2 = null;
        this.unit3 = null;
    }

    //Save Data on Confirmation
    public void saveUnits()
    {
        PlayerPrefs.SetString("Unit1", this.unit1Name.text);
        PlayerPrefs.SetString("Unit2", this.unit2Name.text);
        PlayerPrefs.SetString("Unit3", this.unit3Name.text);
    }


}
