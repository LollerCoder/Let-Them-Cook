using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IngredientsList
{
    public EIngredientType type;
    public int amount;
}

[Serializable]
public class Recipe
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public List<IngredientsList> IngredientsNeeded;

    public Recipe()
    {
        Name = "Empty";
        foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        {
            IngredientsList toAdd = new IngredientsList();
            toAdd.type = type;
            toAdd.amount = 0;
            IngredientsNeeded.Add(toAdd);
        }
    }

    public Recipe(string name, List<IngredientsList> ingredientsNeeded)
    {
        Name = name;
        if (ingredientsNeeded.Count == Enum.GetValues(typeof(EIngredientType)).Length)
            IngredientsNeeded = ingredientsNeeded;
        else
        {
            IngredientsNeeded = FillInMissingIngredients(ingredientsNeeded);
        }
    }

    //will add the unadded ingredients as zero
    private List<IngredientsList> FillInMissingIngredients (List<IngredientsList> ingAdded)
    {
        IngredientsList ingListToAdd = new IngredientsList();

        foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        {
            if (!IsIngredientInRecipe(type, ingAdded))
            {
                ingListToAdd.type = type;
                ingListToAdd.amount = 0;
                ingAdded.Add(ingListToAdd);
            }
        }

        return ingAdded;
    }

    private bool IsIngredientInRecipe(EIngredientType type, List<IngredientsList> ingredientsAdded)
    {
        bool isIn = false;

        foreach(IngredientsList ingList in ingredientsAdded)
        {
            if (ingList.type == type) isIn = true;
        }

        return isIn;
    }

    public void PrintIngredients()
    {
        Debug.Log("Recipe for " + this.Name);
        foreach(IngredientsList ingList in this.IngredientsNeeded)
        {
            Debug.Log(ingList.type + " = " + ingList.amount);
        }
    }
}
