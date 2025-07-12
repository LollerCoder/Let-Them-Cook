using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class Shove : Skill
{

    //private int sucessChance = 90;

    private Unit _UnitHit; //the unit that got shoved into

    public Shove()
    {
        this.skillName = "Shove";
        this.veggieType = EVeggie.CARROT;
        this.skillType = ESkillType.BASIC;

        this.skillRange = 1;

        //for skill progressions
        this.cost = 30;

        SkillDatabase.Instance.GetSkillSprite(this);
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        //Debug.Log("Shoved!");
        PopUpManager.Instance.addPopUp(this.skillName + "d", target.transform);
        Vector3 dir = (target.gameObject.transform.position - origin.gameObject.transform.position).normalized;
        if (!this.WallChecker(target.gameObject.transform.position, dir)) this.ShoveTarget(target, origin, dir);

        //if (Random.Range(1, 100) < sucessChance)
        //{
        //    PopUpManager.Instance.addPopUp(this.skillName + "d", target.transform);
        //    Vector3 dir = (target.gameObject.transform.position - origin.gameObject.transform.position).normalized;
        //    if (!this.WallChecker(target.gameObject.transform.position, dir)) this.ShoveTarget(target, dir);
        //}
        //else
        //{
        //    PopUpManager.Instance.addPopUp("Failed :(", target.transform);
        //}
    }

    private void ShoveTarget (Unit target, Unit origin, Vector3 direction)
    {
        //backup the position
        Vector3 currPos = target.transform.position;

        //getting the direction relative to local position
        Vector3 localDir = target.gameObject.transform.InverseTransformDirection(direction);

        //move the target
        target.gameObject.transform.Translate(localDir);

        //Updating the unit's tile
        Tile landingSpot = this.GetLandingSpot(target);
        if (landingSpot != null)
        {
            target.Tile = landingSpot;
            target.gameObject.transform.position = new Vector3(
                target.gameObject.transform.position.x,
                landingSpot.gameObject.transform.position.y,
                target.gameObject.transform.position.z);

            target.TakeDamage(3, origin);
        }
        else
        {
            target.gameObject.transform.position = currPos;

            //if there is a wall add more damage
            target.TakeDamage(5, origin);

            //if the "wall" is a unit
            if (this._UnitHit != null) this._UnitHit.TakeDamage(2, origin);
        }

        target.AddEffect(new Dizzy(2, origin));
    }

    //checks if the position where target is getting shoved into has a wall or is occupied
    private bool WallChecker(Vector3 originPoint, Vector3 dir)
    {
        bool isWall = true;

        RaycastHit hit;
        originPoint += dir * 0.2f;  
        Debug.DrawRay(originPoint, dir, Color.red, 5f);
        Physics.Raycast(originPoint, dir, out hit, Mathf.Infinity);
        Debug.Log("Hit " + hit);
        if (hit.collider == null) return false;

        if (!hit.collider.gameObject.GetComponent<Unit>()) return false;

        if (hit.collider.gameObject.GetComponent<Unit>()) this._UnitHit = hit.collider.gameObject.GetComponent<Unit>();

        return isWall;
    }

    private Tile GetLandingSpot(Unit target)
    {
        //Getting the tile
        RaycastHit hit;
        Physics.Raycast(target.gameObject.transform.position, Vector3.down, out hit, Mathf.Infinity);
        if (hit.collider == null) return null;

        Tile landingSpot = hit.collider.gameObject.GetComponent<Tile>();
        if (landingSpot == null) return null;
        
        return landingSpot;
    }
}
