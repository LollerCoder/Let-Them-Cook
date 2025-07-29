using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//https://www.youtube.com/watch?v=y2N_J391ptg
public class HoverManager : MonoBehaviour
{
    public static HoverManager Instance;

    public TextMeshProUGUI textComponent, subText;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        else Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x - 500.0f, Input.mousePosition.y + 10.0f, Input.mousePosition.z);
    }

    public void SetAndShowToolTip(string message, string message2)
    {
        gameObject.SetActive(true);
        textComponent.text = message;
        subText.text = message2;
    }

    public string getFlavourText(string message)
    {
        string msg;
        switch (message)
        {
            case "Basic Attack":
                msg = "Slash them!";
                break;
            
            case "Circular Cut":
                msg = "Damage veggies in the circle. Freezes unit after usage!";
                break;
            
            case "Photosynthesis":
                msg = "Heal a unit!";
                break;

            case "Shove":
                msg = "Push your enemies away!";
                break;

            case "Yell":
                msg = "Heal a unit and increase their attack!";
                break;
            
            case "Harvest":
                msg = "Damage and heal yourself in battle!";
                break;
            case "Daze":
                msg = "Makes the enemy dizzy but for low damage!";
                break;
            case "Foil Throw":
                msg = "Throws foils to the enemy from afar!";
                break;
            default:
                msg = "No skill found";
                break;
        }

        return msg;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
