using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookButtonsHandler : MonoBehaviour
{
    private void CookIngredients()
    {
        PotHandler potTracker = GameObject.Find("Pot Ingredients").GetComponent<PotHandler>();
        potTracker.IsCooking = true;
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
                this.CookIngredients();
                break;
            case "Reset":
                this.ResetIngredients();
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
