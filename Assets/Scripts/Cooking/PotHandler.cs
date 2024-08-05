using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotHandler : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();

    [SerializeField]
    private Dictionary<EIngredientType, Sprite> _ingredientSprite = new Dictionary<EIngredientType, Sprite>();

    [SerializeField]
    private List<GameObject> _ingredientHolder = new List<GameObject>();

    //amount of ingredients
    private int _ingredientAmount = 0;
    public Dictionary<EIngredientType, int> IngredientsAdded = new Dictionary<EIngredientType, int>();

    //Cooking
    public bool IsCooking = false;
    [SerializeField]
    private GameObject _PotTargetPos;

    private void SetSprite(int index, EIngredientType type)
    {
        this._ingredientHolder[index].SetActive(true);
        this._ingredientHolder[index].GetComponent<SpriteRenderer>().sprite = this._ingredientSprite[type];
    }

    public void AddIngredient(EIngredientType type)
    {
        if (this._ingredientAmount >= 5) return;

        this.SetSprite(this._ingredientAmount, type);

        IngredientsManager.IngredientAmount[type]--;
        this.IngredientsAdded[type]++;
        this._ingredientAmount++;
    }

    public void ResetIngredients()
    {
        this._ingredientAmount = 0;

        //ingredient amount
        foreach (EIngredientType type in Enum.GetValues(typeof(EIngredientType)))
        {
            this.IngredientsAdded[type] = 0;
        }

        //ingredient holder
        int posIndex = 0;
        foreach (GameObject holder in this._ingredientHolder)
        {
            holder.GetComponent<CookedMover>().GoHome();
            posIndex++;
            if (holder.activeSelf)
            holder.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //sprites
        this._ingredientSprite[EIngredientType.CABBAGE] = sprites[0];
        this._ingredientSprite[EIngredientType.CARROT] = sprites[1];
        this._ingredientSprite[EIngredientType.CHILI] = sprites[2];
        this._ingredientSprite[EIngredientType.POTATO] = sprites[3];

        this.ResetIngredients();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCooking)
        {
            foreach (GameObject ingredient in this._ingredientHolder)
            {
                ingredient.GetComponent<CookedMover>().TargetPos = this._PotTargetPos.transform.position;
            }
        }
    }
}
