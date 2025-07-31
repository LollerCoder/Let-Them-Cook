using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class SpringTile : Tile
{
    [SerializeField] private Tile Location1;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dummyToLaunch;
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

    }

    public override void ApplyOnUnitStart(Unit unit)
    {
        unitToLaunch = unit;
        dummyToLaunch.GetComponent<SpriteRenderer>().enabled = true;
        dummyToLaunch.GetComponent<SpriteRenderer>().sprite = unitToLaunch.spriteRenderer.sprite;
        unitToLaunch.OnSpring(true);//go up
        animator.SetTrigger("Spring");
        
        unitToLaunch.Tile = Location1;
        //unitToLaunch.gameObject.transform.position = Location1.transform.position;
        unitToLaunch = null;
        Parameters param = new Parameters();
        param.PutExtra("pos", Location1.transform.position);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.SPRING, param);
        this.CheckIfThereIsUnitOnLandingTile();
        //if (unitToLaunch != null)
        //{
        //    if(unitToLaunch == UnitActionManager.Instance.GetFirstUnit() as Unit)
        //    {
        //        unitToLaunch.OnSpring(true);//go up
        //        animator.SetTrigger("Spring");
                
        //        unitToLaunch.Tile = Location1;
        //        //unitToLaunch.gameObject.transform.position = Location1.transform.position;
        //        unitToLaunch = null;
        //        Parameters param = new Parameters();
        //        param.PutExtra("pos", Location1.transform.position);
        //        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.SPRING,param);
        //    }
            
        //}
    }

    private void CheckIfThereIsUnitOnLandingTile() {
        Ray ray = new Ray(this.Location1.transform.position, Vector3.up);
        Debug.DrawRay(this.Location1.transform.position, Vector3.up, Color.red, 5f);

        if (Physics.Raycast(ray, out RaycastHit hit, 50.0f, LayerMask.GetMask("Units"))) {
            if (hit.collider.gameObject.GetComponent<Unit>() is Unit unit) {
                unit.SpringDeath();
            }
        }
    }

}
