using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientHandler : Draggable
{
    [SerializeField]
    private EIngredientType _type;

    private Vector3 _initPos;
    private bool _inPot = false;

    private void OnTriggerEnter(Collider other)
    {
        _inPot = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _inPot = false;
    }

    private void Start()
    {
        this._initPos = this.gameObject.transform.position;
    }

    private void Update()
    {
        if (isDragging)
        {
            DragObject();
        }
        else
        {
            this.gameObject.transform.position = this._initPos;

            if (this._inPot)
            {
                this._inPot = false;
                if (IngredientsManager.IngredientAmount[this._type] > 0)
                {
                    GameObject.Find("Pot Ingredients").GetComponent<PotHandler>().AddIngredient(this._type);
                    IngredientsManager.IngredientAmount[this._type]--;
                }
            }
        }
    }
}
