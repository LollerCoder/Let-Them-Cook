using System.Collections.Generic;
using UnityEngine;

public class CircularCut : Skill
{
    private float damage = 10.0f;

    public CircularCut()
    {
        this.skillName = "Circular Cut";
        this.veggieType = EVeggie.CARROT;
        this.skillType = ESkillType.AOE;
        


        //for skill progressions
        this.cost = 30;
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        PopUpManager.Instance.addPopUp(this.skillName, origin.transform);

        List<Vector3> cardinalDirs = new List<Vector3>();
        cardinalDirs.Add(Vector3.left);
        cardinalDirs.Add(Vector3.right);
        cardinalDirs.Add(Vector3.forward);
        cardinalDirs.Add(Vector3.back);

        Unit neighbor;

        foreach (Vector3 dir in cardinalDirs)
        {
            neighbor = this.Get_Neighbor(origin.transform.position, dir);
            Debug.Log("Neighbor" + neighbor);
            if (neighbor != null)
            {
                neighbor.TakeDamage(damage, origin);
                PopUpManager.Instance.addPopUp("-" + damage.ToString(), neighbor.transform);
            }
        }
    }

    private Unit Get_Neighbor(Vector3 originPoint, Vector3 dir)
    {
        RaycastHit hit;
        originPoint += dir * 0.2f;
        Debug.DrawRay(originPoint, dir, Color.red, 5f);
        Physics.Raycast(originPoint, dir, out hit, Mathf.Infinity);
        Debug.Log("Hit " + hit);

        if (hit.collider == null) return null;

        return hit.collider.gameObject.GetComponent<Unit>();
    }
}
