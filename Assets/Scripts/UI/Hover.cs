using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    public string message;

    public void OnMouseEnter()
    {
        if (gameObject.tag == "Skills") message = gameObject.GetComponentInChildren<Text>().text;
        HoverManager.Instance.SetAndShowToolTip(message);

    }

    public void OnMouseExit()
    {
        HoverManager.Instance.HideToolTip();
    }
}
