using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    public string message, message2;

   private void OnMouseEnter()
    {  
        HoverManager.Instance.SetAndShowToolTip(message, message2);
    }

    public void showSkillName()
    {
         if (gameObject.tag == "Skills")
        { 
            message = gameObject.GetComponentInChildren<Text>().text;
            message2 = HoverManager.Instance.getFlavourText(message);

        }   
        HoverManager.Instance.SetAndShowToolTip(message, message2);
    }

    private void OnMouseExit()
    {
        HoverManager.Instance.HideToolTip();
    }
}
