using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    public static IngredientsManager Instance;

    [SerializeField]
    public static Dictionary<EIngredientType, int> IngredientAmount = new Dictionary<EIngredientType, int>();

    public void RemoveIngredients(List<IngredientAmount> ingredients)
    {
        foreach (IngredientAmount ingredient in ingredients)
        {
            IngredientAmount[ingredient.type] -= ingredient.amount;

            if (IngredientAmount[ingredient.type] <= 0)
                IngredientAmount[ingredient.type] = 0;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            //IngredientAmount[EIngredientType.CABBAGE] = 4;
            //IngredientAmount[EIngredientType.POTATO] = 4;
            //IngredientAmount[EIngredientType.CARROT] = 2;
            //IngredientAmount[EIngredientType.CHILI] = 6;
            // IngredientAmount[EIngredientType.CABBAGE] = 0;
            // IngredientAmount[EIngredientType.CARROT] = 0;
            // IngredientAmount[EIngredientType.CHILI] = 0;
            // IngredientAmount[EIngredientType.POTATO] = 0;
        }
        else Destroy(this.gameObject);
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
