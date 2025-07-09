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

    public bool bTomato = false, bPumpkin = false, bGarlic = false;

    public int UnitCounter = 0;

    private GameObject unitParent;
    public GameObject tomatoTemplate, garlicTemplate,pumpkinTemplate /*will add more templates when assets are done*/;
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

    public void manageParty(List<Tile> tiles)//, List<Unit> unitSpawn)
    {
  
        //what level number is the player at right now?
        int levelNum = int.Parse(SceneManager.GetActiveScene().name.Split("-")[1]);


        if (levelNum >= 1 && bTomato)
        {
            addUnit("Tomato", tomatoTemplate, tiles[UnitCounter], EUnitType.Ally, new List<Effect>());
            bTomato = false;
            UnitCounter+= 1;
        }

        if (levelNum >= 4 && bGarlic)
        {
            addUnit("Garlic", garlicTemplate, tiles[UnitCounter], EUnitType.Ally, new List<Effect>());
            bGarlic = false;
            UnitCounter+= 1;
        }


         if (levelNum >= 3 && bPumpkin)
        {
            addUnit("Pumpkin", pumpkinTemplate, tiles[UnitCounter], EUnitType.Ally, new List<Effect>());
            bPumpkin = false;
            UnitCounter+= 1;
        }
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
        UnitActionManager.Instance.AddUnit(unit.GetComponent<Unit>());
        UnitActionManager.Instance.DecideTurnOrder();
    }



}
