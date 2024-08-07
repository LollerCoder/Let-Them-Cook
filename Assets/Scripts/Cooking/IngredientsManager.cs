using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    public static IngredientsManager Instance;

    public static Dictionary<EIngredientType, int> IngredientAmount = new Dictionary<EIngredientType, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            IngredientAmount[EIngredientType.CABBAGE] = InventoryManager.Instance.getItemAmount(EIngredientType.CABBAGE);
            IngredientAmount[EIngredientType.CARROT] = InventoryManager.Instance.getItemAmount(EIngredientType.CARROT);
            IngredientAmount[EIngredientType.CHILI] = InventoryManager.Instance.getItemAmount(EIngredientType.CHILI);
            IngredientAmount[EIngredientType.POTATO] = InventoryManager.Instance.getItemAmount(EIngredientType.POTATO);
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
