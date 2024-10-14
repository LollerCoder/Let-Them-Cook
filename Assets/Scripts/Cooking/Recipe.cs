using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IngredientAmount
{
    public EIngredientType type;
    public int amount;
}

[Serializable]
public class Recipe :  MonoBehaviour
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public List<IngredientAmount> IngredientsNeeded;

    public Recipe()
    {
        Name = "Empty";
        foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        {
            IngredientAmount toAdd = new IngredientAmount();
            toAdd.type = type;
            toAdd.amount = 0;
            IngredientsNeeded.Add(toAdd);
        }
    }

    public Recipe(string name, List<IngredientAmount> ingredientsNeeded)
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
    private List<IngredientAmount> FillInMissingIngredients (List<IngredientAmount> ingAdded)
    {
        IngredientAmount ingListToAdd = new IngredientAmount();

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

    public void FillInMissingIngredients()
    {
        this.IngredientsNeeded = this.FillInMissingIngredients(this.IngredientsNeeded);
    }

    private bool IsIngredientInRecipe(EIngredientType type, List<IngredientAmount> ingredientsAdded)
    {
        bool isIn = false;

        foreach(IngredientAmount ingList in ingredientsAdded)
        {
            if (ingList.type == type) isIn = true;
        }

        return isIn;
    }

    public void PrintIngredients()
    {
        Debug.Log("Recipe for " + this.Name);
        foreach(IngredientAmount ingList in this.IngredientsNeeded)
        {
            Debug.Log(ingList.type + " = " + ingList.amount);
        }
    }

    
    public Recipe (Recipe recipe)
    {
        Name = recipe.Name;
        IngredientsNeeded = recipe.IngredientsNeeded;
    }

    public void SaveProgress()
    {
        IngredientsDatabase.SaveRecipe(this);
    }

    public void LoadProgress()
    {
        Recipe data = IngredientsDatabase.LoadRecipe();

        Name = data.Name;
        IngredientsNeeded = data.IngredientsNeeded;
    }
}
