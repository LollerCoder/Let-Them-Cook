using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookedMealDatabase : MonoBehaviour
{
    public static CookedMealDatabase Instance;

    public static List<ECookedMeal> CookedMeals;

    private void PrintCookedMeals()
    {
        Debug.Log("Cooked meals on inventory: ");
        foreach (ECookedMeal cm in CookedMeals)
        {
            Debug.Log(cm);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CookedMeals = new List<ECookedMeal>();
        }
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.PrintCookedMeals();
        }
    }
}
