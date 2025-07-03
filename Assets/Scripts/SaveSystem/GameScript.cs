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
    public GameObject parent;

    public List<LevelSelector> levelList = new List<LevelSelector>();

    public bool bData = false;

    private int currLvl = 0;

    void Start()
    {
        parent = this.gameObject;

    }

    public void SaveGame()
    {
        FileSystem.saveProgress(this);
        Debug.Log(this.gameObject.name + " saved");
    }

    public void LoadGame()
    {
        Debug.Log("LOAD GAME");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            levelList.Add(parent.transform.GetChild(i).gameObject.GetComponent<LevelSelector>());
        }

        GameData data = FileSystem.LoadProgress();
        if (data == null) { Debug.Log("Save File not found"); return; }
        else bData = true;


        //load it back here
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            levelList[i].canLoad = data.finishedLvlList[i];
            Debug.Log(parent.transform.GetChild(i).name + "    " + levelList[i].canLoad);
            if (levelList[i].canLoad) currLvl = int.Parse(levelList[i].name.Split("-")[1]);
        }



    }

    public int getCurrLvl()
    {
        return 0;//currLvl;
    }

}
