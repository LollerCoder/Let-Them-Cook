using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class Shove : Skill
{

    private int sucessChance = 90;

    public Shove()
    {
        this.skillName = "Shove";
        this.veggieType = EVeggie.CARROT;
        this.skillType = ESkillType.BASIC;

        //for skill progressions
        this.cost = 30;
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        if (Random.Range(1, 100) < sucessChance)
        {
            PopUpManager.Instance.addPopUp(this.skillName + "d", target.transform);
            Vector3 dir = (target.gameObject.transform.position - origin.gameObject.transform.position).normalized;
            this.ShoveTarget(target, dir);
        }
        else
        {
            PopUpManager.Instance.addPopUp("Failed :(", target.transform);
        }
    }

    private void ShoveTarget (Unit target, Vector3 direction)
    {
        //backup the position
        Vector3 currPos = target.transform.position;

        Debug.Log("Direciton: " + direction);

        //getting the direction relative to local position
        Vector3 localDir = target.gameObject.transform.InverseTransformDirection(direction);

        //move the target
        target.gameObject.transform.Translate(localDir);

        ////Check if horizontal or in x-axis
        //if (direction.z == 0)
        //{
        //    //moving it
        //    this.MoveHorizontaly(target, direction.x);
        //}
        //else if (direction.x == 0)
        //{
        //    //moving it
        //    this.MoveVerticaly(target, direction.z);
        //}

        //Updating the unit's tile
        Tile landingSpot = this.GetLandingSpot(target);
        if (landingSpot != null)
        {
            target.Tile = landingSpot;
        }
        else
        {
            target.gameObject.transform.position = currPos;
        }
    }

    private void MoveHorizontaly(Unit target, float isRight)
    {
        target.gameObject.transform.Translate(1 * isRight, 0, 0);
    }

    private void MoveVerticaly(Unit target, float isUp)
    {
        target.gameObject.transform.Translate(0, 0, 1 * isUp);
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
