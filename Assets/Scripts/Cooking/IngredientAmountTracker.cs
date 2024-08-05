using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientAmountTracker : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _AmountLabels;

    private Dictionary<EIngredientType, TextMesh> _IngredientTracker = new Dictionary<EIngredientType, TextMesh>();

    public void SetAmount(EIngredientType type, int amount)
    {
        this._IngredientTracker[type].text = "Amount: " + amount.ToString();
    }

    private void UpdateCurrentAmount()
    {
        EIngredientType currentType = EIngredientType.CARROT;
        this.SetAmount(
            currentType,
            IngredientsManager.IngredientAmount[currentType]
            );

        currentType = EIngredientType.CABBAGE;
        this.SetAmount(
            currentType,
            IngredientsManager.IngredientAmount[currentType]
            );

        currentType = EIngredientType.CHILI;
        this.SetAmount(
            currentType,
            IngredientsManager.IngredientAmount[currentType]
            );

        currentType = EIngredientType.POTATO;
        this.SetAmount(
            currentType,
            IngredientsManager.IngredientAmount[currentType]
            );
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject label in _AmountLabels)
        {
            label.GetComponent<TextMesh>().text = "Amount: 0";
        }

        this._IngredientTracker[EIngredientType.CARROT] = this._AmountLabels[0].GetComponent<TextMesh>();
        this._IngredientTracker[EIngredientType.POTATO] = this._AmountLabels[1].GetComponent<TextMesh>();
        this._IngredientTracker[EIngredientType.CHILI] = this._AmountLabels[2].GetComponent<TextMesh>();
        this._IngredientTracker[EIngredientType.CABBAGE] = this._AmountLabels[3].GetComponent<TextMesh>();

        IngredientsManager.IngredientAmount[EIngredientType.CABBAGE] = 4;
        IngredientsManager.IngredientAmount[EIngredientType.CARROT] = 2;
        IngredientsManager.IngredientAmount[EIngredientType.CHILI] = 6;
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateCurrentAmount();
    }
}
