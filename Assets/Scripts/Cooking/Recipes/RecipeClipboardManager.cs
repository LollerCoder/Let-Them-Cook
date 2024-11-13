using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct IngredientSprite{
    public EIngredientType ingredientType;
    public Sprite ingredientSprite;
}

[Serializable]
public struct MealSprite{
    public ECookedMeal mealName;
    public Sprite mealSprite;
}

public class RecipeClipboardManager : MonoBehaviour
{
    public static RecipeClipboardManager Instance;

    [SerializeField]
    private GameObject clipboardRef;

    [SerializeField]
    private List<IngredientSprite> _IngredientSprites;

    [SerializeField]
    private List<MealSprite> _MealSprites;

    [SerializeField]
    private List<GameObject> _recipeIngredientSpriteHolders;

    [SerializeField]
    private GameObject _MealSpriteRef;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        this.ShowClipboard(false);
    }

    public void ShowMealInfo(ECookedMeal mealName)
    {
        this.ShowClipboard(true);
        //Change the meal sprite
        foreach (MealSprite sprite in _MealSprites)
        {
            if (sprite.mealName == mealName)
            {
                //Debug.Log("Meal: " +  mealName);
                this.ChangeSprite(this._MealSpriteRef, sprite.mealSprite);
            }
        }

        //Change the sprites of the ingredients
        this.ChangeIngredientSprite(RecipeManager.Instance.GetIngredientsInRecipe(mealName));
    }

    public void ShowClipboard(bool show)
    {
        this.clipboardRef.SetActive(show);
    }

    private void ChangeSprite(GameObject holder, Sprite sprite)
    {
        holder.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private Sprite GetIngredientSprite(EIngredientType type)
    {
        Sprite toReturn = null;

        foreach (IngredientSprite ingredientSprite in _IngredientSprites)
        {
            if (ingredientSprite.ingredientType == type)
                toReturn = ingredientSprite.ingredientSprite;
        }

        return toReturn;
    }

    private void ChangeIngredientSprite(List<IngredientAmount> ingredients)
    {
        List<EIngredientType> ingredientsAdded = new List<EIngredientType>();

        foreach (IngredientAmount ingA in ingredients)
        {
            if (ingA.amount > 0)
            {
                for (int i = 0; i <ingA.amount; i++)
                    ingredientsAdded.Add(ingA.type);
            }
        }

        Sprite spriteToLoad;
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Changing sprite " + i + " / Ingredient count " + ingredients.Count);
            if (i < ingredientsAdded.Count)
            {
                spriteToLoad = this.GetIngredientSprite(ingredientsAdded[i]);
                this._recipeIngredientSpriteHolders[i].SetActive(true);
                this._recipeIngredientSpriteHolders[i].GetComponent<SpriteRenderer>().sprite = spriteToLoad;
            }
            else
            {
                this._recipeIngredientSpriteHolders[i].SetActive(false);
            }
        }
    }
}
