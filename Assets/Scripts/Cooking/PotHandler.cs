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

    private int _ingredientAmount = 0;

    private void SetSprite(int index, EIngredientType type)
    {
        this._ingredientHolder[index].SetActive(true);
        this._ingredientHolder[index].GetComponent<SpriteRenderer>().sprite = this._ingredientSprite[type];
    }

    public void AddIngredient(EIngredientType type)
    {
        if (this._ingredientAmount >= 5) return;

        this.SetSprite(this._ingredientAmount, type);

        this._ingredientAmount++;
    }

    // Start is called before the first frame update
    void Start()
    {
        //sprites
        this._ingredientSprite[EIngredientType.CABBAGE] = sprites[0];
        this._ingredientSprite[EIngredientType.CARROT] = sprites[1];
        this._ingredientSprite[EIngredientType.CHILI] = sprites[2];
        this._ingredientSprite[EIngredientType.POTATO] = sprites[3];

        //ingredient holder
        foreach (GameObject holder in this._ingredientHolder)
        {
            holder.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
