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

    public bool bTomato = false;

    private GameObject unitParent;
    public GameObject tomatoTemplate, garlicTemplate /*will add more templates when assets are done*/;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        unitParent = this.gameObject;
    }

    public void manageParty()
    {
        Debug.Log("Tomato is " + bTomato);
        //what level number is the player at right now?
        int levelNum = int.Parse(SceneManager.GetActiveScene().name.Split("-")[1]);


        if (levelNum >= 1 && bTomato)
        {
            addUnit("Tomato", tomatoTemplate, new Vector3(3, 0.5f, 4), EUnitType.Ally, new List<Effect>());
            bTomato = false;
        }

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


    public void addUnit(string name, GameObject template, Vector3 tile_spawn_loc, EUnitType type, List<Effect> effects)
    {
        GameObject unitGO = CreateUnitGameObject(name,
            template,
            tile_spawn_loc);

        //unitGO.transform.Translate(0, 0.25f, 0);

        Unit unit = unitGO.GetComponent<Unit>();
        unit.Type = type;
        unit.AddEffects(effects);

        //assigning the tile
        unit.Tile = TileMapManager.Instance.TileMap[new Vector2Int((int)tile_spawn_loc.x, (int)tile_spawn_loc.z)];

        this.addUnitToList(unit.GetComponent<Unit>());
    }


    public void addUnit(string name, GameObject template, Tile tile_spawn_loc, EUnitType type, List<Effect> effects)
    {
        GameObject unitGO = CreateUnitGameObject(name,
            template,
            tile_spawn_loc.gameObject.transform.position);

        unitGO.transform.Translate(0, 0.25f, 0);

        Unit unit = unitGO.GetComponent<Unit>();
        unit.Type = type;
        unit.Tile = tile_spawn_loc;
        unit.AddEffects(effects);

        this.addUnitToList(unit.GetComponent<Unit>());
    }

    private GameObject CreateUnitGameObject(string name, GameObject template, Vector3 pos)
    {
        GameObject unit = Instantiate(template,
            pos,
            Quaternion.identity,
            unitParent.transform
            );

        //Add it to parent
        unit.transform.parent = unitParent.transform;

        unit.SetActive(true);

        return unit;
    }

    private void addUnitToList(Unit unit)
    {
        //Add it to parent
        unit.transform.parent = unitParent.transform;

        //Add unit in the UnitList
        UnitActionManager.Instance.UnitList.Add(unit.GetComponent<Unit>());
        UnitActionManager.Instance.DecideTurnOrder();
    }



}
