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
        if (Instance == null)
        {
            Instance = null;
        }
        else
        {
            Destroy(this);
        }
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
                msg = "Slash your enemies!";
                break;
            
            case "Circular Cut":
                msg = "Damages surrounding foe, cannot move to other tiles after usage";
                break;
            
            case "Photosynthesis":
                msg = "Slash your enemies!";
                break;

            case "Shove":
                msg = "Push your enemies away!";
                break;

             case "Yell":
                msg = "Heal one of your allies";
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
