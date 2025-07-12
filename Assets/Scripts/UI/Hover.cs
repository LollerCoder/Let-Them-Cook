using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    public string message, message2;
   private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(1))
            HoverManager.Instance.SetAndShowToolTip(message, message2);
    }

    public void showSkillName()
    {
        string msg2_buffer = "_";

        if (gameObject.tag == "Skills")
        {
            message = gameObject.GetComponentInChildren<Text>().text;
            msg2_buffer = HoverManager.Instance.getFlavourText(message);
        }   

        if (msg2_buffer.Equals("No skill found") && message2 != null)
        {
            msg2_buffer = message2;
        }

        HoverManager.Instance.SetAndShowToolTip(message, msg2_buffer);
       
    }

    public void hideSkill()
    {
         HoverManager.Instance.HideToolTip();
    }

    private void OnMouseExit()
    {
        HoverManager.Instance.HideToolTip();
    }
}
