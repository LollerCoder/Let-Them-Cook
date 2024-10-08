using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct IngredientData
{
    public EIngredientType type;
    public int amount;
}

[Serializable]
public class Ingredient
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public List<IngredientData> IngredientsNeeded;

    /*Amount Tracker after each won battle*/
    public Ingredient (Ingredient ing)
    {
        Name = ing.Name;
        IngredientsNeeded = ing.IngredientsNeeded;
    }

    
    /*Save and Load*/
      public void SaveProgress()
    {
        SaveGame.SaveInventory(this);
    }

    public void LoadProgress()
    {
        Ingredient data = SaveGame.LoadInventory();

        Name = data.Name;
        IngredientsNeeded = data.IngredientsNeeded;
    }
}