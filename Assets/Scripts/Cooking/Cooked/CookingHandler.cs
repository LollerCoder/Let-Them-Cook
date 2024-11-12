using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingHandler : MonoBehaviour
{
    [SerializeField]
    private List<MealSprite> _MealSprites;

    [SerializeField]
    private GameObject _mealDisplay;

    [SerializeField]
    private GameObject _statDisplay;

    [SerializeField]
    private GameObject _cookedSprite;

    [SerializeField]
    private GameObject _mealLabelContainer;
    private TextMesh _mealLabel;

    private Vector3 _failedPos;
    private Vector3 _successPos;

    public void CookMeal(ECookedMeal mealType)
    {
        if (mealType != ECookedMeal.FAIL)
        {
            this.CookSuccess(mealType);
            CookedMealDatabase.CookedMeals.Add(mealType);
        }
        else
            this.CookFailed();
    }

    private void CookSuccess(ECookedMeal mealName)
    {
        this.ToggleDisplay(true);

        this._cookedSprite.SetActive(true);
        this._cookedSprite.GetComponent<SpriteRenderer>().sprite = this.GetMealSprite(mealName);
        this._cookedSprite.transform.localPosition = _successPos;

        this._mealLabel.text = mealName + "\ncooked!";
    }

    private void CookFailed()
    {
        this.ToggleDisplay(true);

        this._cookedSprite.SetActive(false);

        this._mealLabel.text = "FAILED" + "\n\n" + "No meal with \n this recipe.";
    }

    private bool CheckValidIngredients(int carrotAmount, int potatoAmount, int chiliAmount, int cabbageAmount)
    {
        PotHandler potHandler = GameObject.Find("Pot Ingredients").GetComponent<PotHandler>();

        bool isValid = false;

        if (potHandler.IngredientsAdded[EIngredientType.CARROT] == 0 &&
            potHandler.IngredientsAdded[EIngredientType.POTATO] >= potatoAmount &&
            potHandler.IngredientsAdded[EIngredientType.CHILI] == 0 &&
            potHandler.IngredientsAdded[EIngredientType.CABBAGE] == 0
            )
        {
            isValid = true;
        }

        return isValid;
    }

    public void ToggleDisplay(bool isDisplayed)
    {
        if (this._mealDisplay != null)
            this._mealDisplay.SetActive(isDisplayed);
        else
            Debug.LogWarning("No Meal Display set!");

        if (this._statDisplay != null)
            this._statDisplay.SetActive(isDisplayed);
        //else
        //    Debug.LogWarning("No Stat Display set!");
    }

    private Sprite GetMealSprite(ECookedMeal mealType)
    {
        Sprite spr = null;

        foreach (MealSprite mealSpr in this._MealSprites)
        {
            if (mealSpr.mealName == mealType)
                return mealSpr.mealSprite;
        }

        return spr;
    }

    // Start is called before the first frame update
    void Start()
    {
        this._mealLabel = this._mealLabelContainer.GetComponent<TextMesh> ();
        //this._mealLabel.text = "Cooked" + "\n" + "Spicy Fries";

        this.ToggleDisplay(false);

        this._failedPos = new Vector3(0, -3, -6);
        this._successPos = new Vector3(0, 0, -6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
