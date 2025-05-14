using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardSystem : MonoBehaviour
{
    private UnitStats stats;
    //private SaveFile data;
    /*Save Game Components*/
    public static RewardSystem Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else Instance = this;
    }
    /*
    -Current level of Map/Battle 
    -Number of enemies
    -Number of party members

    enmies * static * party members * lvel map // if dead member, half 

    Why make different vegatbles if the lveling s all the same
    -random number generator
    */
    public void gainRewards(int outcome, int enemies, int members, int deadMembers, List<Unit> partyList)
    {
        // data = AssetDatabase.LoadAssetAtPath<SaveFile>("Assets/Scripts/BattleSystem/Sample/New Save File.asset");
        // if (data == null)
        // {
        //     // Create and save ScriptableObject because it doesn't exist yet
        //     data = ScriptableObject.CreateInstance<SaveFile>();
        //     data.cabbageCount = InventoryManager.Instance.getItemAmount(EIngredientType.CABBAGE);
        //     data.carrotCount = InventoryManager.Instance.getItemAmount(EIngredientType.CARROT);
        //     data.chiliCount = InventoryManager.Instance.getItemAmount(EIngredientType.CHILI);
        //     data.potatoCount = InventoryManager.Instance.getItemAmount(EIngredientType.POTATO);
        //     AssetDatabase.CreateAsset(data, "Assets/Scripts/BattleSystem/Sample/New Save File.asset");
        // }

        float totalexp = 0f;
        /*total number of raw ingredients left in the battle field*/
     

        /*EXPERIENCE GAINER*/
        /*current level of the map .  BattleScene Names are like this "<WorldName>-<LevelNumber>*/
        int levelNum = int.Parse(SceneManager.GetActiveScene().name.Split("-")[1]);

        List<Unit> party = new List<Unit>();
    

        /*Max Number of EXP is 100*/
        

        /*If players won the game*/
        if (outcome == 2)
        {
            totalexp = (levelNum + enemies + (members - deadMembers) + (int)(deadMembers / 2)) * (levelNum * 10);
        }

        /*if enemies won the game*/
        else
        {
            totalexp = levelNum + enemies + (int)(members/2);
        }

        //distribute the exp to the party
        // foreach(Unit unit in partyList) {
        //     if(unit.Type == EUnitType.Ally) {

        //         //active party members
        //         if (unit.HP >= 1) unit.Experience += totalexp;

        //         //dead members
        //         else unit.Experience += totalexp/2;

        //         //update the stats
        //         stats.SetUnitStats(unit);
        //         party.Add(unit);
        //     }
        // }
        
        //Autosaves the games progress

        // data.cabbageCount = InventoryManager.Instance.getItemAmount(EIngredientType.CABBAGE);
        // data.carrotCount = InventoryManager.Instance.getItemAmount(EIngredientType.CARROT);
        // data.chiliCount = InventoryManager.Instance.getItemAmount(EIngredientType.CHILI);
        // data.potatoCount = InventoryManager.Instance.getItemAmount(EIngredientType.POTATO);
        //AssetDatabase.CreateAsset(data, "Assets/Scripts/BattleSystem/Sample/New Save File.asset");
    }
}
