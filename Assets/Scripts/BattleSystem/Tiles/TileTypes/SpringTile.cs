using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class SpringTile : Tile
{
    [SerializeField] private Tile Location1;
    private Unit unitToLaunch;

    public new void Start()
    {
        base.Start();
        this.tileType = ETileType.SPRING;
    }

    public void setUnitToLaunch(Unit unit)
    {
        //RaycastHit hit;


        //if (Physics.Raycast(this.gameObject.transform.position, Vector3.up, out hit, 1.0f))
        //{
        //    this.unitToLaunch = hit.collider.gameObject.GetComponent<Unit>();
        //}
        unitToLaunch = unit;
        unitToLaunch.Tile = Location1;
        unit.OnSpring(true);
    }

    public override void ApplyOnUnitStart()
    {
        if(unitToLaunch != null)
        {
            if(unitToLaunch == UnitActionManager.Instance.GetFirstUnit() as Unit)
            {
                unitToLaunch.gameObject.transform.position = Location1.transform.position;
                unitToLaunch = null;
            }
            
        }
    }

}
