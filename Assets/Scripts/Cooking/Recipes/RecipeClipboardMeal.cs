using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeClipboardMeal : MonoBehaviour
{
    [SerializeField]
    public ECookedMeal MealType;

    private void Start()
    {

    }

    public void OnMouseDown()
    {
        RecipeClipboardManager.Instance.ShowMealInfo(this.MealType);
    }
}
