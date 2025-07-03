using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircularCut : Skill
{


    private float damage = 7.0f;
    List<Vector3> cardinalDirs = new List<Vector3>();

    private Dictionary<Unit, Vector3> neighbors = new Dictionary<Unit, Vector3>();
    //List<Unit> neighbors = new List<Unit>();
    Unit neighbor = null;
    public CircularCut()
    {
        this.skillName = "Circular Cut";
        this.veggieType = EVeggie.CARROT;
        this.skillType = ESkillType.AOE;

        this.skillRange = 1;

        //for skill progressions
        this.cost = 30;


        cardinalDirs.Add(Vector3.left);  //enemy 
        cardinalDirs.Add(Vector3.right); // dummy
        cardinalDirs.Add(Vector3.forward); // dummy1 behind carrot
        cardinalDirs.Add(Vector3.back); //dummy2 in front of carrot

        this.defaultIcon = Resources.Load<Sprite>("Skills/skillDefault");
        this.highlightedIcon = Resources.Load<Sprite>("Skills/skillHighlighted");
    }

    public override void SkillAction(Unit target, Unit origin)
    {
        origin.AddEffect(new Dizzy(2, origin));
        PopUpManager.Instance.addPopUp(this.skillName, origin.transform);

        //target
        target.TakeDamage(damage, origin);
        PopUpManager.Instance.addPopUp("-" + damage.ToString(), target.transform);

        if (!GameSettingsManager.Instance.enableCutscene)
        {
            GetNeighborList(origin,target);
            foreach (Unit neighbor in neighbors.Keys)
            {
                
                neighbor.TakeDamage(damage, origin);
                PopUpManager.Instance.addPopUp("-" + damage.ToString(), neighbor.transform);
            }
        }
        else
        {
            foreach (var neighbor in neighbors.Keys)
            {

                neighbor.TakeDamage(damage, origin);
                PopUpManager.Instance.addPopUp("-" + damage.ToString(), neighbor.transform);
            }
        }
      



    }
    public override Parameters GetNeighborList(Unit origin, Unit Target)
    {
        Parameters param = new Parameters();
        Debug.Log("Finding neighbors");
            foreach (Vector3 dir in cardinalDirs)
            {
                neighbor = this.Get_Neighbor(origin.transform.position, dir);
                Debug.Log("Neighbor" + neighbor);
                if (neighbor != null  )
                { //&& neighbor != Target

                    neighbors.Add(neighbor, dir);
                   

                }

            }
            if (GameSettingsManager.Instance.enableCutscene)
            {
                int x = 0;
                
                foreach (Unit adjacent in neighbors.Keys)
                {
                    param.PutExtra("Dummy" + x, adjacent);
                    
                    //Debug.Log("Dummy" + x);
                    x++;
                }
                x = 0;
                foreach (Vector3 dir in neighbors.Values)
                {
                    param.PutExtra("Direction" + x, dir);
                    x++;
                }
                param.PutExtra("DummyCount", neighbors.Count);
                
                //EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CUTSCENE_AOE,param);
            //Debug.Log("Event made");
                neighbors.Clear();
            }
        return param;
       
    }
    

    private Unit Get_Neighbor(Vector3 originPoint, Vector3 dir)
    {
        RaycastHit hit;
        originPoint += dir * 0.2f;
        Debug.DrawRay(originPoint, dir, Color.red, 5f);
        Physics.Raycast(originPoint, dir, out hit, 1.0f);
        Debug.Log("Hit " + hit);

        if (hit.collider == null) return null;

        return hit.collider.gameObject.GetComponent<Unit>();
    }

    
}
