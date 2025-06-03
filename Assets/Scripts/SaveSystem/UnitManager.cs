using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/*to be added in the Allies parent*/
public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private GameObject unitParent;
    public GameObject carrotTemplate, garlicTemplate /*will add more templates when assets are done*/;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        unitParent = this.gameObject;
        manageParty();
    }

    public void manageParty()
    {
        //what level number is the player at right now?
        int levelNum = int.Parse(SceneManager.GetActiveScene().name.Split("-")[1]);
        

        //if (levelNum >= 1) addUnit("Carrot", carrotTemplate, new Vector3(3,0.5f,4));

        /*for testing pruposes*/
       //if (levelNum >= 1) addUnit("Garlic", garlicTemplate, new Vector3(4,0.5f,4));

        /*Will uncomment when we have the assets for them*/
        // else if (levelNum >= 3) addUnit("Chili", new Vector3(3,0.5f,4));
        // else if (levelNum >= 5) addUnit("Cabbage", new Vector3(2,0.5f,5));
        // else if (levelNum >= 7) addUnit("Potato", new Vector3(3,0.5f,5) );
        //if (levelNum >= 9) addUnit("Garlic", new Vector3(4,0.5f,4));
        // else if (levelNum >= 11) addUnit("Pumpkin", new Vector3(5,0.5f,3));
        // else if (levelNum >= 13) addUnit("Tomato", new Vector3(5,0.5f,4));
    }

    public void addUnit(string name, GameObject template, Vector3 position, EUnitType type)
    {
        GameObject unit = Instantiate(template, position, Quaternion.identity, unitParent.transform);

        unit.name = name;

        unit.SetActive(true);

        unit.GetComponent<Unit>().Type = type;

        //Add it to parent
        unit.transform.parent = unitParent.transform;

        //Add unit in the UnitList
        //UnitActionManager.Instance.UnitList.Add(unit.GetComponent<Unit>());
    }
}
