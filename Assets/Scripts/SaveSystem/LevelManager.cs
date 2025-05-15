using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

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

        int levelNum = int.Parse(sceneName.Split("-")[1]);
        GameObject.Find("Map").transform.GetChild(levelNum).GetComponent<LevelSelector>().canLoad = true;
        //parent.transform.GetChild(levelNum).gameObject.GetComponent<LevelSelector>().canLoad = true;
        Debug.Log(  GameObject.Find("Map").transform.GetChild(levelNum).gameObject.name + "  is  "  +  GameObject.Find("Map").transform.GetChild(levelNum).gameObject.GetComponent<LevelSelector>().canLoad);
        Debug.Log(  GameObject.Find("Map").transform.GetChild(levelNum + 1).gameObject.name  + "  is  "  +  GameObject.Find("Map").transform.GetChild(levelNum + 1).gameObject.GetComponent<LevelSelector>().canLoad);
        //GameObject.Find("Map").GetComponent<GameScript>().SaveGame();

        // /*calling upon the list of levels*/
        // for (int i = 0; i < parent.transform.childCount;i++)
        // {
           
        //      if (parent.transform.GetChild(i).gameObject.name == sceneName) 
        //      {
        //         parent.transform.GetChild(i + 1).gameObject.GetComponent<LevelSelector>().canLoad = false;
        //          Debug.Log( parent.transform.GetChild(i + 1).gameObject.name + "  is now saved");
        //          Debug.Log( parent.transform.GetChild(i + 2).gameObject.name + "  is  "  + parent.transform.GetChild(i + 2).gameObject.GetComponent<LevelSelector>().canLoad);

        //     // /*save game here*/
        //      parent.gameObject.GetComponent<GameScript>().SaveGame();
        //      return;

        //      } else Debug.Log( parent.transform.GetChild(i).gameObject.name + "  is  "  + parent.transform.GetChild(i).gameObject.GetComponent<LevelSelector>().canLoad);

        // }

    
    }
}
