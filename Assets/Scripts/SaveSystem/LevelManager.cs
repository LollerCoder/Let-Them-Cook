using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public GameObject parent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateMap(string sceneName)
    {
        Debug.Log("Map updating");

        /*calling upon the list of levels*/
        for (int i = 0; i < parent.transform.childCount;i++)
        {
            if (parent.transform.GetChild(i).gameObject.name == sceneName) 
            {
                parent.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
                parent.transform.GetChild(i + 1).gameObject.GetComponent<Button>().interactable = true;

                /*save game here*/
                parent.gameObject.GetComponent<GameScript>().SaveGame();
                return;

            }

        }
    }
}
