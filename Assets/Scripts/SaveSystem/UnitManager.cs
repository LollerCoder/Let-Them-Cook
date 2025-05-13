using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private GameObject unitParent;
    public GameObject unitTemplate;
    // Start is called before the first frame update
    void Start()
    {
        unitParent = this.gameObject;

        //manageParty();
    }

    public void manageParty()
    {
        //how mamy levels has the player accomplished?
        //int levelNum = GameScript.parent.transform.childCount;
        

        // if (levelNum >= 1) addUnit("Carrot", new Vector3(3,0.5f,3));
        // else if (levelNum >= 3) addUnit("Chili", new Vector3(3,0.5f,4));
        // else if (levelNum >= 5) addUnit("Cabbage", new Vector3(2,0.5f,5));
        // else if (levelNum >= 7) addUnit("Potato", new Vector3(3,0.5f,5) );
        // else if (levelNum >= 9) addUnit("Garlic", new Vector3(4,0.5f,4));
        // else if (levelNum >= 11) addUnit("Pumpkin", new Vector3(5,0.5f,3));
        // else if (levelNum >= 13) addUnit("Tomato", new Vector3(5,0.5f,4));
    }

    public void addUnit(string name, Vector3 position)
    {
        GameObject unit = Instantiate(unitTemplate, position, Quaternion.identity, unitParent.transform);

        //Add TRANSFORM
        //SPRITE RENDERER

        //ANIMATOR

        //NAME SCRIPT

        //Add it to parent
        //unit.transform.parent = unitParent.transform;
    }
}
