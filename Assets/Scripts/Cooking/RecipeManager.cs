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
        Debug.Log("I am in " + this.gameObject.name);
        //foreach (Recipe rec in Recipes)
        //{
        //    rec.PrintIngredients();
        //}

        testRecipe.FillInMissingIngredients();
        //testRecipe.PrintIngredients();
        if (FindValidRecipe(testRecipe.IngredientsNeeded) != ECookedMeal.FAIL)
            Debug.Log("TAMA!");
        else
            Debug.Log("Mali!");
    }

    private ECookedMeal FindValidRecipe(List<IngredientAmount> ingredientsAdded)
    {
        ECookedMeal foundRecipe = ECookedMeal.FAIL;

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

    public List<IngredientAmount> GetIngredientsInRecipe(ECookedMeal mealName)
    {
        List<IngredientAmount> ingredients = new List<IngredientAmount>();
        foreach (Recipe recipe in Recipes)
        {
            if (recipe.Name == mealName)
                ingredients = recipe.IngredientsNeeded;
        }
        ingredients.RemoveAll(s => s.amount <= 0);

        return ingredients;
    }
}
