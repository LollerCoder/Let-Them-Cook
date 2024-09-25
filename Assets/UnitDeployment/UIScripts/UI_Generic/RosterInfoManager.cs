using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RosterInfoManager : MonoBehaviour
{
    //Entities
    private GameObject rosterUnitChar;
    public GameObject rosterUI;
    //For UI
    public TMP_Text unitName;
    public TMP_Text unitSkillName;
    public TMP_Text unitSkillDescription;
    public Image unitImage;

    //Reset Info
    public void Awake()
    {
        this.resetUnitInfo();
    }

    public void resetUnitInfo()
    {
        this.unitImage.gameObject.SetActive(false);
        this.unitName.text = " ";
        this.unitSkillName.text = " ";
        this.unitSkillDescription.text = "Select a character to view their skill!";
    }

    //Display Info
    public void displayUnitInfo(Sprite sprite, string name, string skillName, string skillDescription)
    {
        this.unitImage.gameObject.SetActive(true);
        this.unitImage.sprite = sprite;
        unitName.text = name;
        unitSkillName.text = skillName;
        unitSkillDescription.text = skillDescription;
     }

}
