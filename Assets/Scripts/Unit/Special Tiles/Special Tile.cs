using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTile: Unit
{
    public Unit GetUnitOnTop(GameObject parentObj)
    {
        RaycastHit hit;
        Vector3 originPoint = parentObj.transform.position;
        //Debug.DrawRay(originPoint, Vector3.up, Color.red, 5f);
        Physics.Raycast(originPoint, Vector3.up, out hit, 5f);
        //Debug.Log("Hit " + hit);

        if (hit.collider == null) return null;

        return hit.collider.gameObject.GetComponent<Unit>();
    }
}
