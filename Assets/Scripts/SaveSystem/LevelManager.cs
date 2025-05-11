using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }
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
        // for (int i = 0; i < GameScript.parent.transform.childCount;i++)
        // {
        //     if (GameScript.parent.transform.GetChild(i).gameObject.name == sceneName) 
        //     {
        //         GameScript.parent.transform.GetChild(i).gameObject.GetComponent<GameScript>().bComplete = true;

        //         /*save game here*/
        //         GameScript.parent.transform.GetChild(i).gameObject.GetComponent<GameScript>().SaveGame();
        //         return;

        //     }

        // }
    }
}
