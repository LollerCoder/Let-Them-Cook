using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    private Recipe recipeData;
    private Ingredient ingredientData;

    private SaveFile data;

    private void UpdateCurrentAmount()
    {
        //data.cabbageCount = InventoryManager.Instance.getItemAmount(EIngredientType.CABBAGE);
        //data.carrotCount = InventoryManager.Instance.getItemAmount(EIngredientType.CARROT);
        //data.chiliCount = InventoryManager.Instance.getItemAmount(EIngredientType.CHILI);
        //data.potatoCount = InventoryManager.Instance.getItemAmount(EIngredientType.POTATO);
        //AssetDatabase.CreateAsset(data, "Assets/Scripts/BattleSystem/Sample/New Save File.asset");
        
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

        //Loads the amount of ingreidents collected
        SaveFile data = AssetDatabase.LoadAssetAtPath<SaveFile>("Assets/Scripts/BattleSystem/Sample/New Save File.asset");

        if (data != null )
        {
            Debug.Log("Data file found!");
        }

        this._IngredientTracker[EIngredientType.CARROT] = this._AmountLabels[0].GetComponent<TextMesh>();
        this._IngredientTracker[EIngredientType.POTATO] = this._AmountLabels[1].GetComponent<TextMesh>();
        this._IngredientTracker[EIngredientType.CHILI] = this._AmountLabels[2].GetComponent<TextMesh>();
        this._IngredientTracker[EIngredientType.CABBAGE] = this._AmountLabels[3].GetComponent<TextMesh>();

        /*Load ingredients and recipes here*/
        //recipeData.LoadProgress();
        //ingredientData.LoadProgress();



        IngredientsManager.IngredientAmount[EIngredientType.CABBAGE] = 0;
        IngredientsManager.IngredientAmount[EIngredientType.POTATO] = 1;
        IngredientsManager.IngredientAmount[EIngredientType.CARROT] = 0;
        IngredientsManager.IngredientAmount[EIngredientType.CHILI] = 1;

    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateCurrentAmount();
    }
}
