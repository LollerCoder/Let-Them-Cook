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
    public List<IngredientData> IngredientsNeeded = new List<IngredientData>();

       public Ingredient (Ingredient ing)
    {
        IngredientsNeeded = ing.IngredientsNeeded;
    }

     public void SaveProgress()
    {
        IngredientsDatabase.SaveIngredients(this);
    }

    public void LoadProgress()
    {
        Ingredient data = IngredientsDatabase.LoadIngredients();

        IngredientsNeeded = data.IngredientsNeeded;
    }
}
