using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientHandler : Draggable
{
    private Vector3 _initPos;

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
        }
    }
}
