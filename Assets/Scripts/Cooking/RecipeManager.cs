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
    private List<IngredientsList> testList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Recipes = RecipesInput;
        }
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        foreach (Recipe rec in Recipes)
        {
            rec.PrintIngredients();
        }

        if (FindValidRecipe(testList) != null)
            Debug.Log("TAMA!");
        else
            Debug.Log("Mali!");
    }

    string FindValidRecipe(List<IngredientsList> ingredientsAdded)
    {
        string foundRecipe = null;

        foreach (Recipe recipe in Recipes)
        {
            if (recipe.IngredientsNeeded == ingredientsAdded)
                foundRecipe = recipe.Name;
        }

        return foundRecipe;
    }
}
