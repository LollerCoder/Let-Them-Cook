using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{

    /*Map /Level variables*/
    public bool[] finishedLvlList;

    /*Character/Unit variables*/
    
   /* 
   Save System (No more EXP or Stats, just the unlockable characters and the levels unlocked/accomplished)
   */
    
    public GameData(GameScript clickable)
    {
        //3 bc there are 3 elements atm
        finishedLvlList = new bool[5]; 

        //make a list inside the loop
        for (int i = 0; i < clickable.parent.transform.childCount;i++)
        {
            finishedLvlList[i] = clickable.parent.transform.GetChild(i).GetComponent<LevelSelector>().canLoad;
            Debug.Log(clickable.parent.transform.GetChild(i) +  "   "  +  clickable.parent.transform.GetChild(i).GetComponent<LevelSelector>().canLoad);
        }
        Debug.Log("Overwrite done");
        
    }

}
