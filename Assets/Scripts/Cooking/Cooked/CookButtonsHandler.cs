using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookButtonsHandler : MonoBehaviour
{

    private Recipe recipeData;

    private Ingredient ingredientData;
    private IEnumerator CookIngredients()
    {
        PotHandler potTracker = GameObject.Find("Pot Ingredients").GetComponent<PotHandler>();
        potTracker.IsCooking = true;

        yield return new WaitForSeconds(1);

        CookingHandler cookingHandler = GameObject.Find("Cooking").GetComponent<CookingHandler>();
        recipeData = new Recipe(ECookedMeal.INPUT, potTracker.InputtedIngredients);
        recipeData.FillInMissingIngredients();
        recipeData.RemoveDuplicates();
        //recipeData.PrintIngredients();
        ECookedMeal mealToCook = RecipeManager.Instance.FindValidRecipe(recipeData.IngredientsNeeded);
        //Debug.Log("Found recipe for " + mealToCook);
        cookingHandler.CookMeal(mealToCook);

        IngredientsManager.Instance.RemoveIngredients(recipeData.IngredientsNeeded);

        //foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        //{
        //    potTracker.IngredientsAdded[type] = 0;
        //}
    }

    private void ResetIngredients()
    {
        IngredientAmountTracker tracker = GameObject.Find("Ingredients").GetComponent<IngredientAmountTracker>();
        PotHandler potTracker = GameObject.Find("Pot Ingredients").GetComponent<PotHandler>();
        potTracker.IsCooking = false;

        foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        {
            IngredientsManager.IngredientAmount[type] += potTracker.IngredientsAdded[type];

            tracker.SetAmount(
                type,
                IngredientsManager.IngredientAmount[type]
                );
        }

        potTracker.ResetIngredients();
    }

    private void CookAgain()
    {
        IngredientAmountTracker tracker = GameObject.Find("Ingredients").GetComponent<IngredientAmountTracker>();
        PotHandler potTracker = GameObject.Find("Pot Ingredients").GetComponent<PotHandler>();
        potTracker.IsCooking = false;

        potTracker.ResetIngredients();
    }

    public void OnMouseDown()
    {
        switch (this.gameObject.name)
        {
            case "Cook":
                this.StartCoroutine(this.CookIngredients());
                break;
            case "Reset":
                this.ResetIngredients();
                break;
            case "Cook Again":
                GameObject.Find("Cooking").GetComponent<CookingHandler>().ToggleDisplay(false);
                this.CookAgain();
                break;
            case "Go To Map":
                SceneManager.LoadScene("Map");
                break;
            case "Exit":
                ingredientData.SaveProgress();
                /*Save ingredients and recipes here*/
                //recipeData.SaveProgress();
             

                break;
            case "Bookmark":
                GameObject.Find("Recipe Book Popup").GetComponent<RecipeBPPHandler>().ToggleDisplay(true);
                break;
            case "Popdown Cookbook":
                GameObject.Find("Recipe Book Popup").GetComponent<RecipeBPPHandler>().ToggleDisplay(false);
                break;

            case "Clipboard Closer":
                RecipeClipboardManager.Instance.ShowClipboard(false);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
