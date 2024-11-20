// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using System.Runtime.Serialization.Formatters.Binary;
// using System.IO;

// public static class SaveGainSystem 
// {
//     public static void SaveBattleResults(Unit results)
//     {
//         BinaryFormatter formatter = new BinaryFormatter();
//         string path = Application.persistentDataPath + "/party.battleExp";
//         FileStream stream = new FileStream(path, FileMode.Create);

//         Unit New_results = new Unit(results);

//         formatter.Serialize(stream,New_results);
//         stream.Close();
//     }

//     public static Unit LoadBattleResults()
//     {
//         string path = Application.persistentDataPath + "/party.battleExp";

//         if (File.Exists(path))
//         {
//             BinaryFormatter formatter = new BinaryFormatter();
//             FileStream stream = new FileStream(path, FileMode.Open);

//             Unit data  = formatter.Deserialize(stream) as UnitDatabaseManager;
//             stream.Close();

//             return data;


//         } else{
//             Debug.LogError("save file not found in " + path);
//             return null;
//         }
//     }
// }
