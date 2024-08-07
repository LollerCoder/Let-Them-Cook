using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotifIngredientObtained : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    public void SetUpNotif(string ingredient)
    {
        text.text = "Obtained " + ingredient.ToString() + "!";
        Debug.Log(text.text);
    }
}
