using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IngredientData
{
    public EIngredientType type;
    public int amount;
}

[Serializable]
public class Ingredient
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public List<IngredientData> IngredientsNeeded;
}
