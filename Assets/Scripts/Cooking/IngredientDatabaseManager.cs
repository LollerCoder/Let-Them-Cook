using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


/*Saves all unlocked recipes and current Inventory*/
//Save and Load code by: https://www.youtube.com/watch?v=XOjd_qU2Ido&t=939s
public class IngredientsDatabase : MonoBehaviour
{
    public bool bLoad = true;
    public static IngredientsDatabase Instance;

    public Recipe recipeData;

    /*Recipes*/
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


    /*Ingredients*/
     public static void SaveIngredients (Ingredient ingList)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = UnityEngine.Application.persistentDataPath + "/Save.Slots";

        FileStream stream = new FileStream(path,FileMode.Create);

        Ingredient data = new Ingredient(ingList);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static Ingredient LoadIngredients()
    {
        //getting the path where the game is saved
        string path = UnityEngine.Application.persistentDataPath+ "/Save.Slots";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Ingredient data = formatter.Deserialize(stream) as Ingredient;

            stream.Close();

            return data;

        } else
        {
            Debug.LogError("Save not found   || " + path);
            return null;
        }
    }
}

