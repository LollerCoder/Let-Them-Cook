using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    [SerializeField]
    public List<Recipe> RecipesInput = new List<Recipe>();
    public static List<Recipe> Recipes = new List<Recipe>();

    [SerializeField]
    private Recipe testRecipe;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            foreach (Recipe rec in RecipesInput)
            {
                rec.FillInMissingIngredients();
            }
            Recipes = RecipesInput;
        }
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        //foreach (Recipe rec in Recipes)
        //{
        //    rec.PrintIngredients();
        //}

        testRecipe.FillInMissingIngredients();
        //testRecipe.PrintIngredients();
        if (FindValidRecipe(testRecipe.IngredientsNeeded) != null)
            Debug.Log("TAMA!");
        else
            Debug.Log("Mali!");
    }

    private string FindValidRecipe(List<IngredientAmount> ingredientsAdded)
    {
        string foundRecipe = null;

        foreach (Recipe recipe in Recipes)
        {
            if (IsIngredientValid(ingredientsAdded, recipe))
                foundRecipe = recipe.Name;
        }

        return foundRecipe;
    }

    //Check if the ingredient list matches the one in the given recipe
    private bool IsIngredientValid(List<IngredientAmount> ingredientsAdded, Recipe currentRecipe)
    {
        bool isValid = true;

        foreach (IngredientAmount ing in ingredientsAdded)
        {
            foreach (IngredientAmount ingInRecipe in currentRecipe.IngredientsNeeded)
            {
                if (ingInRecipe.type == ing.type &&
                    ingInRecipe.amount != ing.amount
                    )
                isValid = false;
            }
        }

        return isValid;
    }
}
