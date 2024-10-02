using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class SaveGame
{
    public static void SaveRecipe (Recipe ingList)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = UnityEngine.Application.persistentDataPath + "/Save.Slots";

        FileStream stream = new FileStream(path,FileMode.Create);

        Recipe data = new Recipe(ingList);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static Recipe LoadRecipe()
    {
        //getting the path where the game is saved
        string path = UnityEngine.Application.persistentDataPath+ "/Save.Slots";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           Recipe data = formatter.Deserialize(stream) as Recipe;

           stream.Close();

           return data;


        } else
        {
            Debug.LogError("Save not found   || " + path);
            return null;
        }
    }
}
