using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private Dictionary<EIngredientType, int> Inventory = new Dictionary<EIngredientType, int>();

    public void OnAddToInventory(EIngredientType ingredient)
    {
        Debug.Log(ingredient + " enter inventory manager");

        if (Inventory.ContainsKey(ingredient))
        {
            Inventory[ingredient]++;
        }
        else
        {
            Debug.Log(ingredient + " Added");
            Inventory.Add(ingredient, 1);
        }
    }

    public int getItemAmount(EIngredientType type)
    {
        int amount = 0;

        if (Inventory.ContainsKey(type))
        {
            amount = Inventory[type];
        }
        else
        {
            amount = 0;
        }

        return amount;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnAddItem += OnAddToInventory;
        //SaveFile data = AssetDatabase.LoadAssetAtPath<SaveFile>("Assets/Scripts/BattleSystem/Sample/New Save File.asset");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
