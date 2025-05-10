using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public static class FileSystem
{
    public static void saveProgress(GameScript info)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PlayerSaveData.veggie";

        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(info);

        //write dara into file
        formatter.Serialize(stream,data);
        
        stream.Close();

    }

    public static GameData LoadProgress()
    {
        string path = Application.persistentDataPath + "/PlayerSaveData.veggie";
    
        if (File.Exists(path))
        {
            Debug.Log("load exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data =  formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;


        } else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        } 
    }
}
