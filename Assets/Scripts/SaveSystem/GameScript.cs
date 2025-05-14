using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Tutorial : https://www.youtube.com/watch?v=vUEJGYg7FsQ
public class GameScript : MonoBehaviour
{
    //public bool bComplete = false;
    public GameObject parent;

    public static List<LevelSelector> levelList = new List<LevelSelector>();



    void Start()
    {
        parent = this.gameObject;
        LoadGame();
        //this.lvl3.interactable = false;

   
    }

    void Update()
    {
        //if (bComplete) this.gameObject.GetComponent<Button>().interactable = true;
    }

    public void SaveGame()
    {
        FileSystem.saveProgress(this);
        Debug.Log(this.gameObject.name + " saved");
    }
    
    public void LoadGame()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {

            levelList.Add(parent.transform.GetChild(i).gameObject.GetComponent<LevelSelector>());
            //Debug.Log(parent.transform.GetChild(i));
         }
         
        GameData data = FileSystem.LoadProgress();

         //load it back here
        for (int i = 0; i < parent.transform.childCount;i++)
        {
            //levelList[i].bComplete = data.finishedLvlList[i];
            levelList[i].canLoad = data.finishedLvlList[i];
            Debug.Log(parent.transform.GetChild(i).name + "    "  + levelList[i].canLoad);
        }
        
    }

}
