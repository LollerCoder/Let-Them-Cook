using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookButtonsHandler : MonoBehaviour
{
    private IEnumerator CookIngredients()
    {
        PotHandler potTracker = GameObject.Find("Pot Ingredients").GetComponent<PotHandler>();
        potTracker.IsCooking = true;

        yield return new WaitForSeconds(1);

        CookingHandler cookingHandler = GameObject.Find("Cooking").GetComponent<CookingHandler>();
        cookingHandler.CookMeal();

        foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        {
            potTracker.IngredientsAdded[type] = 0;
        }
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
                GameObject.Find("Cooking").GetComponent<CookingHandler>().HideDisplay();
                this.ResetIngredients();
                break;
            case "Exit":
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
