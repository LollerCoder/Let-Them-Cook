using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;
using System.Linq;

[Serializable]
[CreateAssetMenu(fileName = "New Save File", menuName = "Jan/Saving")]
public class SaveFile : ScriptableObject
{
    // Start is called before the first frame update
    [SerializedDictionary("Unit ID", "Stats")]
    public SerializedDictionary<string, List<float>> savedUnits = new SerializedDictionary<string, List<float>>();

    //Unique Ids must be added
    public void SaveGame(Unit[] units)
    {
        savedUnits.Clear();

        List<Unit> unitList = units.ToList();
        foreach (Unit unit in unitList)
        {
            List<float> unitStats = new List<float>();
            unitStats.Add(unit.Accuracy);
            unitStats.Add(unit.Speed);
            unitStats.Add(unit.Attack);
            unitStats.Add(unit.HP);
            unitStats.Add(unit.Experience);
            unitStats.Add(unit.Defense);
            savedUnits.Add(unit.name, unitStats);
        }
    }

    public List<Unit> LoadGame()
    {
        List<Unit> loadedUnits = new List<Unit>();
        foreach (string unitID in savedUnits.Keys)
        {
            //* Create Unit with stats
            // Unit loadedUnit = new Unit(savedUnits[unitID][0], savedUnits[unitID][1]);
            // loadedUnits.Add(loadedUnit);
        }
        return loadedUnits;
    }
}
