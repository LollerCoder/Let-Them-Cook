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
public class Recipe
{
    [SerializeField]
    public Sprite MealImage;
    [SerializeField]
    public ECookedMeal Name;
    [SerializeField]
    public List<IngredientAmount> IngredientsNeeded = new List<IngredientAmount>();

    public Recipe()
    {
        Name = ECookedMeal.FAIL;
        foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        {
            IngredientAmount toAdd = new IngredientAmount();
            toAdd.type = type;
            toAdd.amount = 0;
            IngredientsNeeded.Add(toAdd);
        }
    }

    public Recipe(ECookedMeal name, List<IngredientAmount> ingredientsNeeded)
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
    public List<IngredientAmount> FillInMissingIngredients (List<IngredientAmount> ingAdded)
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

    public void RemoveDuplicates()
    {
        List<IngredientAmount> ingList = new List<IngredientAmount>();

        foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        {
            IngredientAmount ing;
            ing.type = type;
            ing.amount = 0;
            foreach (IngredientAmount ingA in this.IngredientsNeeded)
            {
                if (ingA.type == type)
                    ing.amount += ingA.amount;
            }
            ingList.Add(ing);
        }

        this.IngredientsNeeded = ingList;
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
