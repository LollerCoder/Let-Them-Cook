using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;
using System.Linq;
using UnityEditor;

[Serializable]
[CreateAssetMenu(fileName = "Save Game Data", menuName = "Jan/Saving")]
public class SaveFile : ScriptableObject
{

    /*Ingredient Checker*/
    public int potatoCount = 0;
    public int chiliCount = 0;
    public int cabbageCount = 0;
    public int carrotCount = 0;
    //[SerializedDictionary("Ingredient Data", "Inventory")]

    InventoryManager manager;
  // public SerializedDictionary<string, InventoryManager> itemList = new  SerializedDictionary<string, InventoryManager>();

    public void SaveGame()
    {
        
        //overwrite old data
        // itemList.Clear();
        // itemList.Add("IngredientUpdate", manager);
    }
     public InventoryManager LoadGame()
    {
        // InventoryManager manager = itemList["IngredientUpdate"];

        return manager;
    }

    // Start is called before the first frame update
    //[SerializedDictionary("Unit Data", "Stats")]


    //public SerializedDictionary<string, List<Unit>> savedUnits = new SerializedDictionary<string, List<Unit>>();

    //List<Unit> unitList = new List<Unit>();

    // public void SaveGame(List<Unit> units)
    // {
    //     savedUnits.Clear();

    //     unitList = units;
    //     foreach (Unit unit in units)
    //     {
    //         List<float> unitStats = new List<float>();
    //         unitStats.Add(unit.Accuracy);
    //         unitStats.Add(unit.Speed);
    //         unitStats.Add(unit.Attack);
    //         unitStats.Add(unit.HP);
    //         unitStats.Add(unit.Experience);
    //         unitStats.Add(unit.Defense);
            
    //         savedUnits.Add("Saved", unitStats);
    //     }
    // }

    // public List<Unit> LoadGame()
    // {
    //     List<Unit> loadedUnits = new List<Unit>();
    //     foreach (string unitID in savedUnits.Keys)
    //     {
    //         //* Create Unit with stats
    //         Unit loadedUnit = new Unit(unitList);
    //         loadedUnits.Add(loadedUnit);
    //     }
    //     return loadedUnits;
    // }
}
