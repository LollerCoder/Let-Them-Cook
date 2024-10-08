using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingHandler : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _mealSprites = new List<Sprite>();

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

    public void CookMeal()
    {
        //if (this.CheckValidIngredients(0,1,1,0))
        //{
        //    this.CookSuccess("Spicy Fries");
        //}
        //else 
        if (this.CheckValidIngredients(0, 1, 0, 0))
        {
            this.CookSuccess("Fries");
        }
        else
        {
            this.CookFailed();
        }
    }

    private void CookSuccess(string mealName)
    {
        this.ToggleDisplay(true);

        this._cookedSprite.SetActive(true);
        this._cookedSprite.GetComponent<SpriteRenderer>().sprite = this._mealSprites[1];
        this._cookedSprite.transform.localPosition = _successPos;

        this._mealLabel.text = "SUCCESS" + "\n\n" + mealName + " cooked!";
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
        this._mealDisplay.SetActive(isDisplayed);
        this._statDisplay.SetActive(isDisplayed);
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
