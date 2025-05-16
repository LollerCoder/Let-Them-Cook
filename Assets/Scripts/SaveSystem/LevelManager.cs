using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public GameObject parent;
    //public GameObject parent;
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

        int levelNum = int.Parse(SceneManager.GetActiveScene().name.Split("-")[1]);

        parent.transform.GetChild(levelNum).gameObject.GetComponent<LevelSelector>().canLoad = true;
        Debug.Log(parent.transform.GetChild(levelNum).gameObject.name + "  is now saved");

        /*save game here*/
        parent.gameObject.GetComponent<GameScript>().SaveGame();

    
    }
}
