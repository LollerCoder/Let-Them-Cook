/*
 * September 18, 2024
 *
 * Temporary solution until more efficient organization method is implemented
 * SHOULD work semi-well
 * Auto-assignment of scriptable items to prefabs should be considered
 * ...as should the automatic sorting of units
 * ...and auto-filling of units as more types of vegetables are unlocked
 * how should the unit deployment system work anyway
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    //FOR SKILL DESCRIPTION
    //Scriptable Item
    public Roster_Scriptable rosterUnit;
    //Image
    public Image image;
    //public Sprite sprite;
    //Utility Attributes
    private RosterInfoManager rosterInfo;
    [HideInInspector]public Transform parentAfterDrag;
    //For UI
    public TMP_Text unitName;
    public TMP_Text unitSkillName;
    public TMP_Text unitSkillDescription;
    public Image unitImage;

    //INTERFACE IMPLEMENTATION
    //Click Object
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        //rosterInfo.resetUnitInfo();
        //rosterInfo.displayUnitInfo(sprite, rosterUnit.name, rosterUnit.skillName, rosterUnit.skillDescription);
        this.resetUnitInfo();
        this.displayUnitInfo();
    }
    //Drag Object
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    //Reset Info
    private void resetUnitInfo()
    {
        this.unitImage.gameObject.SetActive(false);
        this.unitName.text = " ";
        this.unitSkillName.text = " ";
        this.unitSkillDescription.text = "Click on a character to view their skill!";
    }
    //Display Info
    private void displayUnitInfo()
    {
        this.unitImage.gameObject.SetActive(true);
        this.unitImage.sprite = this.rosterUnit.sprite;
        unitName.text = this.rosterUnit.name;
        unitSkillName.text = this.rosterUnit.skillName;
        unitSkillDescription.text = this.rosterUnit.skillDescription;
    }

}
